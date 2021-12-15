using MemoryToolKit.MemoryUtils;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.ProcessUtils;

/// <summary>
///     Provides a myriad of extension methods on the <see cref="Process"/> object.
/// </summary>
public static partial class ProcessExtensions
{
	#region General
	/// <summary>
	///     Checks whether the specified process' architecture is 64-bit.
	/// </summary>
	/// <param name="process">The process to check.</param>
	/// <returns>
	///     <see langword="true"/> if the process' architecture is 64-bit;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool Is64Bit(this Process process)
	{
		WinAPI.IsWow64Process(process.Handle, out var Wow64Process);
		return Environment.Is64BitOperatingSystem ? !Wow64Process : Wow64Process;
	}

	/// <summary>
	///     Retrieves a process' pointer size based on its architecture.
	/// </summary>
	/// <param name="process">The process to check.</param>
	/// <returns>
	///     0x8 for 64-bit processes;
	///     0x4 for 32-bit processes.
	/// </returns>
	public static int PointerSize(this Process process)
	{
		return process.Is64Bit() ? 0x8 : 0x4;
	}

	/// <summary>
	///     Reads a target address in an assembly based on the process' architecture.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address which references the target.</param>
	public static IntPtr FromAssemblyAddress(this Process process, IntPtr address)
	{
		return process.Is64Bit() ? process.FromRelativeAddress(address) : process.FromAbsoluteAddress(address);
	}

	/// <summary>
	///     Reads a target address in an assembly relative to a specified address.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address which references the target.</param>
	public static IntPtr FromRelativeAddress(this Process process, IntPtr address)
	{
		return address + 0x4 + process.Read<int>(address);
	}

	/// <summary>
	///     Reads a target address in an assembly at a specified address.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address which references the target.</param>
	public static IntPtr FromAbsoluteAddress(this Process process, IntPtr address)
	{
		return process.Read<IntPtr>(address);
	}

	/// <summary>
	///     Enumerates the memory pages currently loaded by the specified process.
	/// </summary>
	/// <param name="process">The process to check.</param>
	/// <param name="allPages">Specifies whether to include pages that are not private or guarded.</param>
	public static IEnumerable<MEMORY_BASIC_INFORMATION> MemoryPages(this Process process, bool allPages = false)
	{
		long address = 0x10000, maximum = process.Is64Bit() ? 0x7FFFFFFEFFFF : 0x7FFEFFFF;
		var mbiSize = Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));

		while (WinAPI.VirtualQueryEx(process.Handle, (IntPtr)(address), out var mbi, mbiSize) != SIZE_T.Zero)
		{
			address += (long)(mbi.RegionSize);
			var privateAndNotGuarded = mbi.Type == MemPageType.MEM_PRIVATE && !mbi.Protect.HasFlag(MemPageProtect.PAGE_GUARD);

			if (mbi.State == MemPageState.MEM_COMMIT && (allPages || privateAndNotGuarded))
				yield return mbi;

			if (address >= maximum)
				yield break;
		}
	}
	#endregion



	#region Modules
	/// <summary>
	///     Retrieves the specified module by name.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="moduleName">The name of the module, including file extension.</param>
	/// <returns>
	///     The <see cref="ProcessModule"/> object with the specified name if it can be found;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static ProcessModule Module(this Process process, string moduleName)
	{
		return process.Modules().FirstOrDefault(module => module.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
	}

	private static Dictionary<int, ProcessModule[]> ModuleCache = new();

	/// <summary>
	///     Retrieves an array of <see cref="ProcessModule"/> objects currently loaded by the specified process.
	/// </summary>
	/// <param name="process">The process from which to fetch the modules.</param>
	/// <param name="maximum">The maximum amount of modules to return.</param>
	public static ProcessModule[] Modules(this Process process, int maximum = 1024)
	{
		var hModules = new IntPtr[maximum];

		if (!WinAPI.EnumProcessModulesEx(process.Handle, hModules, (uint)(IntPtr.Size * maximum), out uint totalModules, 3))
			return Array.Empty<ProcessModule>();

		var moduleCount = totalModules / (uint)(IntPtr.Size);
		var hash = process.StartTime.GetHashCode() + process.Id + (int)(moduleCount);

		if (ModuleCache.TryGetValue(hash, out var cachedModules))
			return cachedModules;

		lock (ModuleCache)
		{
			if (ModuleCache.Count > 100)
				ModuleCache.Clear();

			var modules = new List<ProcessModule>();
			var strBuilder = new StringBuilder(200);
			var procHandle = process.Handle;

			for (int i = 0; i < moduleCount; ++i)
			{
				var moduleHandle = hModules[i];

				if (WinAPI.GetModuleFileNameEx(procHandle, moduleHandle, strBuilder, (uint)(strBuilder.Capacity)) == 0)
					return modules.ToArray();

				var fileName = strBuilder.ToString();
				strBuilder.Clear();

				if (WinAPI.GetModuleBaseName(procHandle, moduleHandle, strBuilder, (uint)(strBuilder.Capacity)) == 0)
					return modules.ToArray();

				var moduleName = strBuilder.ToString();
				strBuilder.Clear();

				var moduleInfo = new MODULEINFO();

				if (!WinAPI.GetModuleInformation(procHandle, moduleHandle, out moduleInfo, (uint)(Marshal.SizeOf(moduleInfo))))
					return modules.ToArray();

				modules.Add(new()
				{
					BaseAddress = moduleInfo.lpBaseOfDll,
					EntryPointAddress = moduleInfo.EntryPoint,
					ModuleMemorySize = (int)(moduleInfo.SizeOfImage),
					FileName = fileName,
					ModuleName = moduleName
				});
			}

			ModuleCache[hash] = modules.ToArray();
			return modules.ToArray();
		}
	}
	#endregion



	#region Debug Symbols
	/// <summary>
	///     Retrieves the address of a global debug symbol within the process' main module.
	/// </summary>
	/// <param name="process">The process to check.</param>
	/// <param name="symbol">The symbol to find.</param>
	/// <param name="searchPDB">
	///     When set to <see langword="true"/>, will attempt to load symbols from a .PDB file in the same directory as the executable;
	///     otherwise, attempts to load embedded symbols.<br/>
	///     Note: if no .PDB file is found, no symbols can be loaded.
	/// </param>
	/// <returns>
	///     The symbol's address if it can be found;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr SymbolAddress(this Process process, string symbol, bool searchPDB = false)
	{
		return process.SymbolAddress(process.MainModule.ModuleName, symbol, searchPDB);
	}

	/// <summary>
	///     Retrieves the address of a global debug symbol within a specified module.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="moduleName">The name of the module, including file extension.</param>
	/// <param name="symbol">The symbol to find.</param>
	/// <param name="searchPDB">
	///     When set to <see langword="true"/>, will attempt to load symbols from a .PDB file in the same directory as the executable;
	///     otherwise, attempts to load embedded symbols.<br/>
	///     Note: if no .PDB file is found, no symbols can be loaded.
	/// </param>
	/// <returns>
	///     The symbol's address if it can be found;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr SymbolAddress(this Process process, string moduleName, string symbol, bool searchPDB = false)
	{
		try
		{
			if (process.Module(moduleName) is not ProcessModule module)
				return IntPtr.Zero;

			var symbols = process.AllSymbols(module, searchPDB, symbol);
			return symbols.Any() ? (IntPtr)(symbols.First().Address) : IntPtr.Zero;
		}
		catch (Exception ex)
		{
			Trace.TraceError(ex.ToString());
			return IntPtr.Zero;
		}
	}

	/// <summary>
	///     Retrieves all symbols within a specified module.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="module">The module to check.</param>
	/// <param name="searchPDB">
	///     When set to <see langword="true"/>, will attempt to load symbols from a .PDB file in the same directory as the executable;
	///     otherwise, attempts to load embedded symbols.<br/>
	///     Note: if no .PDB file is found, no symbols can be loaded.
	/// </param>
	/// <param name="mask">A wildcard string that indicates the names of the symbols to be enumerated.</param>
	/// <returns>
	///     The symbols which matched <paramref name="mask"/>.
	/// </returns>
	public static IEnumerable<SYMBOL_INFO> AllSymbols(this Process process, ProcessModule module, bool searchPDB = false, string mask = "*")
	{
		var procHandle = process.Handle;
		var symbols = new List<SYMBOL_INFO>();

		if (!WinAPI.SymInitialize(procHandle, searchPDB ? Path.GetDirectoryName(module.FileName) : null, false))
			throw new Exception("Failed to initialize symbols.");

		try
		{
			if (WinAPI.SymLoadModuleEx(procHandle, IntPtr.Zero, module.ModuleName, null, (ulong)(module.BaseAddress), (uint)(module.ModuleMemorySize), IntPtr.Zero, 0) == 0)
				throw new Exception("Failed to load module's symbols.");

			WinAPI.PSYM_ENUMERATESYMBOLS_CALLBACK callback = new(enumSyms);

			if (!WinAPI.SymEnumSymbols(procHandle, (ulong)(module.BaseAddress), mask, callback, IntPtr.Zero))
				throw new Exception("Failed to enumerate over module's symbols.");
		}
		finally
		{
			WinAPI.SymCleanup(procHandle);
		}

		return symbols;

		bool enumSyms(ref SYMBOL_INFO pSymInfo, uint SymbolSizem, IntPtr UserContext)
		{
			symbols.Add(pSymInfo);
			return true;
		}
	}
	#endregion
}
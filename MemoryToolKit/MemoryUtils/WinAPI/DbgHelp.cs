using System.Runtime.InteropServices;

namespace MemoryToolKit.MemoryUtils;

internal static partial class WinAPI
{
	/// <summary>
	///     Initializes the symbol handler for a process.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-syminitialize">SymInitialize function</see>.
	/// </summary>
	/// <param name="hProcess">A handle that identifies the caller.</param>
	/// <param name="UserSearchPath">The path, or series of paths separated by a semicolon, that is used to search for symbol files.</param>
	/// <param name="fInvadeProcess">If <see langword="true"/>, enumerates the loaded modules for the process and effectively calls the <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symloadmodule64">SymLoadModule64 function</see> for each module.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SymInitialize([In] IntPtr hProcess, [In, Optional] string UserSearchPath, [In] bool fInvadeProcess);

	/// <summary>
	///     Loads the symbol table for the specified module.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symloadmoduleex">SymLoadModuleEx function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process that was originally passed to the <see cref="SymInitialize"/> function.</param>
	/// <param name="hFile">A handle to the file for the executable image.</param>
	/// <param name="ImageName">The name of the executable image.</param>
	/// <param name="ModuleName">A shortcut name for the module.</param>
	/// <param name="BaseOfDll">The load address of the module.</param>
	/// <param name="DllSize">The size of the module, in bytes.</param>
	/// <param name="Data">A pointer to a MODLOAD_DATA structure that represents headers other than the standard PE header.</param>
	/// <param name="Flags">If this parameter is zero, the function loads the modules and the symbols for the module.</param>
	/// <returns>
	///     The base address of the loaded module if the function succeeds;
	///     otherwise, 0.
	/// </returns>
	[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.U8)]
	public static extern ulong SymLoadModuleEx([In] IntPtr hProcess, [In] IntPtr hFile, [In] string ImageName, [In] string ModuleName, [In, MarshalAs(UnmanagedType.U8)] ulong BaseOfDll, [In] uint DllSize, [In] IntPtr Data, [In] uint Flags);

	/// <summary>
	///     Enumerates all symbols in a process.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symenumsymbols">SymEnumSymbols function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to a process.</param>
	/// <param name="BaseOfDll">The base address of the module.</param>
	/// <param name="Mask">A wildcard string that indicates the names of the symbols to be enumerated.</param>
	/// <param name="EnumSymbolsCallback">A <see cref="PSYM_ENUMERATESYMBOLS_CALLBACK"/> callback function that receives the symbol information.</param>
	/// <param name="UserContext">A user-defined value that is passed to the callback function.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SymEnumSymbols([In] IntPtr hProcess, [In] ulong BaseOfDll, [In, Optional] string Mask, [In] PSYM_ENUMERATESYMBOLS_CALLBACK EnumSymbolsCallback, [In, Optional] IntPtr UserContext);

	/// <summary>
	///     An application-defined callback function used with the SymEnumSymbols.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nc-dbghelp-psym_enumeratesymbols_callback">PSYM_ENUMERATESYMBOLS_CALLBACK callback function</see>.
	/// </summary>
	/// <param name="pSymInfo">A pointer to a SYMBOL_INFO structure that provides information about the symbol.</param>
	/// <param name="SymbolSize">The size of the symbol, in bytes.</param>
	/// <param name="UserContext">The user-defined value passed from the SymEnumSymbols function.</param>
	/// <remarks>
	///     If the function returns <see langword="true"/>, the enumeration continues;
	///     otherwise, it will stop.
	/// </remarks>
	public delegate bool PSYM_ENUMERATESYMBOLS_CALLBACK([In] ref SYMBOL_INFO pSymInfo, [In] uint SymbolSize, [In, Optional] IntPtr UserContext);

	/// <summary>
	///     Deallocates all resources associated with the process handle.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/nf-dbghelp-symcleanup">SymCleanup function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process that was originally passed to the <see cref="SymInitialize"/> function.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("dbghelp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SymCleanup([In] IntPtr hProcess);
}
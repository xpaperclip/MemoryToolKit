using System.Runtime.InteropServices;
using System.Text;

namespace MemoryToolKit.MemoryUtils;

internal static partial class WinAPI
{
	/// <summary>
	///     Retrieves a handle for each module in the specified process that meets the specified filter criteria.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/psapi/nf-psapi-enumprocessmodulesex">EnumProcessModulesEx function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process.</param>
	/// <param name="lphModule">An array that receives the list of module handles.</param>
	/// <param name="cb">The size of the <paramref name="lphModule"/> array, in bytes.</param>
	/// <param name="lpcbNeeded">The number of bytes required to store all module handles in the <paramref name="lphModule"/> array.</param>
	/// <param name="dwFilterFlag">The filter criteria.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("psapi.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool EnumProcessModulesEx([In] IntPtr hProcess, [Out] IntPtr[] lphModule, [In] uint cb, [Out, MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded, [In] uint dwFilterFlag);

	/// <summary>
	///     Retrieves the fully qualified path for the file containing the specified module.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/psapi/nf-psapi-getmodulefilenameexa">GetModuleFileNameExA function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process that contains the module.</param>
	/// <param name="hModule">A handle to the module. If this parameter is <see langword="null"/>, GetModuleFileNameEx returns the path of the executable file of the process specified in <paramref name="hProcess"/>.</param>
	/// <param name="lpFilename">A pointer to a buffer that receives the fully qualified path to the module.</param>
	/// <param name="nSize">The size of the <paramref name="lpFilename"/> buffer, in characters.</param>
	/// <returns>
	///     The length of the string copied to the buffer, in characters, if the function succeeds;
	///     otherwise, 0.
	/// </returns>
	[DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint GetModuleFileNameEx([In] IntPtr hProcess, [In, Optional] IntPtr hModule, [Out] StringBuilder lpFilename, [In] uint nSize);

	/// <summary>
	///     Retrieves the base name of the specified module.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/psapi/nf-psapi-getmodulebasenamea">GetModuleBaseNameA function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process that contains the module.</param>
	/// <param name="hModule">A handle to the module.</param>
	/// <param name="lpBaseName">A pointer to the buffer that receives the base name of the module.</param>
	/// <param name="nSize">The size of the <paramref name="lpBaseName"/> buffer, in characters.</param>
	/// <returns>
	///     The length of the string copied to the buffer, in characters, if the function succeeds;
	///     otherwise, 0.
	/// </returns>
	[DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	[return: MarshalAs(UnmanagedType.U4)]
	public static extern uint GetModuleBaseName([In] IntPtr hProcess, [In, Optional] IntPtr hModule, [Out] StringBuilder lpBaseName, [In] uint nSize);

	/// <summary>
	///     Retrieves information about the specified module in the <see cref="MODULEINFO"/> structure.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/psapi/nf-psapi-getmoduleinformation">GetModuleInformation function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process that contains the module.</param>
	/// <param name="hModule">A handle to the module.</param>
	/// <param name="lpmodinfo">A pointer to the <see cref="MODULEINFO"/> structure that receives information about the module.</param>
	/// <param name="cb">The size of the <see cref="MODULEINFO"/> structure, in bytes.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("psapi.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetModuleInformation([In] IntPtr hProcess, [In] IntPtr hModule, [Out] out MODULEINFO lpmodinfo, [In] uint cb);
}
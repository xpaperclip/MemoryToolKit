using System.Runtime.InteropServices;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Provides several functions from the Win32 API.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/">Microsoft Docs</see>.
/// </remarks>
internal static partial class WinAPI
{
	/// <summary>
	///     Determines whether the specified process is running under WOW64 or an Intel64 of x64 processor.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/wow64apiset/nf-wow64apiset-iswow64process">IsWow64Process function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process.</param>
	/// <param name="Wow64Process">
	///     A pointer to a value that is set to <see langword="true"/> if the process is running under WOW64 on an Intel64 or x64 processor.
	///     <para>
	///     If the process is running under 32-bit Windows, the value is set to <see langword="false"/>.
	///     If the process is a 32-bit application running under 64-bit Windows 10 on ARM, the value is set to <see langword="false"/>.
	///     If the process is a 64-bit application running under 64-bit Windows, the value is also set to <see langword="false"/>.
	///     </para>
	/// </param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWow64Process([In] IntPtr hProcess, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

	/// <summary>
	///     Copies the data in the specified address range from the address space of the specified process into the specified buffer of the current process.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-readprocessmemory">ReadProcessMemory function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process with memory that is being read.</param>
	/// <param name="lpBaseAddress">A pointer to the base address in the specified process from which to read.</param>
	/// <param name="lpBuffer">A pointer to a buffer that receives the contents from the address space of the specified process.</param>
	/// <param name="nSize">The number of bytes to be read from the specified process.</param>
	/// <param name="lpNumberOfBytesRead">A pointer to a variable that receives the number of bytes transferred into the specified buffer.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ReadProcessMemory([In] IntPtr hProcess, [In] IntPtr lpBaseAddress, [Out] byte[] lpBuffer, [In] SIZE_T nSize, [Out] out SIZE_T lpNumberOfBytesRead);

	/// <summary>
	///     Writes data to an area of memory in a specified process.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-writeprocessmemory">WriteProcessMemory function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process memory to be modified.</param>
	/// <param name="lpBaseAddress">A pointer to the base address in the specified process to which data is written.</param>
	/// <param name="lpBuffer">A pointer to the buffer that contains data to be written in the address space of the specified process.</param>
	/// <param name="nSize">The number of bytes to be written to the specified process.</param>
	/// <param name="lpNumberOfBytesWritten">A pointer to a variable that receives the number of bytes transferred into the specified process.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool WriteProcessMemory([In] IntPtr hProcess, [In] IntPtr lpBaseAddress, [In] byte[] lpBuffer, [In] SIZE_T nSize, [Out] out SIZE_T lpNumberOfBytesWritten);

	/// <summary>
	///     Retrieves information about a range of pages within the virtual address space of a specified process.<br/>
	///     For further information see:
	///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualqueryex">VirtualQueryEx function</see>.
	/// </summary>
	/// <param name="hProcess">A handle to the process whose memory information is queried.</param>
	/// <param name="lpAddress">A pointer to the base address of the region of pages to be queried.</param>
	/// <param name="lpBuffer">A pointer to a <see cref="MEMORY_BASIC_INFORMATION"/> structure in which information about the specified page range is returned.</param>
	/// <param name="dwLength">The size of the buffer pointed to by <paramref name="lpBuffer"/>, in bytes.</param>
	/// <returns>
	///     The actual number of bytes returned in the information buffer if the function succeeds;
	///     otherwise, 0.
	/// </returns>
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern SIZE_T VirtualQueryEx([In] IntPtr hProcess, [In, Optional] IntPtr lpAddress, [Out] out MEMORY_BASIC_INFORMATION lpBuffer, [In] int dwLength);
}
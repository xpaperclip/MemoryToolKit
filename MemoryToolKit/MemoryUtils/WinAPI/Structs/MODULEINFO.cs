using System.Runtime.InteropServices;

namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Contains the module load address, size, and entry point.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/psapi/ns-psapi-moduleinfo">MODULEINFO structure</see>.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct MODULEINFO
{
	/// <summary>
	///     The load address of the module.
	/// </summary>
	public IntPtr lpBaseOfDll;

	/// <summary>
	///     The size of the linear space that the module occupies, in bytes.
	/// </summary>
	public uint SizeOfImage;

	/// <summary>
	///     The entry point of the module.
	/// </summary>
	public IntPtr EntryPoint;
}
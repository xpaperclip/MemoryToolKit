using System.Runtime.InteropServices;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Contains information about a range of pages in the virtual address space of a process.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-memory_basic_information">MEMORY_BASIC_INFORMATION structure</see>.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct MEMORY_BASIC_INFORMATION
{
	/// <summary>
	///     A pointer to the base address of the region of pages.
	/// </summary>
	public IntPtr BaseAddress;

	/// <summary>
	///     A pointer to the base address of a range of pages allocated by the VirtualAlloc function.
	/// </summary>
	public IntPtr AllocationBase;

	/// <summary>
	///     The memory protection option when the region was initially allocated.
	/// </summary>
	public MemPageProtect AllocationProtect;

	/// <summary>
	///     The size of the region beginning at the base address in which all pages have identical attributes, in bytes.
	/// </summary>
	public SIZE_T RegionSize;

	/// <summary>
	///     The state of the pages in the region.
	/// </summary>
	public MemPageState State;

	/// <summary>
	///     The access protection of the pages in the region.
	/// </summary>
	public MemPageProtect Protect;

	/// <summary>
	///     The type of pages in the region.
	/// </summary>
	public MemPageType Type;
}
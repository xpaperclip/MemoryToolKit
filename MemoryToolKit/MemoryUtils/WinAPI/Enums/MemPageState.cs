namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Provides memory page state constants.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/memory/memory-protection-constants#constants">Memory Protection Constants</see>.
/// </remarks>
public enum MemPageState : uint
{
	/// <summary>
	///     Indicates committed pages for which physical storage has been allocated, either in memory or in the paging file on disk.
	/// </summary>
	MEM_COMMIT = 0x01000,

	/// <summary>
	///     Indicates free pages not accessible to the calling process and available to be allocated.
	/// </summary>
	MEM_RESERVE = 0x02000,

	/// <summary>
	///     Indicates reserved pages where a range of the process's virtual address space is reserved without any physical storage being allocated.
	/// </summary>
	MEM_RELEASE = 0x10000
}
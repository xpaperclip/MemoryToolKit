﻿namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Provides memory-protection constants.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/memory/memory-protection-constants#constants">Memory Protection Constants</see>.
/// </remarks>
[Flags]
public enum MemPageProtect : uint
{
	/// <summary>
	///     Disables all access to the committed region of pages.
	/// </summary>
	PAGE_NOACCESS = 0x001,

	/// <summary>
	///     Enables read-only access to the committed region of pages.
	/// </summary>
	PAGE_READONLY = 0x002,

	/// <summary>
	///     Enables read-only or read/write access to the committed region of pages.
	/// </summary>
	PAGE_READWRITE = 0x004,

	/// <summary>
	///     Enables read-only or copy-on-write access to a mapped view of a file mapping object.
	/// </summary>
	PAGE_WRITECOPY = 0x008,

	/// <summary>
	///     Enables execute access to the committed region of pages.
	/// </summary>
	PAGE_EXECUTE = 0x010,

	/// <summary>
	///     Enables execute or read-only access to the committed region of pages.
	/// </summary>
	PAGE_EXECUTE_READ = 0x020,

	/// <summary>
	///     Enables execute, read-only, or read/write access to the committed region of pages.
	/// </summary>
	PAGE_EXECUTE_READWRITE = 0x040,

	/// <summary>
	///     Enables execute, read-only, or copy-on-write access to a mapped view of a file mapping object.
	/// </summary>
	PAGE_EXECUTE_WRITECOPY = 0x080,

	/// <summary>
	///     Pages in the region become guard pages.
	/// </summary>
	PAGE_GUARD = 0x100,

	/// <summary>
	///     Sets all pages to be non-cachable.
	/// </summary>
	PAGE_NOCACHE = 0x200,

	/// <summary>
	///     Sets all pages to be write-combined.
	/// </summary>
	PAGE_WRITECOMBINE = 0x400
}
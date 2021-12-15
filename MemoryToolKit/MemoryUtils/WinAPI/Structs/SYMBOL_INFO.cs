using System.Runtime.InteropServices;

namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Contains symbol information.
/// </summary>
/// <remarks>
///     For further information see:
///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/dbghelp/ns-dbghelp-symbol_info">SYMBOL_INFO structure</see>.
/// </remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct SYMBOL_INFO
{
	/// <summary>
	///     The size of the structure, in bytes.
	/// </summary>
	public uint SizeOfStruct;

	/// <summary>
	///     A unique value that identifies the type data that describes the symbol.
	/// </summary>
	public uint TypeIndex;

	/// <summary>
	///     This member is reserved for system use.
	/// </summary>
	public ulong Reserved_0;

	/// <summary>
	///     This member is reserved for system use.
	/// </summary>
	public ulong Reserved_1;

	/// <summary>
	///     The unique value for the symbol.
	/// </summary>
	public uint Index;

	/// <summary>
	///     The symbol size, in bytes.
	/// </summary>
	public uint Size;

	/// <summary>
	///     The base address of the module that contains the symbol.
	/// </summary>
	public ulong ModBase;

	/// <summary>
	///     The type of the symbol.
	/// </summary>
	public uint Flags;

	/// <summary>
	///     The value of a constant.
	/// </summary>
	public ulong Value;

	/// <summary>
	///     The virtual address of the start of the symbol.
	/// </summary>
	public ulong Address;

	/// <summary>
	///     The register.
	/// </summary>
	public uint Register;

	/// <summary>
	///     The DIA scope.
	/// </summary>
	public uint Scope;

	/// <summary>
	///     The PDB classification.
	/// </summary>
	public uint Tag;

	/// <summary>
	///     The length of the name, in characters, not including the null-terminating character.
	/// </summary>
	public uint NameLen;

	/// <summary>
	///     The size of the Name buffer, in characters.
	/// </summary>
	public uint MaxNameLen;

	/// <summary>
	///     The name of the symbol.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
	public string Name;
}
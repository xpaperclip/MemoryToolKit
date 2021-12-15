namespace MemoryToolKit.MemoryUtils;

/// <summary>
///     Provides several string types, which are handled differently when reading or writing.
/// </summary>
public enum StringType
{
	/// <summary>
	///     Auto-detected string type.
	/// </summary>
	Auto,

	/// <summary>
	///     Auto-detected string type with an existing length in memory.
	/// </summary>
	AutoSized,

	/// <summary>
	///     UTF8-encoded string.
	/// </summary>
	UTF8,

	/// <summary>
	///     UTF8-encoded string with an existing length in memory.
	/// </summary>
	UTF8Sized,

	/// <summary>
	///     UTF16-encoded string.
	/// </summary>
	UTF16,

	/// <summary>
	///     UTF16-encoded string with an existing length in memory.
	/// </summary>
	UTF16Sized
}
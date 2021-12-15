using System.Diagnostics;

namespace MemoryToolKit.ProcessUtils;

/// <summary>
///     Provides a custom class to hold special information about a
///     <see cref="System.Diagnostics.ProcessModule"/> for architecture compatibility.
/// </summary>
public class ProcessModule
{
	/// <summary>
	///     The memory address where the module was loaded.
	/// </summary>
	public IntPtr BaseAddress { get; internal set; }

	/// <summary>
	///     The memory address for the function that runs when the system loads the module.
	/// </summary>
	public IntPtr EntryPointAddress { get; internal set; }

	/// <summary>
	///     The amount of memory that is required to load the module, in bytes.
	/// </summary>
	public int ModuleMemorySize { get; internal set; }

	/// <summary>
	///     The fully qualified file path to the module.
	/// </summary>
	public string FileName { get; internal set; }

	/// <summary>
	///     The name of the process module.
	/// </summary>
	public string ModuleName { get; internal set; }

	/// <summary>
	///     The module's version information.
	/// </summary>
	public FileVersionInfo VersionInfo
		=> FileVersionInfo.GetVersionInfo(FileName);

	/// <returns>
	///     The module's <see cref="ModuleName"/>.
	/// </returns>
	public override string ToString()
	{
		return ModuleName ?? base.ToString();
	}
}
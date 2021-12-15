using MemoryToolKit.MemoryUtils;
using System.Diagnostics;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.ProcessUtils;

public static partial class ProcessExtensions
{
	#region WriteBytes
	#region ModuleName
	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="moduleName">The name of the module to which the memory should be written, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, string moduleName, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="moduleName">The name of the module to which the memory should be written, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="moduleName">The name of the module to which the memory should be written, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, bool is64Bit, string moduleName, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Module
	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="module">The module to which the memory should be written.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && module is not null)
		{
			bool is64Bit = process.Is64Bit();
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="module">The module to which the memory should be written.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && module is not null)
		{
			bool is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="module">The module to which the memory should be written.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, bool is64Bit, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if ((!process?.HasExited ?? false) && module is not null)
			return process.WriteBytes(bytes, is64Bit, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Address
	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process to which the memory should be written.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="address">The address which to write or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, IntPtr address, params int[] offsets)
	{
		if (!process?.HasExited ?? false)
		{
			bool is64Bit = process.Is64Bit();
			return process.WriteBytes(bytes, is64Bit, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process to which the memory should be written.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="address">The address which to write or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, DerefType derefType, IntPtr address, params int[] offsets)
	{
		if (!process?.HasExited ?? false)
		{
			bool is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.WriteBytes(bytes, is64Bit, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Writes an array of bytes to a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process to which the memory should be written.</param>
	/// <param name="bytes">The array of bytes to write to memory.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="address">The address which to write or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool WriteBytes(this Process process, byte[] bytes, bool is64Bit, IntPtr address, params int[] offsets)
	{
		return (!process?.HasExited ?? false)
		       && process.TryDeref(is64Bit, out var derefAddress, address, offsets)
		       && derefAddress != IntPtr.Zero
		       && WinAPI.WriteProcessMemory(process.Handle, derefAddress, bytes, (SIZE_T)(bytes.Length), out _);
	}
	#endregion
	#endregion
}
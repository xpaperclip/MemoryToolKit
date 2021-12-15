using MemoryToolKit.MemoryUtils;
using System.Diagnostics;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.ProcessUtils;

public static partial class ProcessExtensions
{
	#region ReadBytes
	#region ModuleName
	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.ReadBytes(numberOfBytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return null;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.ReadBytes(numberOfBytes, module.BaseAddress + baseOffset, offsets);
		}

		return null;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, bool is64Bit, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
			return process.ReadBytes(numberOfBytes, module.BaseAddress + baseOffset, offsets);

		return null;
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.ReadBytes(numberOfBytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return null;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.ReadBytes(numberOfBytes, is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return null;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, bool is64Bit, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
			return process.ReadBytes(numberOfBytes, is64Bit, module.BaseAddress + baseOffset, offsets);

		return null;
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, IntPtr address, params int[] offsets)
	{
		var is64Bit = process.Is64Bit();
		return process.ReadBytes(numberOfBytes, is64Bit, address, offsets);
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, DerefType derefType, IntPtr address, params int[] offsets)
	{
		var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
		return process.ReadBytes(numberOfBytes, is64Bit, address, offsets);
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <returns>
	///     The array of bytes read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static byte[] ReadBytes(this Process process, int numberOfBytes, bool is64Bit, IntPtr address, params int[] offsets)
	{
		address = process.Deref(is64Bit, address, offsets);
		var bytes = new byte[numberOfBytes];

		if (address != IntPtr.Zero && WinAPI.ReadProcessMemory(process.Handle, address, bytes, (SIZE_T)(numberOfBytes), out _))
			return bytes;

		return null;
	}
	#endregion
	#endregion



	#region TryReadBytes
	#region ModuleName
	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, out byte[] result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, DerefType derefType, out byte[] result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, bool is64Bit, out byte[] result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, out byte[] result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, DerefType derefType, out byte[] result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, bool is64Bit, out byte[] result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, out byte[] result, IntPtr address, params int[] offsets)
	{
		result = null;

		if (!process?.HasExited ?? false)
		{
			var is64Bit = process.Is64Bit();
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, DerefType derefType, out byte[] result, IntPtr address, params int[] offsets)
	{
		result = null;

		if (!process?.HasExited ?? false)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryReadBytes(numberOfBytes, is64Bit, out result, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified number of bytes at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="numberOfBytes">The amount of bytes to be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The array of bytes read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadBytes(this Process process, int numberOfBytes, bool is64Bit, out byte[] result, IntPtr address, params int[] offsets)
	{
		result = new byte[numberOfBytes];

		if ((!process?.HasExited ?? false)
		    && process.TryDeref(is64Bit, out var derefAddress, address, offsets)
		    && derefAddress != IntPtr.Zero
		    && WinAPI.ReadProcessMemory(process.Handle, derefAddress, result, (SIZE_T)(numberOfBytes), out _))
		{
			return true;
		}

		result = null;
		return false;
	}
	#endregion
	#endregion
}
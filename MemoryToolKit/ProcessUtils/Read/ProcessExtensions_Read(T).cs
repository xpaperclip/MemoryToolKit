using MemoryToolKit.Helpers;
using MemoryToolKit.MemoryUtils;
using System.Diagnostics;

namespace MemoryToolKit.ProcessUtils;

public static partial class ProcessExtensions
{
	#region Read<T>
	#region ModuleName
	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return default(T);
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, DerefType derefType, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return default(T);
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, bool is64Bit, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (process.Module(moduleName) is ProcessModule module)
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);

		return default(T);
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return default(T);
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return default(T);
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, bool is64Bit, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		if (module is not null)
			return process.Read<T>(is64Bit, module.BaseAddress + baseOffset, offsets);

		return default(T);
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, IntPtr address, params int[] offsets) where T : unmanaged
	{
		var is64Bit = process.Is64Bit();
		return process.Read<T>(is64Bit, address, offsets);
	}

	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static T Read<T>(this Process process, DerefType derefType, IntPtr address, params int[] offsets) where T : unmanaged
	{
		var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
		return process.Read<T>(is64Bit, address, offsets);
	}

	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The <typeparamref name="T"/> value read from the process' memory if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static unsafe T Read<T>(this Process process, bool is64Bit, IntPtr address, params int[] offsets) where T : unmanaged
	{
		address = process.Deref(is64Bit, address, offsets);

		if (address != IntPtr.Zero)
		{
			var isPtr = typeof(T) == typeof(IntPtr) || typeof(T) == typeof(UIntPtr);
			var numberOfBytes = isPtr ? (is64Bit ? 0x8 : 0x4) : sizeof(T);

			return process.ReadBytes(numberOfBytes, address)?.To<T>() ?? default(T);
		}

		return default(T);
	}
	#endregion
	#endregion



	#region TryRead<T>
	#region ModuleName
	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, out T result, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, DerefType derefType, out T result, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, bool is64Bit, out T result, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, out T result, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, DerefType derefType, out T result, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, bool is64Bit, out T result, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryRead<T>(is64Bit, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, out T result, IntPtr address, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if (!process?.HasExited ?? false)
		{
			var is64Bit = process.Is64Bit();
			return process.TryRead<T>(is64Bit, out result, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryRead<T>(this Process process, DerefType derefType, out T result, IntPtr address, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if (!process?.HasExited ?? false)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryRead<T>(is64Bit, out result, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Reads a specified type at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="result">The <typeparamref name="T"/> value read from the process' memory if the function succeeds; otherwise, <see langword="default"/>(<typeparamref name="T"/>).</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static unsafe bool TryRead<T>(this Process process, bool is64Bit, out T result, IntPtr address, params int[] offsets) where T : unmanaged
	{
		result = default(T);

		if ((process?.HasExited ?? true) || !process.TryDeref(is64Bit, out var derefAddress, address, offsets) || derefAddress == IntPtr.Zero)
			return false;

		var isPtr = typeof(T) == typeof(IntPtr) || typeof(T) == typeof(UIntPtr);
		var numberOfBytes = isPtr ? (is64Bit ? 0x8 : 0x4) : sizeof(T);

		if (!process.TryReadBytes(numberOfBytes, out var bytes, derefAddress) || bytes is null)
			return false;

		result = bytes.To<T>();
		return true;
	}
	#endregion
	#endregion
}
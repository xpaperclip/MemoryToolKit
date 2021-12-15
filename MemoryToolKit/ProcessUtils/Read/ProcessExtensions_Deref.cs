using MemoryToolKit.MemoryUtils;
using System.Diagnostics;

namespace MemoryToolKit.ProcessUtils;

public static partial class ProcessExtensions
{
	#region Deref
	#region ModuleName
	/// <summary>
	///     Dereferences a pointer path.<br/>
	///     An <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return IntPtr.Zero;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return IntPtr.Zero;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, bool is64Bit, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is ProcessModule module)
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);

		return IntPtr.Zero;
	}
	#endregion

	#region Module
	/// <summary>
	///     Dereferences a pointer path.<br/>
	///     An <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return IntPtr.Zero;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);
		}

		return IntPtr.Zero;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, bool is64Bit, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is not null)
			return process.Deref(is64Bit, module.BaseAddress + baseOffset, offsets);

		return IntPtr.Zero;
	}
	#endregion

	#region Address
	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, IntPtr address, params int[] offsets)
	{
		var is64Bit = process.Is64Bit();
		return process.Deref(is64Bit, address, offsets);
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, DerefType derefType, IntPtr address, params int[] offsets)
	{
		var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
		return process.Deref(is64Bit, address, offsets);
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds;
	///     otherwise, <see cref="IntPtr.Zero"/>.
	/// </returns>
	public static IntPtr Deref(this Process process, bool is64Bit, IntPtr address, params int[] offsets)
	{
		if (offsets.Length == 0 || address == IntPtr.Zero)
			return address;

		address = process.Read<IntPtr>(is64Bit, address);

		foreach (var offset in offsets[..^1])
		{
			address = process.Read<IntPtr>(is64Bit, address + offset);

			if (address == IntPtr.Zero)
				return IntPtr.Zero;
		}

		return address += offsets[^1];
	}
	#endregion
	#endregion



	#region TryDeref
	#region ModuleName
	/// <summary>
	///     Dereferences a pointer path.<br/>
	///     An <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, out IntPtr derefAddress, string moduleName, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = process.Is64Bit();
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, DerefType derefType, out IntPtr derefAddress, string moduleName, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, bool is64Bit, out IntPtr derefAddress, string moduleName, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is ProcessModule module)
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Module
	/// <summary>
	///     Dereferences a pointer path.<br/>
	///     An <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, out IntPtr derefAddress, ProcessModule module, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = process.Is64Bit();
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, DerefType derefType, out IntPtr derefAddress, ProcessModule module, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && module is not null)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, bool is64Bit, out IntPtr derefAddress, ProcessModule module, int baseOffset, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryDeref(is64Bit, out derefAddress, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Address
	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, out IntPtr derefAddress, IntPtr address, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if (!process?.HasExited ?? false)
		{
			var is64Bit = process.Is64Bit();
			return process.TryDeref(is64Bit, out derefAddress, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="derefType">The dereferencing type of the pointer. Reads 4 bytes for <see cref="DerefType.Bit32"/> and 8 bytes for <see cref="DerefType.Bit64"/>.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, DerefType derefType, out IntPtr derefAddress, IntPtr address, params int[] offsets)
	{
		derefAddress = IntPtr.Zero;

		if (!process?.HasExited ?? false)
		{
			var is64Bit = derefType == DerefType.Auto ? process.Is64Bit() : derefType == DerefType.Bit64;
			return process.TryDeref(is64Bit, out derefAddress, address, offsets);
		}

		return false;
	}

	/// <summary>
	///     Dereferences a pointer path.
	///     This means an <see cref="IntPtr"/> is read at each offset, adding the offset to the previously read <see cref="IntPtr"/> at each step.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="is64Bit">The architecture type of the module or process.</param>
	/// <param name="derefAddress">The <see cref="IntPtr"/> from the final dereference plus the final offset if the function succeeds; otherwise, <see cref="IntPtr.Zero"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">The offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryDeref(this Process process, bool is64Bit, out IntPtr derefAddress, IntPtr address, params int[] offsets)
	{
		derefAddress = address;

		if (address == IntPtr.Zero)
			return false;

		if (offsets.Length == 0)
			return true;

		if ((process?.HasExited ?? true) || !process.TryRead<IntPtr>(is64Bit, out derefAddress, address))
			return false;

		foreach (var offset in offsets[..^1])
		{
			if (!process.TryRead<IntPtr>(is64Bit, out var tempResult, derefAddress + offset) || (derefAddress = tempResult) == IntPtr.Zero)
				return false;
		}

		derefAddress += offsets[^1];
		return true;
	}
	#endregion
	#endregion
}
using MemoryToolKit.MemoryUtils;
using System.Diagnostics;
using System.Text;
using SIZE_T = System.UIntPtr;

namespace MemoryToolKit.ProcessUtils;

public static partial class ProcessExtensions
{
	#region ReadString
	#region ModuleName
	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, string, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is var module)
			return process.ReadString(StringType.Auto, module.BaseAddress + baseOffset, offsets);

		return null;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     When the <paramref name="stringType"/> parameter is any of the <c>Sized</c> <see cref="StringType"/>s, will attempt to infer the length of the string automatically.<br/>
	///     Otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, StringType, string, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, StringType stringType, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is var module)
			return process.ReadString(stringType, module.BaseAddress + baseOffset, offsets);

		return null;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is var module)
			return process.ReadString(length, StringType.Auto, module.BaseAddress + baseOffset, offsets);

		return null;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, StringType stringType, string moduleName, int baseOffset, params int[] offsets)
	{
		if (process.Module(moduleName) is var module)
			return process.ReadString(length, stringType, module.BaseAddress + baseOffset, offsets);

		return null;
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, ProcessModule, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is null)
			return null;

		return process.ReadString(StringType.Auto, module.BaseAddress + baseOffset, offsets);
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, StringType, ProcessModule, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, StringType stringType, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is null)
			return null;

		return process.ReadString(stringType, module.BaseAddress + baseOffset, offsets);
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is null)
			return null;

		return process.ReadString(length, StringType.Auto, module.BaseAddress + baseOffset, offsets);
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, StringType stringType, ProcessModule module, int baseOffset, params int[] offsets)
	{
		if (module is null)
			return null;

		return process.ReadString(length, stringType, module.BaseAddress + baseOffset, offsets);
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, IntPtr, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, IntPtr address, params int[] offsets)
	{
		return process.ReadString(StringType.Auto, address, offsets);
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="ReadString(Process, int, StringType, IntPtr, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, StringType stringType, IntPtr address, params int[] offsets)
	{
		address = process.Deref(address, offsets);

		if (address == IntPtr.Zero)
			return null;

		return stringType switch
		{
			StringType.AutoSized or StringType.UTF8Sized or StringType.UTF16Sized => process.ReadString(process.Read<int>(address - 0x4), stringType, address),
			_ => process.ReadString(1024, stringType, address)
		};
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, IntPtr address, params int[] offsets)
	{
		return process.ReadString(length, StringType.Auto, address, offsets);
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     The string read from the process' memory if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static string ReadString(this Process process, int length, StringType stringType, IntPtr address, params int[] offsets)
	{
		if (length == 0)
			return string.Empty;

		var bytes = new byte[length];
		address = process.Deref(address, offsets);

		if (address == IntPtr.Zero || !WinAPI.ReadProcessMemory(process.Handle, address, bytes, (SIZE_T)(length), out var numBytesRead) || numBytesRead != (SIZE_T)(length))
			return null;

		return stringType switch
		{
			StringType.AutoSized => (int)(numBytesRead) == 1 || bytes[1] != 0 ? Encoding.UTF8.GetString(bytes) : Encoding.Unicode.GetString(bytes),
			StringType.UTF8Sized => Encoding.UTF8.GetString(bytes),
			StringType.UTF16Sized => Encoding.Unicode.GetString(bytes),
			_ => getUnsizedString()
		};

		string getUnsizedString()
		{
			var byteList = new List<byte>();
			
			(Encoding Type, bool IsUnicode, int CharSize) encoding = stringType switch
			{
				StringType.UTF8 or StringType.Auto when bytes[1] != 0 => (Encoding.UTF8, false, 1),
				_ => (Encoding.Unicode, true, 2)
			};

			length *= encoding.CharSize;

			for (int j = 0; j < length; j += encoding.CharSize)
			{
				if (bytes[j] == 0)
					return encoding.Type.GetString(byteList.ToArray());

				byteList.Add(bytes[j]);

				if (encoding.IsUnicode)
					byteList.Add(bytes[j + 1]);
			}

			return null;
		}
	}
	#endregion
	#endregion



	#region TryReadString
	#region ModuleName
	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, int, out string, string, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, out string result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is var module)
			return process.TryReadString(StringType.Auto, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, int, StringType, out string, string, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, StringType stringType, out string result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is var module)
			return process.TryReadString(stringType, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, out string result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is var module)
			return process.TryReadString(length, StringType.Auto, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="moduleName">The name of the module from which the memory should be read, including its file extension.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, StringType stringType, out string result, string moduleName, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.Module(moduleName) is var module)
			return process.TryReadString(length, stringType, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Module
	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, out string, ProcessModule, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, out string result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryReadString(StringType.Auto, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, StringType, out string, ProcessModule, int, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, StringType stringType, out string result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryReadString(stringType, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, out string result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryReadString(length, StringType.Auto, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified module's address or pointer.
	/// </summary>
	/// <param name="process">The process which contains the module.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="module">The module from which the memory should be read.</param>
	/// <param name="baseOffset">The offset from the module's base address.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, StringType stringType, out string result, ProcessModule module, int baseOffset, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && module is not null)
			return process.TryReadString(length, stringType, out result, module.BaseAddress + baseOffset, offsets);

		return false;
	}
	#endregion

	#region Address
	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, int, out string, IntPtr, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, out string result, IntPtr address, params int[] offsets)
	{
		result = null;

		if (!process?.HasExited ?? false)
			return process.TryReadString(StringType.Auto, out result, address, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <remarks>
	///     Unless explicitly specified otherwise, will read a maximum of 1024 characters or until a null-terminator is found.<br/>
	///     Use the <see cref="TryReadString(Process, int, StringType, out string, IntPtr, int[])"/> overload to specify a different length.
	/// </remarks>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, StringType stringType, out string result, IntPtr address, params int[] offsets)
	{
		result = null;

		if ((!process?.HasExited ?? false) && process.TryDeref(out var derefAddress, address, offsets) && derefAddress != IntPtr.Zero)
			return stringType switch
			{
				StringType.AutoSized or StringType.UTF8Sized or StringType.UTF16Sized => process.TryReadString(process.Read<int>(derefAddress - 0x4), stringType, out result, derefAddress),
				_ => process.TryReadString(1024, stringType, out result, derefAddress)
			};

		return false;
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, out string result, IntPtr address, params int[] offsets)
	{
		result = null;

		if (!process?.HasExited ?? false)
			return process.TryReadString(length, StringType.Auto, out result, address, offsets);

		return false;
	}

	/// <summary>
	///     Reads a string at a specified address or pointer.
	/// </summary>
	/// <param name="process">The process from which the memory should be read.</param>
	/// <param name="length">The maximum amount of characters to read.</param>
	/// <param name="stringType">The type of the string in memory.</param>
	/// <param name="result">The string read from the process' memory if the function succeeds; otherwise, <see langword="null"/>.</param>
	/// <param name="address">The address from which to read or start the pointer at.</param>
	/// <param name="offsets">An optional amount of offsets to dereference.</param>
	/// <returns>
	///     <see langword="true"/> if the function succeeds;
	///     otherwise, <see langword="false"/>.
	/// </returns>
	public static bool TryReadString(this Process process, int length, StringType stringType, out string result, IntPtr address, params int[] offsets)
	{
		result = null;

		if (length == 0)
		{
			result = string.Empty;
			return true;
		}

		var bytes = new byte[length];

		if ((process?.HasExited ?? true) || !process.TryDeref(out var derefAddress, address, offsets) || derefAddress == IntPtr.Zero)
			return false;

		if (!WinAPI.ReadProcessMemory(process.Handle, derefAddress, bytes, (SIZE_T)(length), out var numBytesRead) || numBytesRead != (SIZE_T)(length))
			return false;

		result = stringType switch
		{
			StringType.AutoSized => (int)(numBytesRead) == 1 || bytes[1] != 0 ? Encoding.UTF8.GetString(bytes) : Encoding.Unicode.GetString(bytes),
			StringType.UTF8Sized => Encoding.UTF8.GetString(bytes),
			StringType.UTF16Sized => Encoding.Unicode.GetString(bytes),
			_ => getUnsizedString()
		};

		return result is not null;

		string getUnsizedString()
		{
			var byteList = new List<byte>();

			(Encoding Type, bool IsUnicode, int CharSize) encoding = stringType switch
			{
				StringType.UTF8 or StringType.Auto when bytes[1] != 0 => (Encoding.UTF8, false, 1),
				_ => (Encoding.Unicode, true, 2)
			};

			length *= encoding.CharSize;

			for (int j = 0; j < length; j += encoding.CharSize)
			{
				if (bytes[j] == 0)
					return encoding.Type.GetString(byteList.ToArray());

				byteList.Add(bytes[j]);

				if (encoding.IsUnicode)
					byteList.Add(bytes[j + 1]);
			}

			return null;
		}
	}
	#endregion
	#endregion
}
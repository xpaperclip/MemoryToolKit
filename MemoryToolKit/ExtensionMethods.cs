namespace MemoryToolKit;

/// <summary>
///     Provides several useful extension methods for the user's convenience.
/// </summary>
public static class ExtensionMethods
{
	#region Conversions
	/// <summary>
	///     Converts an array of bytes into the specified type.
	/// </summary>
	/// <typeparam name="T">The type to convert to.</typeparam>
	/// <param name="bytes">The array of bytes to convert.</param>
	/// <returns>
	///     The converted type if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static unsafe T To<T>(this byte[] bytes) where T : unmanaged
	{
		if (bytes is null)
			return default(T);

		fixed (byte* bytesPtr = bytes)
			return *(T*)(bytesPtr);
	}

	/// <summary>
	///     Converts an array of bytes into the specified type.
	/// </summary>
	/// <typeparam name="T">The type to convert to.</typeparam>
	/// <param name="bytes">The array of bytes to convert.</param>
	/// <param name="offset">An offset to add to the byte array's address.</param>
	/// <returns>
	///     The converted type if the function succeeds;
	///     otherwise, <see langword="default"/>(<typeparamref name="T"/>).
	/// </returns>
	public static unsafe T To<T>(this byte[] bytes, int offset = 0x0) where T : unmanaged
	{
		if (bytes is null)
			return default(T);

		fixed (byte* bytesPtr = bytes)
			return *(T*)(bytesPtr + offset);
	}

	/// <summary>
	///     Converts a non-nullable value into an array of bytes.
	/// </summary>
	/// <typeparam name="T">The type to convert from.</typeparam>
	/// <param name="value">The value to convert.</param>
	public static unsafe byte[] ToBytes<T>(this T value) where T : unmanaged
	{
		var size = sizeof(T);
		var bytes = new byte[size];
		return value.ToBytes<T>(bytes, size);
	}

	/// <summary>
	///     Converts a non-nullable value into an array of bytes.
	/// </summary>
	/// <typeparam name="T">The type to convert from.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <param name="bytes">The array of bytes the conversion should be copied into.</param>
	/// <returns>
	///     The converted <see cref="byte"/>[] if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static unsafe byte[] ToBytes<T>(this T value, byte[] bytes) where T : unmanaged
	{
		var size = sizeof(T);
		return value.ToBytes<T>(bytes, size);
	}

	/// <summary>
	///     Converts a non-nullable value into an array of bytes.
	/// </summary>
	/// <typeparam name="T">The type to convert from.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <param name="size">The amount of bytes that should be copied into the array.</param>
	public static byte[] ToBytes<T>(this T value, int size) where T : unmanaged
	{
		var bytes = new byte[size];
		return value.ToBytes<T>(bytes, size);
	}

	/// <summary>
	///     Converts a non-nullable value into an array of bytes.
	/// </summary>
	/// <typeparam name="T">The type to convert from.</typeparam>
	/// <param name="value">The value to convert.</param>
	/// <param name="bytes">The array of bytes the conversion should be copied into.</param>
	/// <param name="size">The amount of bytes that should be copied into the array.</param>
	/// <returns>
	///     The converted <see cref="byte"/>[] if the function succeeds;
	///     otherwise, <see langword="null"/>.
	/// </returns>
	public static unsafe byte[] ToBytes<T>(this T value, byte[] bytes, int size) where T : unmanaged
	{
		if (bytes is null)
			return null;

		fixed (byte* bytesPtr = bytes)
			Buffer.MemoryCopy(&value, bytesPtr, bytes.Length, size);

		return bytes;
	}
	#endregion

	/// <summary>
	///     Returns a specified default value when the provided <see cref="string"/> is <see langword="null"/>.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="_default">The default value to return.</param>
	public static string Default(this string? value, string _default)
	{
		return value ?? _default;
	}
}
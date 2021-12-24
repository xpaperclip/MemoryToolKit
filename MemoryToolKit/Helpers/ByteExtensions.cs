namespace MemoryToolKit.Helpers;

internal enum ByteCompareType
{
	Any,
	Lower,
	Upper,
	Full
}

internal static class ByteExtensions
{
	public static byte GetUpper(this byte value)
	{
		return (byte)(value >> 4);
	}

	public static byte GetLower(this byte value)
	{
		return (byte)(value & 0xF);
	}

	public static bool CompareNibbles(this byte value, byte other, bool otherIsUpper = true)
	{
		var valueNibble = value.GetLower();
		var otherNibble = otherIsUpper ? other.GetUpper() : other.GetLower();

		return valueNibble == otherNibble;
	}

	public static bool Compare(this byte value, byte other, ByteCompareType compareType)
	{
		return compareType switch
		{
			ByteCompareType.Any => true,
			ByteCompareType.Lower => value.CompareNibbles(other, true),
			ByteCompareType.Upper => value.CompareNibbles(other, false),
			ByteCompareType.Full => value == other,
			_ => false
		};
	}
}
namespace MemoryToolKit.MemoryUtils.SigScan;

internal struct PatternByte
{
	public byte Value { get; set; }
	public ByteType Type { get; set; }
}

internal enum ByteType
{
	Any,
	Lower,
	Upper,
	Full
}
using System.Text;

namespace MemoryToolKit.IO;

public class BinaryReaderEndian : BinaryReader
{
	public BinaryReaderEndian(Stream stream, bool isBigEndian = false)
		: base(stream)
	{
		IsBigEndian = isBigEndian;
	}

	public BinaryReaderEndian(string path, bool isBigEndian = false)
		: this(File.OpenRead(path), isBigEndian) { }

	public bool IsBigEndian { get; private set; }

	/// <inheritdoc cref="BinaryReader.ReadByte"/>
	public override byte ReadByte()
	{
		return IsBigEndian ? (byte)ReadBytesBigEndian(1) : base.ReadByte();
	}

	/// <inheritdoc cref="BinaryReader.ReadSByte"/>
	public override sbyte ReadSByte()
	{
		return IsBigEndian ? (sbyte)ReadBytesBigEndian(1) : base.ReadSByte();
	}

	/// <inheritdoc cref="BinaryReader.ReadUInt16"/>
	public override ushort ReadUInt16()
	{
		return IsBigEndian ? (ushort)ReadBytesBigEndian(2) : base.ReadUInt16();
	}

	/// <inheritdoc cref="BinaryReader.ReadInt16"/>
	public override short ReadInt16()
	{
		return IsBigEndian ? (short)ReadBytesBigEndian(2) : base.ReadInt16();
	}

	/// <inheritdoc cref="BinaryReader.ReadUInt32"/>
	public override uint ReadUInt32()
	{
		return IsBigEndian ? (uint)ReadBytesBigEndian(4) : base.ReadUInt32();
	}

	/// <inheritdoc cref="BinaryReader.ReadInt32"/>
	public override int ReadInt32()
	{
		return IsBigEndian ? (int)ReadBytesBigEndian(4) : base.ReadInt32();
	}

	/// <inheritdoc cref="BinaryReader.ReadUInt64"/>
	public override ulong ReadUInt64()
	{
		return IsBigEndian ? (ulong)ReadBytesBigEndian(8) : base.ReadUInt64();
	}

	/// <inheritdoc cref="BinaryReader.ReadInt64"/>
	public override long ReadInt64()
	{
		return IsBigEndian ? (long)ReadBytesBigEndian(8) : base.ReadInt64();
	}

	/// <inheritdoc cref="BinaryReader.ReadString"/>
	public override string ReadString()
	{
		if (!IsBigEndian)
			return base.ReadString();

		var bytes = new List<byte>();

		while (BaseStream.Position != BaseStream.Length && bytes.Count < 2048)
		{
			var b = base.ReadByte();
			if (b == 0)
				break;

			bytes.Add(b);
		}

		return Encoding.UTF8.GetString(bytes.ToArray());
	}

	public ulong ReadBytesBigEndian(int count)
	{
		var bytes = base.ReadBytes(count);
		ulong result = bytes[^1];

		for (var i = count - 2; i >= 0; --i)
			result |= (uint)(bytes[i]) << (count - 1 - i) * 8;

		return result;
	}
}
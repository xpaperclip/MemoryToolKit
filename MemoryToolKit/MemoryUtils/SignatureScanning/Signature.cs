using System.Globalization;
using System.Text.RegularExpressions;

namespace MemoryToolKit.MemoryUtils.SigScan;

public struct Signature
{
	public Signature(int offset, params string[] pattern)
	{
		Name = null;
		VerifyMatch = null;
		Offset = offset;

		var sig = string.Concat(pattern).Replace(" ", "");
		var length = sig.Length;

		if (length % 2 != 0)
			throw new ArgumentException("Signature was in an incorrect format! Make sure that all bytes are fully specified.", nameof(pattern));


		var bytes = Regex.Matches(sig, "..").Select(match => match.Value).ToArray();
		Bytes = new PatternByte[bytes.Length];

		for (int i = 0; i < bytes.Length; ++i)
		{
			var currByte = bytes[i];

			if (currByte == "??")
			{
				Bytes[i] = new() { Value = 0, Type = ByteType.Any };
			}
			else
			{
				Bytes[i] = currByte.IndexOf('?') switch
				{
					0 => new() { Value = byte.Parse(currByte[1].ToString(), NumberStyles.HexNumber), Type = ByteType.Lower },
					1 => new() { Value = byte.Parse(currByte[0].ToString(), NumberStyles.HexNumber), Type = ByteType.Upper },
					_ => new() { Value = byte.Parse(currByte, NumberStyles.HexNumber), Type = ByteType.Full }
				};
			}
		}
	}

	public Signature(params string[] pattern)
		: this(0, pattern) { }

	public Signature(int offset, params byte[] pattern)
	{
		Name = null;
		VerifyMatch = null;
		Offset = offset;

		Bytes = pattern.Select(b => new PatternByte { Value = b, Type = ByteType.Full }).ToArray();
	}

	public Signature(params byte[] pattern)
		: this(0, pattern) { }

	public string Name { get; private set; }
	public Func<IntPtr, bool> VerifyMatch { internal get; set; }

	internal PatternByte[] Bytes;
	internal int Offset;

	public int Length
	{
		get => Bytes?.Length ?? 0;
	}
}
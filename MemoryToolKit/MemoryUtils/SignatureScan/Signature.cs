using MemoryToolKit.Helpers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MemoryToolKit.MemoryUtils.SigScan;

public struct Signature
{
	public Signature(int offset, params string[] pattern)
	{
		var sig = string.Concat(pattern).Replace(" ", "");
		var length = sig.Length;
		if (length % 2 != 0)
			throw new ArgumentException("Signature was in an incorrect format! Make sure that all bytes are fully specified.", nameof(pattern));

		Name = null;
		EvaluateMatch = null;
		Offset = offset;

		var bytes = new List<byte>();
		var bools = new List<bool>();
		var hasMask = false;

		for (int i = 0; i < length; i += 2)
		{
			if (byte.TryParse(sig[i..(i + 2)], NumberStyles.HexNumber, null, out var b))
			{
				bytes.Add(b);
				bools.Add(false);
			}
			else
			{
				bytes.Add(0);
				bools.Add(true);
				hasMask = true;
			}
		}

		Pattern = bytes.ToArray();
		Mask = hasMask ? bools.ToArray() : null;
	}

	public Signature(params string[] pattern)
		: this(0, pattern) { }

	public Signature(int offset, params byte[] pattern)
	{
		Name = null;
		EvaluateMatch = null;
		Offset = offset;

		Pattern = pattern;
		Mask = null;
	}

	public Signature(params byte[] pattern)
		: this(0, pattern) { }

	public string Name { get; set; }
	public Func<IntPtr, bool> EvaluateMatch { internal get; set; }

	internal byte[] Pattern;
	internal bool[] Mask;
	internal int Offset;

	public int Length
	{
		get => Pattern?.Length ?? 0;
	}
}
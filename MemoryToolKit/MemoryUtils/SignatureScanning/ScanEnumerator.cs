using System.Collections;

namespace MemoryToolKit.MemoryUtils.SigScan;

public partial class SignatureScanner
{
	private class ScanEnumerator : IEnumerator<int>, IEnumerable<int>
	{
		public ScanEnumerator(byte[] memory, int alignment, Signature signature)
		{
			if ((memory?.Length ?? 0) < signature.Length)
				throw new ArgumentOutOfRangeException(nameof(memory), "Memory buffer length must be greater or equal to the pattern's length.");

			_memory = memory;
			_alignment = alignment;
			_signature = signature;
			_length = signature.Length;
			_end = memory.Length - signature.Length;
		}

		private readonly byte[] _memory;
		private readonly int _alignment;
		private readonly Signature _signature;
		private readonly int _length;
		private readonly int _end;
		private int _next;

		private unsafe bool NextPattern()
		{
			fixed (byte* memory = _memory)
			fixed (PatternByte* pattern = _signature.Bytes)
			{
				int alignment = _alignment;
				int length = _length;
				int end = _end;
				int next = _next;

				next:

				if (next >= end)
					return false;

				for (int i = 0; i < length; ++i)
				{
					var pByte = pattern[i];
					var mByte = memory[next + i];

					switch (pByte.Type)
					{
						case ByteType.Any:
							break;
						case ByteType.Lower when pByte.Value != (mByte & 0xF):
						case ByteType.Upper when pByte.Value != (mByte >> 4):
						case ByteType.Full when pByte.Value != mByte:
							next += alignment;
							goto next;
					}
				}

				Current = next;
				_next = next + alignment;
				return true;
			}
		}

		public bool MoveNext()
		{
			return NextPattern();
		}

		public void Reset()
		{
			_next = 0;
		}

		public int Current { get; private set; }

		int IEnumerator<int>.Current => Current;

		object IEnumerator.Current => Current;

		public void Dispose() { }

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return this;
		}

		public IEnumerator GetEnumerator()
		{
			return this;
		}
	}
}
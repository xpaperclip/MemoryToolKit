using MemoryToolKit.Helpers;
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
			fixed (byte* memory = _memory, pattern = _signature.Pattern)
			fixed (bool* mask = _signature.Mask)
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
					if (!mask[i] && memory[next + i] != pattern[i])
					{
						next += alignment;
						goto next;
					}
				}

				Current = next;
				_next = next + alignment;
				return true;
			}
		}

		private unsafe bool NextBytes()
		{
			fixed (byte* memory = _memory, pattern = _signature.Pattern)
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
					if (memory[next + i] != pattern[i])
					{
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
			return _signature.Mask is null ? NextBytes() : NextPattern();
		}

		public void Reset()
		{
			_next = 0;
		}

		public int Current { get; private set; }

		int IEnumerator<int>.Current => Current;

		object IEnumerator.Current => Current;

		public void Dispose() { }

		public IEnumerator GetEnumerator()
		{
			return this;
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return this;
		}
	}
}
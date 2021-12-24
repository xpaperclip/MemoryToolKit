using MemoryToolKit.ProcessUtils;
using System.Diagnostics;

namespace MemoryToolKit.MemoryUtils.SigScan;

public partial class SignatureScanner
{
	public SignatureScanner(Process process, IntPtr startAddress, int size)
	{
		_process = process;
		_startAddress = startAddress;
		_size = size;
		_memory = null;
	}

	public SignatureScanner(Process process, IntPtr startAddress, IntPtr end)
	{
		_process = process;
		_startAddress = startAddress;
		_size = (int)((long)(end) - (long)(startAddress));
		_memory = null;
	}

	public SignatureScanner(byte[] memory)
	{
		_memory = memory;
		_size = memory.Length;
	}

	private readonly Process _process;
	private readonly IntPtr _startAddress;
	private readonly int _size;
	private byte[] _memory;

	public IntPtr Scan(ScanTarget target, int alignment = 1)
	{
		return ScanAll(target, alignment).FirstOrDefault();
	}

	public IEnumerable<IntPtr> ScanAll(ScanTarget target, int alignment = 1)
	{
		if ((long)(_startAddress) % alignment != 0)
			throw new ArgumentOutOfRangeException(nameof(alignment), "Start address must be aligned.");

		if (!UpdateMemory())
			yield break;

		foreach (var signature in target)
		{
			foreach (int offset in new ScanEnumerator(_memory, alignment, signature))
			{
				var scan = _startAddress + offset + signature.Offset;
				if (signature.VerifyMatch?.Invoke(scan) ?? true)
				{
					target.OnFound?.Invoke(signature.Name, scan);
					yield return scan;
				}
			}
		}
	}

	private bool UpdateMemory()
	{
		if (_memory is null || _memory.Length != _size)
			_memory = _process.ReadBytes(_size, _startAddress);

		return _memory is not null;
	}
}
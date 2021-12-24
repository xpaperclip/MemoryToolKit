using System.Collections;

namespace MemoryToolKit.MemoryUtils.SigScan;

public partial class ScanTarget : IReadOnlyCollection<Signature>
{
	public int Count => _signatures.Count;

	public IEnumerator<Signature> GetEnumerator()
	{
		return _signatures.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _signatures.GetEnumerator();
	}
}
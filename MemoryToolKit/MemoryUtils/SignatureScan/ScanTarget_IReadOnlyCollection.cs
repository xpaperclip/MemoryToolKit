using System.Collections;

namespace MemoryToolKit.MemoryUtils.SigScan;

public partial class ScanTarget : IReadOnlyCollection<Signature>
{
	public int Count
		=> _sigList.Count;

	public IEnumerator<Signature> GetEnumerator()
	{
		return _sigList.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _sigList.GetEnumerator();
	}
}
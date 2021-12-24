namespace MemoryToolKit.MemoryUtils.SigScan;

public partial class ScanTarget
{
	public ScanTarget(IEnumerable<Signature> signatures)
	{
		foreach (var signature in signatures)
			this.Add(signature);
	}

	public ScanTarget() { }

	public bool DoScan { get; set; } = true;

	public delegate IntPtr OnFoundCallBack(string name, IntPtr address);
	public OnFoundCallBack OnFound { get; set; }

	private readonly List<Signature> _sigList = new();

	public void Add(Signature signature)
	{
		_sigList.Add(signature);
	}
}
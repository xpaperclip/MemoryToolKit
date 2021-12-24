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

	public delegate IntPtr OnFoundCallback(string name, IntPtr address);
	public OnFoundCallback OnFound { get; set; }

	private readonly List<Signature> _signatures = new();

	public void Add(Signature signature)
	{
		_signatures.Add(signature);
	}
}
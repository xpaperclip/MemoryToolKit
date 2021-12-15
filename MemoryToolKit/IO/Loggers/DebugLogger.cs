using System.Diagnostics;

namespace MemoryToolKit.IO;

public class DebugLogger : Logger
{
	public override void Log()
	{
		Debug.WriteLine("");
	}

	public override void Log(object output)
	{
		Debug.WriteLine($"{DateTime.Now:HH:mm:ss.fff} | {output}");
	}

	public override void Start()
	{
		throw new NotImplementedException();
	}

	public override void Stop()
	{
		throw new NotImplementedException();
	}
}
using System.Runtime.InteropServices;

namespace MemoryToolKit.IO;

public class ConsoleLogger : Logger
{
	public override void Log()
	{
		Console.WriteLine();
	}

	public override void Log(object output)
	{
		Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} | {output}");
	}

	public override void Start()
	{
		WinAPI.AllocConsole();
		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
	}

	public override void Stop()
	{
		WinAPI.FreeConsole();
	}
}

internal class WinAPI
{
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool AllocConsole();

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool FreeConsole();
}
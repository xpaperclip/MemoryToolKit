using MemoryToolKit.Helpers;
using MemoryToolKit.IO;
using MemoryToolKit.ProcessUtils;
using System.Diagnostics;
using ProcessModule = MemoryToolKit.ProcessUtils.ProcessModule;

namespace MemoryToolKit.MemoryUtils.SigScan;

public class ScanData : Dictionary<string, Dictionary<string, ScanTarget>> { }
public class ScanResults : Dictionary<string, Dictionary<string, IntPtr>> { }

public class ScanTask : TaskRunner
{
	public ScanTask(Process process, Logger logger = null)
	{
		_process = process;
	}

	public bool ScanAllPages;
	private readonly Process _process;

	public void Run(ScanData scanData, Action<ScanResults> action = null)
	{
		this.Run(() =>
		{
			var results = ScanMemory(scanData);
			action?.Invoke(results);
		});
	}

	public ScanResults ScanMemory(ScanData scanData)
	{
		Log("Scanning memory.");

		var token = this._cancelSource.Token;
		var results = new ScanResults();
		var allFound = false;

		foreach (var moduleTargets in scanData)
		{
			results[moduleTargets.Key] = new();

			foreach (var target in moduleTargets.Value)
			{
				results[moduleTargets.Key][target.Key] = IntPtr.Zero;
			}
		}

		while (true)
		{
			token.ThrowIfCancellationRequested();

			foreach (var moduleTargets in scanData)
			{
				token.ThrowIfCancellationRequested();

				if (string.IsNullOrEmpty(moduleTargets.Key))
				{
					foreach (var page in _process.MemoryPages(ScanAllPages))
					{
						token.ThrowIfCancellationRequested();

						searchAllSignatures(moduleTargets, new(_process, page.BaseAddress, (int)(page.RegionSize)));

						if (allFound = allSignaturesFound())
							break;
					}
				}
				else
				{
					if (_process.Module(moduleTargets.Key) is ProcessModule module)
					{
						searchAllSignatures(moduleTargets, new(_process, module.BaseAddress, module.ModuleMemorySize));

						if (allFound = allSignaturesFound())
							break;
					}
				}
			}

			if (allFound)
				break;

			ScanTask.Sleep(MillisecondsTimeout);
		}

		Log("Scan completed!");
		return results;

		bool allSignaturesFound()
		{
			foreach (var moduleTargets in scanData)
			{
				foreach (var target in moduleTargets.Value)
				{
					if (target.Value.DoScan && results[moduleTargets.Key][target.Key] == IntPtr.Zero)
						return false;
				}
			}

			return true;
		}

		void searchAllSignatures(KeyValuePair<string, Dictionary<string, ScanTarget>> moduleTargets, SignatureScanner scanner)
		{
			foreach (var target in moduleTargets.Value)
			{
				token.ThrowIfCancellationRequested();

				if (target.Value.DoScan && results[moduleTargets.Key][target.Key] == IntPtr.Zero)
				{
					var scan = scanner.Scan(target.Value);
					if (scan != IntPtr.Zero)
					{
						Log($"Found target '{target.Key}' at 0x{scan:X}.");
						results[moduleTargets.Key][target.Key] = scan;
					}
				}
			}
		}
	}

	protected override void Log(object output)
	{
		_logger?.Log($"[Scan Task] {output}");
	}
}
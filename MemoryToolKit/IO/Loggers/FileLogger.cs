using System.Diagnostics;

namespace MemoryToolKit.IO;

public class FileLogger : Logger
{
	public FileLogger(string filePath)
	{
		FilePath = filePath;
	}

	private int LineNumber;
	private readonly string FilePath;
	private readonly Queue<string> QueuedLines = new();
	private readonly CancellationTokenSource CancelSource = new();
	private readonly ManualResetEvent ResetEvent = new(false);

	public int MaximumLinesInFile { get; set; } = 4096;
	public int LinesAfterFlush { get; set; } = 512;

	public override void Log()
	{
		lock (QueuedLines)
		{
			QueuedLines.Enqueue("");
			ResetEvent.Set();
		}
	}

	public override void Log(object output)
	{
		lock (QueuedLines)
		{
			QueuedLines.Enqueue($"{DateTime.Now:HH:mm:ss:fff} | {output}");
			ResetEvent.Set();
		}
	}

	public override void Start()
	{
		var token = CancelSource.Token;

		Task.Run(() =>
		{
			if (!File.Exists(FilePath))
			{
				File.Create(FilePath);
				LineNumber = 0;
			}
			else
			{
				LineNumber = File.ReadAllLines(FilePath).Length;
			}

			string output = null;

			while (true)
			{
				ResetEvent.WaitOne();

				lock (QueuedLines)
				{
					if (!QueuedLines.Any())
					{
						ResetEvent.Reset();
						continue;
					}

					output = QueuedLines.Dequeue();
				}

				WriteLine(output);
				output = null;
			}
		}, token);
	}

	public override void Stop()
	{
		CancelSource.Cancel();
		ResetEvent.Set();
	}

	private void WriteLine(string output)
	{
		if (LineNumber >= MaximumLinesInFile)
		{
			string tempFile = $"{FilePath}-temp";
			var lines = File.ReadAllLines(FilePath)[^LinesAfterFlush..];

			File.WriteAllLines(tempFile, lines);

			try
			{
				File.Copy(tempFile, FilePath, true);
				LineNumber = lines.Length;
			}
			catch
			{
				Trace.TraceError("Failed replacing log file with temporary log file.");
			}
			finally
			{
				try
				{
					File.Delete(tempFile);
				}
				catch
				{
					Trace.TraceError("Failed deleting temporary log file.");
				}
			}
		}

		try
		{
			using StreamWriter streamWriter = new(FilePath, true);

			streamWriter.WriteLine(output);
			++LineNumber;
		}
		catch (Exception ex)
		{
			Trace.TraceError($"{ex}");
		}
	}
}
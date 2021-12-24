using System.Diagnostics;

namespace MemoryToolKit.IO;

public class FileLogger : Logger
{
	public FileLogger(string filePath)
	{
		_filePath = filePath;
	}

	private int _lineNumber;
	private readonly string _filePath;
	private readonly Queue<string> _queuedLines = new();
	private readonly CancellationTokenSource _cancelSource = new();
	private readonly ManualResetEvent _resetEvent = new(false);

	public int MaximumLinesInFile { get; set; } = 4096;
	public int LinesAfterFlush { get; set; } = 512;

	public override void Log()
	{
		lock (_queuedLines)
		{
			_queuedLines.Enqueue("");
			_resetEvent.Set();
		}
	}

	public override void Log(object output)
	{
		lock (_queuedLines)
		{
			_queuedLines.Enqueue($"{DateTime.Now:HH:mm:ss:fff} | {output}");
			_resetEvent.Set();
		}
	}

	public override void Start()
	{
		var token = _cancelSource.Token;

		Task.Run(() =>
		{
			if (!File.Exists(_filePath))
			{
				File.Create(_filePath);
				_lineNumber = 0;
			}
			else
			{
				_lineNumber = File.ReadAllLines(_filePath).Length;
			}

			string output = null;

			while (true)
			{
				_resetEvent.WaitOne();

				lock (_queuedLines)
				{
					if (!_queuedLines.Any())
					{
						_resetEvent.Reset();
						continue;
					}

					output = _queuedLines.Dequeue();
				}

				WriteLine(output);
				output = null;
			}
		}, token);
	}

	public override void Stop()
	{
		_cancelSource.Cancel();
		_resetEvent.Set();
	}

	private void WriteLine(string output)
	{
		if (_lineNumber >= MaximumLinesInFile)
		{
			string tempFile = $"{_filePath}-temp";
			var lines = File.ReadAllLines(_filePath)[^LinesAfterFlush..];

			File.WriteAllLines(tempFile, lines);

			try
			{
				File.Copy(tempFile, _filePath, true);
				_lineNumber = lines.Length;
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
			using StreamWriter streamWriter = new(_filePath, true);

			streamWriter.WriteLine(output);
			++_lineNumber;
		}
		catch (Exception ex)
		{
			Trace.TraceError($"{ex}");
		}
	}
}
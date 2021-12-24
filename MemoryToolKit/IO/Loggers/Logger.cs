using System.Diagnostics;

namespace MemoryToolKit.IO;

public abstract class Logger
{
	protected readonly Dictionary<string, Stopwatch> _stopwatches = new();
	protected readonly Dictionary<string, List<double>> _averages = new();

	public abstract void Log();
	public abstract void Log(object output);
	public abstract void Start();
	public abstract void Stop();

	public void Benchmark(string id, Action action)
	{
		StartBenchmark(id);
		action?.Invoke();
		StopBenchmark(id);
	}

	public void AvgBenchmark(string id, Action action)
	{
		StartAvgBenchmark(id);
		action?.Invoke();
		StopAvgBenchmark(id);
	}

	public void StartBenchmark(string id)
	{
		_stopwatches[id] = Stopwatch.StartNew();
	}

	public void StopBenchmark(string id, string prefix = "")
	{
		_stopwatches[id].Stop();
		Log($"{prefix}Benchmark for [{id}]: {_stopwatches[id].Elapsed}");
	}

	public void StartAvgBenchmark(string id)
	{
		_stopwatches[id] = Stopwatch.StartNew();

		if (!_averages.ContainsKey(id))
			_averages[id] = new();
	}

	public void StopAvgBenchmark(string id, string prefix = "")
	{
		_stopwatches[id].Stop();

		_averages[id].Add(_stopwatches[id].Elapsed.TotalSeconds);
		Log($"{prefix}Benchmark for [{id}]: {_stopwatches[id].Elapsed} | Average: {_averages[id].Sum() / _averages[id].Count:0.0000000}");
	}
}
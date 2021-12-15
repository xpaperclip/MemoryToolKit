﻿using System.Diagnostics;

namespace MemoryToolKit.IO;

public abstract class Logger
{
	private readonly Dictionary<string, Stopwatch> Stopwatches = new();
	private readonly Dictionary<string, List<double>> Averages = new();

	public abstract void Log();
	public abstract void Log(object output);
	public abstract void Start();
	public abstract void Stop();

	public void StartBenchmark(string id)
	{
		Stopwatches[id] = Stopwatch.StartNew();
	}

	public void StopBenchmark(string id, string prefix = "")
	{
		Stopwatches[id].Stop();
		Log($"{prefix}Benchmark for [{id}]: {Stopwatches[id].Elapsed}");
	}

	public void StartAvgBenchmark(string id)
	{
		Stopwatches[id] = Stopwatch.StartNew();

		if (!Averages.ContainsKey(id))
			Averages[id] = new();
	}

	public void StopAvgBenchmark(string id, string prefix = "")
	{
		Stopwatches[id].Stop();

		Averages[id].Add(Stopwatches[id].Elapsed.TotalSeconds);
		Log($"{prefix}Benchmark for [{id}]: {Stopwatches[id].Elapsed} | Average: {Averages[id].Sum() / Averages[id].Count:0.0000000}");
	}
}
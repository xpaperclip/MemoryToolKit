using MemoryToolKit.IO;

namespace MemoryToolKit.Helpers;

public class TaskRunner : IDisposable
{
	public TaskRunner(Logger logger = null)
	{
		_logger = logger;
	}

	protected Task _runTask;
	protected CancellationTokenSource _cancelSource = new();
	protected readonly Logger _logger;

	public bool IsCompleted
	{
		get => _runTask?.IsCompletedSuccessfully ?? true;
	}

	public int MillisecondsTimeout { get; set; } = 50;

	public void Run(Action action)
	{
		Cancel();

		var token = _cancelSource.Token;
		_runTask = Task.Run(() =>
		{
			try
			{
				action?.Invoke();
			}
			catch (Exception ex)
			{
				Log($"Task aborted due to error:{Environment.NewLine}{ex}");
			}
		}, token);
	}

	public void RunRetry(Action action, Func<bool> condition)
	{
		Cancel();

		var token = _cancelSource.Token;
		_runTask = Task.Run(() =>
		{
			try
			{
				while (true)
				{
					action?.Invoke();

					if (condition())
						break;

					Sleep(MillisecondsTimeout);
				}
			}
			catch (Exception ex)
			{
				Log($"Task aborted due to error:{Environment.NewLine}{ex}");
			}
		}, token);
	}

	public virtual void Cancel()
	{
		if (!IsCompleted)
		{
			_cancelSource.Cancel();
			_runTask.Wait();
		}

		_cancelSource = new();
	}

	public static void Sleep(int millisecondsTimeout)
	{
		Thread.Sleep(millisecondsTimeout);
	}

	public async Task WaitForCompletionAsync()
	{
		await Task.Run(async () =>
		{
			while (!IsCompleted)
				await Task.Delay(MillisecondsTimeout);
		});
	}

	protected virtual void Log(object output)
	{
		_logger?.Log($"[Task] {output}");
	}

	public void Dispose()
	{
		Cancel();
	}
}
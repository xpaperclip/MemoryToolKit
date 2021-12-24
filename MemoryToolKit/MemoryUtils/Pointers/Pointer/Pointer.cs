using MemoryToolKit.ProcessUtils;
using System.Diagnostics;

namespace MemoryToolKit.MemoryUtils.Pointers;

public abstract partial class Pointer
{
	#region Constructors
	internal Pointer(Process process, DerefType derefType, IntPtr baseAddress, params int[] offsets)
	{
		_process = process;
		DerefType = derefType;
		Offsets = offsets;
		Base = baseAddress;
	}

	internal Pointer(DerefType derefType, Pointer parent, params int[] offsets)
		: this(parent._process, derefType, parent.Base, parent.Offsets.Concat(offsets).ToArray())
	{
		Name = parent.Name;
	}
	#endregion

	#region Fields
	/// <summary>
	///     The dereferencing type of the pointer.
	/// </summary>
	public DerefType DerefType;

	internal readonly Process _process;
	internal DateTime? _lastUpdate;
	internal object _current;
	internal object _old;
	#endregion

	#region Properties
	/// <summary>
	///     The name of the pointer.
	/// </summary>
	/// <value>Acts as an identifier for the pointer.</value>
	public string Name { get; set; }

	/// <summary>
	///     The base address of the pointer.
	/// </summary>
	/// <value>Calculated by the pointer's module's BaseAddress plus the base offset.</value>
	public IntPtr Base { get; }

	/// <summary>
	///     The offsets of the pointer.
	/// </summary>
	public int[] Offsets { get; }

	/// <summary>
	///     The optional update interval of the pointer.
	/// </summary>
	/// <value>When this property is not <see langword="null"/>, <see cref="Current"/> will only be updated when the specified amount of milliseconds has passed.</value>
	public uint? UpdateInterval { get; set; } = null;

	/// <summary>
	///     Specifies whether <see cref="Current"/> should be updated even when the pointer's address is IntPtr.Zero.
	/// </summary>
	public bool UpdateOnNullPointer { get; set; } = true;

	public object Current
	{
		get
		{
			Update();
			return _current;
		}
		set
		{
			_current = value;
		}
	}

	public object Old
	{
		get
		{
			Update();
			return _old;
		}
		set
		{
			_old = value;
		}
	}

	public bool Changed
	{
		get => !Old.Equals(_current);
	}
	#endregion

	#region Abstract
	public abstract void Reset();
	protected abstract bool Internal_Update();
	#endregion

	#region Methods
	public bool Update()
	{
		if (UpdateInterval is null)
			return Internal_Update();

		var dtNow = DateTime.Now;

		if (_lastUpdate is null || (dtNow - _lastUpdate.Value).TotalMilliseconds > UpdateInterval.Value)
		{
			_lastUpdate = dtNow;
			return Internal_Update();
		}

		return false;
	}

	public IntPtr Deref()
	{
		if (_process.TryDeref(DerefType, out var derefAddress, Base, Offsets))
			return derefAddress;

		return IntPtr.Zero;
	}

	public void ForceUpdate(bool ignoreTicks = false)
	{
		if (ignoreTicks) Internal_Update();
		else _ = Current;
	}

	public override string ToString()
	{
		return Offsets.Length > 0 ? $"[{string.Join(", ", Offsets.Select(o => $"0x{o:X}"))}]" : "";
	}
	#endregion
}
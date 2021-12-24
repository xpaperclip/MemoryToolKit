using MemoryToolKit.ProcessUtils;
using System.Diagnostics;

namespace MemoryToolKit.MemoryUtils.Pointers;

public class Pointer<T> : Pointer where T : unmanaged
{
	#region Constructors
	public Pointer(Process process, DerefType derefType, IntPtr baseAddress, params int[] offsets)
		: base(process, derefType, baseAddress, offsets) { }

	public Pointer(Process process, IntPtr baseAddress, params int[] offsets)
		: this(process, DerefType.Auto, baseAddress, offsets) { }

	public Pointer(Process process, DerefType derefType, ProcessUtils.ProcessModule module, int baseOffset, params int[] offsets)
		: this(process, derefType, module.BaseAddress + baseOffset, offsets) { }

	public Pointer(Process process, ProcessUtils.ProcessModule module, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, module.BaseAddress + baseOffset, offsets) { }

	public Pointer(Process process, DerefType derefType, int baseOffset, params int[] offsets)
		: this(process, derefType, process.Modules().First(), baseOffset, offsets) { }

	public Pointer(Process process, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, process.Modules().First(), baseOffset, offsets) { }

	public Pointer(Process process, DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
		: this(process, derefType, process.Module(moduleName), baseOffset, offsets) { }

	public Pointer(Process process, string moduleName, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, process.Module(moduleName), baseOffset, offsets) { }

	public Pointer(DerefType derefType, Pointer parent, params int[] offsets)
		: base(derefType, parent, offsets) { }

	public Pointer(Pointer parent, params int[] offsets)
		: this(parent.DerefType, parent, offsets) { }
	#endregion

	#region Properties
	public new T Current
	{
		get => (T)(base.Current);
		set => base._current = value;
	}

	public new T Old
	{
		get => (T)(base.Old);
		set => base._old = value;
	}

	public delegate void DataChangedEventHandler(T old, T current);
	public event DataChangedEventHandler OnChanged;
	#endregion

	#region Methods
	public bool Write(T value)
	{
		return _process.Write<T>(value, DerefType, Base, Offsets);
	}

	public override void Reset()
	{
		base.Current = default(T);
		base.Old = default(T);
		_lastUpdate = null;
	}

	protected override bool Internal_Update()
	{
		if (_process.TryRead<T>(DerefType, out var result, Base, Offsets) || UpdateOnNullPointer)
		{
			_old = (T)(_current ?? default(T));
			_current = result;
			OnChanged?.Invoke((T)(_old), (T)(_current));
			return true;
		}

		return false;
	}
	#endregion
}
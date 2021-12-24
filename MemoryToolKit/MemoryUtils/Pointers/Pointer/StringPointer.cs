using MemoryToolKit.ProcessUtils;
using System.Diagnostics;

namespace MemoryToolKit.MemoryUtils.Pointers;

public class StringPointer : Pointer
{
	#region Constructors
	public StringPointer(Process process, DerefType derefType, IntPtr baseAddress, params int[] offsets)
		: base(process, derefType, baseAddress, offsets) { }

	public StringPointer(Process process, IntPtr baseAddress, params int[] offsets)
		: this(process, DerefType.Auto, baseAddress, offsets) { }

	public StringPointer(Process process, DerefType derefType, ProcessUtils.ProcessModule module, int baseOffset, params int[] offsets)
		: this(process, derefType, module.BaseAddress + baseOffset, offsets) { }

	public StringPointer(Process process, ProcessUtils.ProcessModule module, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, module.BaseAddress + baseOffset, offsets) { }

	public StringPointer(Process process, DerefType derefType, int baseOffset, params int[] offsets)
		: this(process, derefType, process.Modules().First(), baseOffset, offsets) { }

	public StringPointer(Process process, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, process.Modules().First(), baseOffset, offsets) { }

	public StringPointer(Process process, DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
		: this(process, derefType, process.Module(moduleName), baseOffset, offsets) { }

	public StringPointer(Process process, string moduleName, int baseOffset, params int[] offsets)
		: this(process, DerefType.Auto, process.Module(moduleName), baseOffset, offsets) { }

	public StringPointer(DerefType derefType, Pointer parent, params int[] offsets)
		: base(derefType, parent, offsets) { }

	public StringPointer(Pointer parent, params int[] offsets)
		: this(parent.DerefType, parent, offsets) { }
	#endregion

	#region Properties
	public uint Length { get; set; } = 1024;
	public StringType StringType { get; set; } = StringType.Auto;

	public new string Current
	{
		get => (string)(base.Current);
		set => base._current = value;
	}

	public new string Old
	{
		get => (string)(base.Old);
		set => base._old = value;
	}

	public delegate void DataChangedEventHandler(string old, string current);
	public event DataChangedEventHandler OnChanged;
	#endregion

	#region Methods
	public override void Reset()
	{
		base.Current = null;
		base.Old = null;
		_lastUpdate = null;
	}

	protected override bool Internal_Update()
	{
		if (_process.TryReadString(Length, StringType, out var result, Base, Offsets) || UpdateOnNullPointer)
		{
			_old = (string)(_current);
			_current = result;
			OnChanged?.Invoke((string)(_old), (string)(_current));
			return true;
		}

		return false;
	}
	#endregion
}
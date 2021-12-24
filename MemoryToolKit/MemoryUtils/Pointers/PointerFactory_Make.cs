using MemoryToolKit.ProcessUtils;

namespace MemoryToolKit.MemoryUtils.Pointers;

public partial class Pointer
{
	public partial class Factory
	{
		#region Make<T>
		#region DefaultModule
		public Pointer<T> Make<T>(int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(_derefType, _defaultModuleBase + baseOffset, offsets);
		}

		public Pointer<T> Make<T>(DerefType derefType, int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(derefType, _defaultModuleBase + baseOffset, offsets);
		}
		#endregion

		#region ModuleName
		public Pointer<T> Make<T>(string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(_derefType, _process.Module(moduleName).BaseAddress + baseOffset, offsets);
		}

		public Pointer<T> Make<T>(DerefType derefType, string moduleName, int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(derefType, _process.Module(moduleName).BaseAddress + baseOffset, offsets);
		}
		#endregion

		#region Module
		public Pointer<T> Make<T>(ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(_derefType, module.BaseAddress + baseOffset, offsets);
		}

		public Pointer<T> Make<T>(DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets) where T : unmanaged
		{
			return Make<T>(derefType, module.BaseAddress + baseOffset, offsets);
		}
		#endregion

		#region Address
		public Pointer<T> Make<T>(IntPtr address, params int[] offsets) where T : unmanaged
		{
			return Make<T>(_derefType, address, offsets);
		}

		public Pointer<T> Make<T>(DerefType derefType, IntPtr address, params int[] offsets) where T : unmanaged
		{
			var pointer = new Pointer<T>(_process, derefType, address, offsets);
			_ = pointer.Current;

			return pointer;
		}
		#endregion

		#region Pointer
		public Pointer<T> Make<T>(Pointer parent, params int[] offsets) where T : unmanaged
		{
			return Make<T>(_derefType, parent, offsets);
		}

		public Pointer<T> Make<T>(DerefType derefType, Pointer parent, params int[] offsets) where T : unmanaged
		{
			var pointer = new Pointer<T>(derefType, parent, offsets);
			_ = pointer.Current;

			return pointer;
		}
		#endregion
		#endregion



		#region MakeString
		#region DefaultModule
		public StringPointer MakeString(int baseOffset, params int[] offsets)
		{
			return MakeString(_derefType, _defaultModuleBase + baseOffset, offsets);
		}

		public StringPointer MakeString(DerefType derefType, int baseOffset, params int[] offsets)
		{
			return MakeString(derefType, _defaultModuleBase + baseOffset, offsets);
		}
		#endregion

		#region ModuleName
		public StringPointer MakeString(string moduleName, int baseOffset, params int[] offsets)
		{
			return MakeString(_derefType, _process.Module(moduleName).BaseAddress + baseOffset, offsets);
		}

		public StringPointer MakeString(DerefType derefType, string moduleName, int baseOffset, params int[] offsets)
		{
			return MakeString(derefType, _process.Module(moduleName).BaseAddress + baseOffset, offsets);
		}
		#endregion

		#region Module
		public StringPointer MakeString(ProcessModule module, int baseOffset, params int[] offsets)
		{
			return MakeString(_derefType, module.BaseAddress + baseOffset, offsets);
		}

		public StringPointer MakeString(DerefType derefType, ProcessModule module, int baseOffset, params int[] offsets)
		{
			return MakeString(derefType, module.BaseAddress + baseOffset, offsets);
		}
		#endregion

		#region Address
		public StringPointer MakeString(IntPtr address, params int[] offsets)
		{
			return MakeString(_derefType, address, offsets);
		}

		public StringPointer MakeString(DerefType derefType, IntPtr address, params int[] offsets)
		{
			var pointer = new StringPointer(_process, derefType, address, offsets);
			_ = pointer.Current;

			return pointer;
		}
		#endregion

		#region Pointer
		public StringPointer MakeString(Pointer parent, params int[] offsets)
		{
			return MakeString(_derefType, parent, offsets);
		}

		public StringPointer MakeString(DerefType derefType, Pointer parent, params int[] offsets)
		{
			var pointer = new StringPointer(derefType, parent, offsets);
			_ = pointer.Current;

			return pointer;
		}
		#endregion
		#endregion
	}
}
using MemoryToolKit.ProcessUtils;
using System.Diagnostics;

namespace MemoryToolKit.MemoryUtils.Pointers;

public partial class Pointer
{
	public partial class Factory
	{
		#region Constructors
		public Factory(Process process, DerefType derefType, ProcessUtils.ProcessModule module)
		{
			Process = process;
			DerefType = derefType;
			DefaultModuleBase = module.BaseAddress;
		}

		public Factory(Process process, ProcessUtils.ProcessModule module)
			: this(process, DerefType.Auto, module) { }

		public Factory(Process process, DerefType derefType, string moduleName)
			: this(process, derefType, process.Module(moduleName)) { }

		public Factory(Process process, string moduleName)
			: this(process, DerefType.Auto, moduleName) { }

		public Factory(Process process, DerefType derefType)
			: this(process, derefType, process.Modules().First()) { }

		public Factory(Process process)
			: this(process, DerefType.Auto, process.Modules().First()) { }
		#endregion

		#region Fields
		internal Process Process;
		internal DerefType DerefType;
		internal IntPtr DefaultModuleBase;
		#endregion
	}
}
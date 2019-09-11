using dnlib.DotNet;

namespace Goldfuscator.Core.Protections.Analyzer
{
	public class MethodDefAnalyzer : iAnalyze
	{
		public override bool Execute(object context)
		{
			MethodDef method = (MethodDef)context;
			if (method.IsRuntimeSpecialName)
				return false;
			if (method.DeclaringType.IsForwarder)
				return false;
			return true;
		}
	}
}

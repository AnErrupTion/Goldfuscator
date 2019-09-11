using dnlib.DotNet;

namespace Goldfuscator.Core.Protections.Analyzer
{
	public class EventDefAnalyzer : iAnalyze
	{
		public override bool Execute(object context)
		{
			EventDef ev = (EventDef)context;
			if (ev.IsRuntimeSpecialName)
				return false;
			return true;
		}
	}
}

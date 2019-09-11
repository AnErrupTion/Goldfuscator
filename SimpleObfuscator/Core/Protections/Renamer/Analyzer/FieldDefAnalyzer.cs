using dnlib.DotNet;

namespace Goldfuscator.Core.Protections.Analyzer
{
	public class FieldDefAnalyzer : iAnalyze
	{
		public override bool Execute(object context)
		{
			FieldDef field = (FieldDef)context;
			if (field.IsRuntimeSpecialName)
				return false;
			if (field.IsLiteral && field.DeclaringType.IsEnum)
				return false;
			return true;
		}
	}
}

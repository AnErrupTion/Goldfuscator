using dnlib.DotNet;
using Goldfuscator.Core.Protections.Analyzer;
using Goldfuscator.Core.Utils;
using System;

namespace Goldfuscator.Core.Protections
{
	internal class Renamer : SecureRandoms
	{
        /// <summary>
        /// We are executing the method 'Renamer'. The Renamer will rename name of { Types, Methods, Parameters, Properties, Fields, Events }.
        /// </summary>
        public static void Execute(ModuleDefMD module)
		{
            foreach (var type in module.Types)
			{
				if (CanRename(type))
                {
                    if (!module.HasResources)
                    {
                        Console.WriteLine("  [RENAMER] Renaming Type Name \"" + type.Name + "\"...");
                        type.Name = GenerateRandomString(20);
                        Console.WriteLine("  [RENAMER] Renaming Type Namespace \"" + type.Namespace + "\"...");
                        type.Namespace = GenerateRandomString(20);
                    }
                    else
                    {
                        if (!Program.IsWinForms)
                        {
                            if (!Program.FileExtension.Contains("dll"))
                            {
                                Console.WriteLine("  [RENAMER] Renaming Type Name \"" + type.Name + "\"...");
                                type.Name = GenerateRandomString(20);
                                Console.WriteLine("  [RENAMER] Renaming Type Namespace \"" + type.Namespace + "\"...");
                                type.Namespace = GenerateRandomString(20);
                            }
                        }
                    }
                }

                foreach (var m in type.Methods)
                {
                    if (CanRename(m) && !Program.IsWinForms)
                    {
                        Console.WriteLine("  [RENAMER] Renaming method \"" + m.Name + "\"...");
                        m.Name = GenerateRandomString(20);
                    }

                    foreach (var para in m.Parameters)
                    {
                        Console.WriteLine("  [RENAMER] Renaming method \"" + m.Name + "\"'s parameter \"" + para.Name + "\"...");
                        para.Name = GenerateRandomString(20);
                    }
                }

                foreach (var p in type.Properties)
                {
                    if (CanRename(p))
                    {
                        Console.WriteLine("  [RENAMER] Renaming property \"" + p.Name + "\"...");
                        p.Name = GenerateRandomString(20);
                    }
                }

                foreach (var field in type.Fields)
                {
                    if (CanRename(field))
                    {
                        Console.WriteLine("  [RENAMER] Renaming field \"" + field.Name + "\"...");
                        field.Name = GenerateRandomString(20);
                    } 
                }

                foreach (var e in type.Events)
                {
                    if (CanRename(e))
                    {
                        Console.WriteLine("  [RENAMER] Renaming event \"" + e.Name + "\"...");
                        e.Name = GenerateRandomString(20);
                    }
                }
            }
		}

		/// <summary>
		/// We are checking with some Analyzers if it is possible to modify a determinate { TypeDef, MethodDef, EventDef, FieldDef }.
		/// return analyze.Execute(obj); || We are returning the execution of the renamer after checking if it is possible to modify a determinate { TypeDef, MethodDef, EventDef, FieldDef }.
		/// </summary>
		public static bool CanRename(object obj)
		{
			iAnalyze analyze = null;
			if (obj is TypeDef)
				analyze = new TypeDefAnalyzer();
			else if (obj is MethodDef)
				analyze = new MethodDefAnalyzer();
			else if (obj is EventDef)
				analyze = new EventDefAnalyzer();
			else if (obj is FieldDef)
				analyze = new FieldDefAnalyzer();
			if (analyze == null)
				return false;
			return analyze.Execute(obj);
		}
	}
}

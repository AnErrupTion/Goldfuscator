using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Goldfuscator.Core.Utils;
using System.Linq;
using System;

namespace Goldfuscator.Core.Protections.AddRandoms
{
	internal class RandomOutlinedMethods : SecureRandoms
	{
		/// <summary>
		/// We are executing the method 'RandomOutlinedMethods'. RandomOutlinedMethods will add random methods to Types.
		/// </summary>
		public static void Execute(ModuleDef module)
		{
			foreach (var type in module.Types)
			{
				foreach (var method in type.Methods.ToArray())
				{
					MethodDef strings = CreateReturnMethodDef(GenerateRandomString(Next(50, 70)), method);
					MethodDef ints = CreateReturnMethodDef(Next(11111, 999999999), method);
                    Console.WriteLine($"  [ROM] Adding junk string method \"{strings.Name}\" in \"{method.Name}\" ({type.Name})...");
                    type.Methods.Add(strings);
                    Console.WriteLine($"  [ROM] Adding junk integer method \"{strings.Name}\" in \"{method.Name}\" ({type.Name})...");
                    type.Methods.Add(ints);
                }
			}
		}

		/// <summary>
		/// We are making the return value for the randomly generated method. The return value can be an Integer, a Double or a String.
		/// </summary>
		public static MethodDef CreateReturnMethodDef(object value, MethodDef source_method)
		{
			CorLibTypeSig corlib = null;

            if (value is int)
                corlib = source_method.Module.CorLibTypes.Int32;
            else if (value is string)
                corlib = source_method.Module.CorLibTypes.String;
            MethodDef newMethod = new MethodDefUser(GenerateRandomString(50),
					MethodSig.CreateStatic(corlib),
					MethodImplAttributes.IL | MethodImplAttributes.Managed,
					MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig)
			{
				Body = new CilBody()
			};
			if (value is int)
				newMethod.Body.Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, (int)value));
			else if (value is string)
				newMethod.Body.Instructions.Add(Instruction.Create(OpCodes.Ldstr, (string)value));
            newMethod.Body.Instructions.Add(new Instruction(OpCodes.Ret));
			return newMethod;
		}
    }
}

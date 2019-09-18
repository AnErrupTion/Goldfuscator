using dnlib.DotNet;
using System.Text;
using System;
using dnlib.DotNet.Emit;

namespace Goldfuscator.Core.Protections.StringEncrypt
{
    internal class StringEncryption
    {
        /// <summary>
        /// In construction.
        /// </summary>
        public static void Execute(ModuleDefMD module)
        {
            MethodDef cctor = module.GlobalType.FindOrCreateStaticConstructor();
            MethodDef strings = CreateReturnMethodDef(Encoding.UTF8.GetString(Convert.FromBase64String("dGVzdA==")), cctor);
            Console.WriteLine($"  [STRINGENCRYPTION] Adding method \"{strings.Name}\" in \"{cctor.Name}\"...");
            module.GlobalType.Methods.Add(strings);
        }

        private static MethodDef CreateReturnMethodDef(string value, MethodDef sourceMethod)
        {
            MethodDef newMethod = new MethodDefUser("Decrypt",
                    MethodSig.CreateStatic(sourceMethod.Module.CorLibTypes.String),
                    MethodImplAttributes.IL | MethodImplAttributes.Managed,
                    MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig)
            { Body = new CilBody() };

            newMethod.Body.Instructions.Add(OpCodes.Ldstr.ToInstruction(value));
            newMethod.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
            return newMethod;
        }
    }
}

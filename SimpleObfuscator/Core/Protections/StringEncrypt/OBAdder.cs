using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Goldfuscator.Core.Utils
{
    internal class OBAdder
    {
        public static void Execute(ModuleDefMD module)
        {
            foreach (var type in module.Types)
            {
                MethodDef cctor = module.GlobalType.FindOrCreateStaticConstructor();
                string value = "Obfuscated with " + Reference.Name + " v" + Reference.Version;
                MethodDef strings = CreateReturnMethodDef(value, cctor);
                type.Methods.Add(strings);
            }
        }

        private static MethodDef CreateReturnMethodDef(string value, MethodDef source_method)
        {
            CorLibTypeSig corlib = source_method.Module.CorLibTypes.String;
            MethodDef newMethod = new MethodDefUser("Goldfuscation",
                    MethodSig.CreateStatic(corlib),
                    MethodImplAttributes.IL | MethodImplAttributes.Managed,
                    MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig)
            {
                Body = new CilBody()
            };

            newMethod.Body.Instructions.Add(Instruction.Create(OpCodes.Ldstr, value));
            newMethod.Body.Instructions.Add(new Instruction(OpCodes.Ret));
            return newMethod;
        }
    }
}

using dnlib.DotNet;
using System;

namespace Goldfuscator.Core.Protections.MetaStrip
{
    internal class MetaStrip
    {
        public static void Execute(ModuleDefMD module)
        {
            foreach (var attr in module.Assembly.CustomAttributes)
            {
                if(Renamer.CanRename(attr))
                {
                    Console.WriteLine($"  [METASTRIP] Removing \"{module.Assembly.Name}\"'s custom attribute \"{attr}\"...");
                    module.Assembly.CustomAttributes.Remove(attr);
                }
            }

            Console.WriteLine($"  [METASTRIP] Removing \"{module.Name}\"'s Mvid \"{module.Mvid}\"...");
            module.Mvid = null;
            Console.WriteLine($"  [METASTRIP] Removing \"{module.Name}\"'s Name...");
            module.Name = null;

            foreach (var type in module.Types)
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (Renamer.CanRename(attr))
                    {
                        Console.WriteLine($"  [METASTRIP] Removing \"{type.Name}\"'s custom attribute \"{attr}\"...");
                        type.CustomAttributes.Remove(attr);
                    }
                }

                foreach (var m in type.Methods)
                {
                    foreach (var attr in m.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                        {
                            Console.WriteLine($"  [METASTRIP] Removing \"{m.Name}\"'s custom attribute \"{attr}\"...");
                            m.CustomAttributes.Remove(attr);
                        }
                    }
                }

                foreach (var p in type.Properties)
                {
                    foreach (var attr in p.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                        {
                            Console.WriteLine($"  [METASTRIP] Removing \"{p.Name}\"'s custom attribute \"{attr}\"...");
                            p.CustomAttributes.Remove(attr);
                        }
                    }
                }

                foreach (var field in type.Fields)
                {
                    foreach (var attr in field.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                        {
                            Console.WriteLine($"  [METASTRIP] Removing \"{field.Name}\"'s custom attribute \"{attr}\"...");
                            field.CustomAttributes.Remove(attr);
                        }
                    }
                }

                foreach (var e in type.Events)
                {
                    foreach (var attr in e.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                        {
                            Console.WriteLine($"  [METASTRIP] Removing \"{e.Name}\"'s custom attribute \"{attr}\"...");
                            e.CustomAttributes.Remove(attr);
                        }
                    }
                }
            }
        }
    }
}

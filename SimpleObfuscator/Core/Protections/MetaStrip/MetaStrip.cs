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
                    module.Assembly.CustomAttributes.Remove(attr);
            }

            module.Mvid = null;
            module.Name = null;

            foreach (var m in module.Assembly.Modules)
            {
                m.Mvid = null;
                m.Name = null;
            }

            foreach (var type in module.Types)
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (Renamer.CanRename(attr))
                        type.CustomAttributes.Remove(attr);
                }

                foreach (var m in type.Methods)
                {
                    foreach (var attr in m.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                            m.CustomAttributes.Remove(attr);
                    }
                }

                foreach (var p in type.Properties)
                {
                    foreach (var attr in p.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                            p.CustomAttributes.Remove(attr);
                    }
                }

                foreach (var field in type.Fields)
                {
                    foreach (var attr in field.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                            field.CustomAttributes.Remove(attr);
                    }
                }

                foreach (var e in type.Events)
                {
                    foreach (var attr in e.CustomAttributes)
                    {
                        if (Renamer.CanRename(attr))
                            e.CustomAttributes.Remove(attr);
                    }
                }
            }
        }
    }
}

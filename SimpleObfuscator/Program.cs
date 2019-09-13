using dnlib.DotNet;
using Goldfuscator.Core.Protections;
using Goldfuscator.Core.Protections.AddRandoms;
using Goldfuscator.Core.Protections.MetaStrip;
using Goldfuscator.Core.Utils;
using System;
using System.IO;

internal class Program
{
    public static bool IsWinForms = false;
    public static string FileExtension = string.Empty;

    /// <summary>
    /// ModuleDefMD module = ModuleDefMD.Load(Console.ReadLine()); || We are getting the file path by reading the console.
    /// Execute(module); || We are obfuscating the file.
    /// module.Write(Environment.CurrentDirectory + @"\protected.exe"); || We are rewriting the file in the current directory.
    /// </summary>
    private static void Main()
	{
        Console.Title = Reference.Name + " v" + Reference.Version;

        Console.WriteLine("Drag & drop your file here :");
        string file = Console.ReadLine().Replace("\"", "");

        FileExtension = Path.GetExtension(file);
        if (FileExtension.Contains("exe"))
        {
            Console.Clear();
            Console.WriteLine("Is your file a Windows Forms app (true) or Console app (false)?");
            IsWinForms = Convert.ToBoolean(Console.ReadLine());
        }

        Console.Clear();

        ModuleDefMD module = ModuleDefMD.Load(file);
        string fileName = Path.GetFileNameWithoutExtension(file);

        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine(Reference.Prefix + "Loaded : " + module.Assembly.FullName);
        Console.WriteLine(Reference.Prefix + "Has Resources : " + module.HasResources);
        if (FileExtension.Contains("exe"))
        {
            Console.WriteLine(Reference.Prefix + "Is Windows Forms : " + IsWinForms);
        }
        Console.WriteLine(Reference.Prefix + "File Extension : " + FileExtension.Replace(".", "").ToUpper());
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        Console.Clear();

        Execute(module);
        Console.WriteLine(Reference.Prefix + "Saving file...");
		module.Write(@"C:\Users\" + Environment.UserName + @"\Desktop\" + fileName + "_protected" + FileExtension);

        Console.WriteLine(Reference.Prefix + "Done!");
        Console.ReadKey();
    }

    /// <summary>
    /// Renamer.Execute(module); || We are executing the obfuscation method 'Renamer'.
    /// RandomOutlinedMethods.Execute(module); || We are executing the obfuscation method 'RandomOutlinedMethods'.
    /// MetaStrip.Execute(module); || We are executing the obfuscation method 'MetaStrip'.
    /// OBAdder.Execute(module); || We are executing the obfuscation method 'OBAdder'.
    /// </summary>
    private static void Execute(ModuleDefMD module)
	{
        Console.WriteLine(Reference.Prefix + "Applying 'Renamer' obfuscation...");
		Renamer.Execute(module: module);
        Console.WriteLine(Reference.Prefix + "Applying 'RandomOutlinedMethods' obfuscation...");
        RandomOutlinedMethods.Execute(module: module);
        Console.WriteLine(Reference.Prefix + "Applying 'MetaStrip' obfuscation...");
        MetaStrip.Execute(module: module);
        Console.WriteLine(Reference.Prefix + "Applying 'OBAdder' obfuscation...");
        OBAdder.Execute(module: module);
        //Console.WriteLine(Reference.Prefix + "Applying 'StringEncryption' obfuscation...");
        //StringEncryption.Execute(module: module);
    }
}

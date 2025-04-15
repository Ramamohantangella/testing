using System;
using System.IO;
using IWshRuntimeLibrary;
using Microsoft.Win32;

class Installer
{
    static void CopyFiles(string sourceDir, string targetDir)
    {
        Directory.CreateDirectory(targetDir);
        foreach (var file in Directory.GetFiles(sourceDir))
        {
            var dest = Path.Combine(targetDir, Path.GetFileName(file));
            File.Copy(file, dest, true);
        }
        Console.WriteLine("Files copied successfully.");
    }

    static void CreateShortcut(string exePath, string shortcutPath)
    {
        var shell = new WshShell();
        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = exePath;
        shortcut.Save();
        Console.WriteLine("Shortcut created.");
    }

    static void AddRegistryEntry()
    {
        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MyMechanicalSoftware"))
        {
            key.SetValue("InstallPath", @"C:\Program Files\MyMechanicalSoftware");
        }
        Console.WriteLine("Registry entry added.");
    }

    static void Main()
    {
        string sourceDir = @"C:\InstallSource";
        string targetDir = @"C:\Program Files\MyMechanicalSoftware";
        string exePath = Path.Combine(targetDir, "MechanicalApp.exe");
        string shortcutPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "MechanicalApp.lnk");

        CopyFiles(sourceDir, targetDir);
        CreateShortcut(exePath, shortcutPath);
        AddRegistryEntry();

        Console.WriteLine("Installation complete.");
    }
}

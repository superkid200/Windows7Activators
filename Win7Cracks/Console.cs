using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Win7Cracks.Properties;

namespace Win7Cracks
{
    public class ConsoleClass
    {
        public static void TakeControl()
        {
            Win32.AllocConsole();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("[1] Download Windows Loader (not implemented yet)");
            Console.WriteLine("[2] Download KMSPico");
            Console.WriteLine("[3] KMS Servers (Pro/Enterprise only!)");
            Console.WriteLine("[4] Increase trial period by 1 month (limited).");
            Console.WriteLine("[5] Reset trial period increase count*.");
            Console.WriteLine("[6] Download RemoveWAT* (not implemented yet)");
            Console.WriteLine("[7] Disable Automatic Activation*");
            Console.WriteLine("[8] Disable WAT*");
            Console.WriteLine("");
            Console.WriteLine("The options with asterisk * is not recommended since it changes system files or registry keys.");
            Console.WriteLine("-------------------------------");
            bool hasSucc = false;
            while(!hasSucc)
            {
                string keyChar = Console.ReadLine();
                switch (keyChar)
                {
                    case "1":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Unfortunately, this option has not been implemented yet. Please check back at a later release.");
                        hasSucc = true;
                        break;
                    case "2":
                        Console.WriteLine("-------------------------------");
                        Uri uri = new Uri("https://official-kmspico.com/wp-content/uploads/2019/07/KMSpico.zip");
                        new frmDownload(uri, Path.GetTempPath() + "KMSpico.zip").ShowDialog();
                        if(File.Exists(Path.GetTempPath() + "KMSpico.zip"))
                        {
                            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", Path.GetTempPath() + "KMSpico.zip"));
                            Console.WriteLine("Password of ZIP file: 12345");
                            Console.WriteLine("Extract the selected ZIP file (KMSpico.zip) and extract it using WinRAR with password above and then run KMSpico-setup.exe and follow the screen prompts.");
                        }
                        else
                        {
                            Console.WriteLine("You canceled the download. Please relaunch this program.");
                        }
                        hasSucc = true;
                        break;
                    case "3":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Activating Windows using KMS servers...");
                        bool succ = Activators.KMSServers();
                        if(succ)
                        {
                            Console.WriteLine("Activation successful!");
                            Console.WriteLine("A restart is required to complete the activation.");
                        }
                        else
                        {
                            Console.WriteLine("Activation failed!");
                            Console.WriteLine("Please check if you have the correct edition of Windows and if you're connected to the Internet.");
                        }
                        hasSucc = true;
                        break;
                    case "4":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Increasing your trial period by 1 month...");
                        bool succe = Activators.Rearm();
                        if(succe)
                        {
                            Console.WriteLine("Activation successful!");
                        }
                        else
                        {
                            Console.WriteLine("Activation failed! Make sure you still have rearms remaining by typing slmgr /dlv into an administrative command prompt.");
                        }
                        hasSucc = true;
                        break;
                    case "5":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Are you sure about your choice? This option may damage your computer!");
                        Console.Write("Press Y to continue...");
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Y)
                        {
                            Console.Write("\n");
                            using (StreamWriter stream = new StreamWriter("C:\\resettrial.bat"))
                            {
                                stream.Write(Resources.resettrial);
                            }
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\Setup", "CmdLine", "cmd.exe /k C:\\resettrial.bat");
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\Setup", "SystemSetupInProgress", 1, RegistryValueKind.DWord);
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\Setup", "SetupType", 2, RegistryValueKind.DWord);
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLUA", 0, RegistryValueKind.DWord);
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "VerboseStatus", 1, RegistryValueKind.DWord);
                            Console.WriteLine("Preparation done! You need to restart your PC to start resetting the trial increase count.");
                        }
                        hasSucc = true;
                        break;
                    case "6":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Unfortunately, this option has not been implemented yet. Please check back at a later release.");
                        hasSucc = true;
                        break;
                    case "7":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Are you sure about your choice? This option may damage your computer!");
                        Console.Write("Press Y to continue...");
                        var b = Console.ReadKey();
                        if (b.Key == ConsoleKey.Y)
                        {
                            Console.Write("\n");
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SoftwareProtectionPlatform\\Activation", "Manual", 1, RegistryValueKind.DWord);
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SoftwareProtectionPlatform\\Activation", "NotificationDisabled", 1, RegistryValueKind.DWord);
                            Console.WriteLine("Done!");
                        }
                        hasSucc = true;
                        break;
                    case "8":
                        Console.WriteLine("-------------------------------");
                        Console.WriteLine("Are you sure about your choice? This option may damage your computer!");
                        Console.Write("Press Y to continue...");
                        var a = Console.ReadKey();
                        if (a.Key == ConsoleKey.Y)
                        {
                            Console.Write("\n");
                            Process prog = new Process();
                            prog.StartInfo.FileName = "cmd.exe";
                            prog.StartInfo.Arguments = "/c \"sc config sppsvc start= disabled && net stop sppsvc\"";
                            prog.StartInfo.Verb = "runas";
                            prog.Start();
                            Console.WriteLine("Service \"Software Protection\" has been disabled and stopped.");
                        }
                        hasSucc = true;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
            }
            Console.Write("Press Enter to exit...");
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }
    }
}

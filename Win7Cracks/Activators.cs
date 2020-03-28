using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace Win7Cracks
{
    public static class Activators
    {
        public static bool KMSServers()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection col = searcher.Get();
            ManagementObject operatingSystem = col.OfType<ManagementObject>().ElementAt(0);
            uint sku = (uint)operatingSystem["OperatingSystemSKU"];

            bool isPro = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", string.Empty).ToString().Contains("Professional");
            // Enterprise = 4
            // To get whether user is using Professional or not we'll use Registry.
            if(sku != 4 && !isPro)
            {
                return false;
            }
            string arguments = string.Empty;
            if(sku == 4)
            {
                arguments = $"/c \"cscript //nologo slmgr.vbs -ipk 33PXH-7Y6KF-2VJC9-XBBR8-HVTHH\"";
            }
            if(isPro)
            {
                arguments = $"/c \"cscript //nologo slmgr.vbs -ipk FJ82H-XT6CR-J8D7P-XQJJ2-GPDD4\"";
            }
            Process prog = new Process();
            prog.StartInfo.FileName = "cmd.exe";
            prog.StartInfo.Arguments = arguments;
            prog.Start();
            Console.WriteLine("Installing KMS key...");
            prog.WaitForExit();
            string[] servers =
            {
                "kms.digiboy.ir",
                "kms8.MSGuides.com",
                "KMS_Sev=kms.chinancce.com"
            };
            Console.WriteLine("");
            foreach(string server in servers)
            {
                arguments = $"/c \"cscript //nologo C:\\Windows\\System32\\slmgr.vbs -skms {server}\"";
                prog = new Process();
                prog.StartInfo.FileName = "cmd.exe";
                prog.StartInfo.Verb = "runas";
                prog.StartInfo.Arguments = arguments;
                prog.StartInfo.UseShellExecute = false;
                prog.StartInfo.RedirectStandardOutput = true;
                prog.Start();
                Console.Write("Setting KMS server to server {0}...", servers.ToList().IndexOf(server) + 1);
                string output = string.Empty;
                using (StreamReader reader = prog.StandardOutput)
                {
                    output = reader.ReadToEnd();
                }
                prog.WaitForExit();
                if (output.Contains("successfully"))
                {
                    Console.WriteLine("done!");
                    arguments = $"/c \"cscript //nologo C:\\Windows\\System32\\slmgr.vbs -ato\"";
                    prog = new Process();
                    prog.StartInfo.FileName = "cmd.exe";
                    prog.StartInfo.Verb = "runas";
                    prog.StartInfo.Arguments = arguments;
                    prog.StartInfo.UseShellExecute = false;
                    prog.StartInfo.RedirectStandardOutput = true;
                    prog.Start();
                    output = string.Empty;
                    Console.Write("Activating using server {0}...", servers.ToList().IndexOf(server) + 1);
                    using (StreamReader reader = prog.StandardOutput)
                    {
                        output = reader.ReadToEnd();
                    }
                    prog.WaitForExit();
                    if(output.Contains("successfully"))
                    {
                        Console.WriteLine("done!");
                        Console.WriteLine("");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("failed!");
                        Console.WriteLine("");
                        Console.WriteLine("Activation using server {0} failed! Trying server {1}", servers.ToList().IndexOf(server) + 1, servers.ToList().IndexOf(server) + 2);
                        Console.WriteLine("");
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static bool Rearm()
        {
            Process prog = new Process();
            prog.StartInfo.FileName = "cmd.exe";
            prog.StartInfo.Arguments = "/c \"cscript //nologo slmgr.vbs -rearm\"";
            prog.StartInfo.UseShellExecute = false;
            prog.StartInfo.RedirectStandardOutput = true;
            prog.Start();
            string output = string.Empty;
            using (StreamReader reader = prog.StandardOutput)
            {
                output = reader.ReadToEnd();
            }
            prog.WaitForExit();
            return output.Contains("successfully");
        }
    }
}

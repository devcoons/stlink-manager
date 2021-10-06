using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace STLinkDevice
{
    public partial class STLinkManager
    {
        private static volatile STLinkManager instance;
        private static object syncRoot = new Object();

        public static STLinkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new STLinkManager();
                    }
                }

                return instance;
            }
        }
    }


    public partial class STLinkManager
    {
        private bool ExtractSTLinkCLI()
        {
            try
            {
                if (DeleteSTLinkCLI() == false)
                    return false;

                File.WriteAllBytes(@".stlink.zip", Resources.stlink);
                System.IO.Compression.ZipFile.ExtractToDirectory(@".stlink.zip", @".stlink");
                File.Delete(@".stlink.zip");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool DeleteSTLinkCLI()
        {
            try
            {
                if (Directory.Exists(@".stlink"))
                    Directory.Delete(@".stlink", true);

                if (File.Exists(@".stlink.zip"))
                    File.Delete(@".stlink.zip");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExecuteSTLinkCmd(string cmd, string resContains)
        {
            if (ExtractSTLinkCLI() == false)
                return false;

            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            proc.StandardInput.WriteLine(".stlink\\bin\\STM32_Programmer_CLI.exe "+cmd);
            proc.StandardInput.Flush();
            proc.StandardInput.Close();
            proc.WaitForExit();
            return proc.StandardOutput.ReadToEnd().ToUpper().Contains(resContains.ToUpper());
        }

        public string ExecuteSTLinkCmd(string cmd)
        {
            if (ExtractSTLinkCLI() == false)
                return null;

            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            proc.StandardInput.WriteLine(".stlink\\bin\\STM32_Programmer_CLI.exe " + cmd);
            proc.StandardInput.Flush();
            proc.StandardInput.Close();
            proc.WaitForExit();
            return proc.StandardOutput.ReadToEnd();
        }
    }

}

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace FacebookAutologin
{
    class Dcom
    {
        public static void DcomChange()
        {
            Console.WriteLine("OK111111");
            Thread main = new Thread(() =>
            {
                RunCMD(@"%Windir%\system32\Rasdial /disconnect");

                Console.WriteLine("OK1");
                Thread.Sleep(1000);

                Console.WriteLine("OK2");

                RunCMD(@"%Windir%\system32\Rasdial Mobifone");
                Console.WriteLine("OK3");
            });
            main.IsBackground = true;
            main.Start();
        }
        public static void RunCMD(string cmd)
        {
            Process cmdProcess;
            cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.Arguments = "/c " + cmd;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();
            string output = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
        }
        public static void WriteLine(string tenfile, string chuoi)
        {
            using (StreamWriter file = new StreamWriter(tenfile, true, System.Text.Encoding.UTF8))
            {
                file.WriteLine(chuoi);
                file.Close();
            }
        }
    }
}

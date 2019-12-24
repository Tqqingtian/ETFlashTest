using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Path = System.IO.Path;

namespace ETModel
{
    public static class ProcessHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="arguments"></param>
        /// <param name="workingDirectory">是否等待退出</param>
        /// <param name="waitExit"></param>
        /// <returns></returns>
        public static Process Run(string exe, string arguments, string workingDirectory = ".", bool waitExit = false)
        {
            try
            {
                bool redirectStandardOutput = true;
                bool redirectStandardError = true;
                bool useShellExecute = false;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    redirectStandardOutput = false;
                    redirectStandardError = false;
                    useShellExecute = true;
                }

                if (waitExit)
                {
                    redirectStandardOutput = true;
                    redirectStandardError = true;
                    useShellExecute = false;
                }
                
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = useShellExecute,
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = redirectStandardOutput,
                    RedirectStandardError = redirectStandardError,
                };

                Log.Debug("Web运行的路径："+ workingDirectory);
                Process process = Process.Start(info);

                if (waitExit)
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"{process.StandardOutput.ReadToEnd()} {process.StandardError.ReadToEnd()}");
                    }
                }

                return process;
            }
            catch (Exception e)
            {
                throw new Exception($"dir: {Path.GetFullPath(workingDirectory)}, command: {exe} {arguments}", e);
            }
        }
    }
}
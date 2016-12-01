using System;
using System.IO;

namespace SecuredMail.Logger
{
    /// <summary>
    /// Very simple logger
    /// </summary>
    public static class Logger
    {
        private static readonly string currentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
        private static readonly string logFile = Path.Combine(currentDirectory, $"log_{DateTime.Now.ToString("yyyy_MM_dd__HH_mm_ss")}.txt");

        public static void AppendLogMessage(string message, params object[] arg)
        {
            Log(message, arg, logFile);
        }

        private static async void Log(string message, object[] args, string logFile)
        {
            FileUtils.CreateDirectoryIfNotExists(currentDirectory);
            StreamWriter writer = FileUtils.GetOrCreateFile(logFile);
            await writer.WriteLineAsync($"{DateTime.Now.ToString("s")}\t\t{message}{string.Join(",", args)}");
            writer.Close();
        }
    }
}
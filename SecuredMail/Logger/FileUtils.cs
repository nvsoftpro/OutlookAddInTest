using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuredMail.Logger
{
    /// <summary>
    /// Simple file utils class
    /// </summary>
    public static class FileUtils
    {
        public static readonly object lockObject = new object();

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                lock (lockObject)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }

        public static StreamWriter GetOrCreateFile(string path)
        {
            if (!File.Exists(path))
            {
                lock (lockObject)
                {
                    if (!File.Exists(path))
                    {
                        return File.CreateText(path);
                    }
                }
            }

            return File.AppendText(path);
        }
    }
}

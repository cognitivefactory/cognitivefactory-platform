using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveFactory.Installer.Windows
{
    class Wsl
    {
        // Executes : wsl -l -v
        // Returns  : 
        // -1 if WSL2 is not installed
        //  0 if WSL2 is ready but no distribution was installed
        //  1 if WSL2 is ready but the default distribution is set to run in WSL version 1
        //  2 if WSL2 is ready and the default distribution is set to run in WSL version 2
        public static int CheckWSLVersion()
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("wsl.exe", "-l -v", 5, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error))
                {
                    if (String.IsNullOrEmpty(output))
                    {
                        return 0;
                    }
                    var lines = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length < 2)
                    {
                        return 0;
                    }
                    for (var i = 1; i < lines.Length; i++)
                    {
                        var line = lines[i];
                        var cols = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (cols.Length == 4)
                        {
                            return Int32.Parse(cols[3]);
                        }
                    }
                    return 0;
                }
            }
            catch (Exception)
            { }
            return -1;
        }

        public static Version CheckKernelVersion()
        {
            return new Version();
        }
    }
}

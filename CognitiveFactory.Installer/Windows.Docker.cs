using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveFactory.Installer.Windows
{
    class Docker
    {
        // Executes : wsl -- ls -l $(which docker)
        // Returns  : 
        // true if a previous docker version must be uninstalled
        // false otherwise
        public static bool CheckPreviousDockerVersionInWsl()
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("wsl.exe", "-- ls -l $(which docker)", 5, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error) && !String.IsNullOrEmpty(output))
                {
                    if(!output.Contains("docker-desktop"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            { }
            return false;
        }
    }
}

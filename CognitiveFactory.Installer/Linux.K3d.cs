﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveFactory.Installer.Linux
{
    class K3d
    {
        // Executes : k3d version
        // Returns  : 
        // Version object if k3d.io version was correctly parsed
        // null otherwise
        public static Version CheckK3dVersion()
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("k3d", "version", 5, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error) && !String.IsNullOrEmpty(output))
                {
                    // k3d version v4.4.7
                    // k3s version v1.21.2-k3s1 (default)
                    int versionIndex = output.IndexOf("version");
                    if (versionIndex > 0 && (versionIndex + 9) < output.Length)
                    {
                        versionIndex += 9;
                        int firstDot = output.IndexOf('.', versionIndex);
                        int secondDot = output.IndexOf('.', firstDot + 1);
                        int eol = output.IndexOf('\n', secondDot + 1);
                        if (firstDot > versionIndex && secondDot > firstDot && eol > secondDot)
                        {
                            var major = Int32.Parse(output.Substring(versionIndex, firstDot - versionIndex));
                            var minor = Int32.Parse(output.Substring(firstDot + 1, secondDot - firstDot - 1));
                            var build = Int32.Parse(output.Substring(secondDot + 1, eol - secondDot - 1));
                            return new Version(major, minor, build);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            return null;
        }
    }
}

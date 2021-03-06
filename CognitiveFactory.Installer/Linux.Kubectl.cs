﻿using System;

namespace CognitiveFactory.Installer.Linux
{
    class Kubectl
    {
        // Executes : kubectl version
        // Returns  : 
        // Version object if kubectl version was correctly parsed
        // null otherwise
        public static Version CheckKubectlVersion()
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("kubectl", "version", 5, out output, out error);
                if (!String.IsNullOrEmpty(output))
                {
                    // GitVersion:"v1.21.2"
                    int versionIndex = output.IndexOf("GitVersion");
                    if (versionIndex > 0 && (versionIndex + 13) < output.Length)
                    {
                        versionIndex += 13;
                        int firstDot = output.IndexOf('.', versionIndex);
                        int secondDot = output.IndexOf('.', firstDot + 1);
                        int eol = output.IndexOf('"', secondDot + 1);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveFactory.Installer.Windows
{
    class Wsl
    {
        // wsl -l -v
        public static void CheckVersion()
        {
            string output;
            string error;
            int exitcode = Process.Run("wsl2.exe", "-l -v", 5, out output, out error);

            Console.WriteLine("--exitcode--");
            Console.WriteLine(exitcode);
            Console.WriteLine("--output--");
            Console.WriteLine(output);
            Console.WriteLine("--error--");
            Console.WriteLine(error);
        }
    }
}

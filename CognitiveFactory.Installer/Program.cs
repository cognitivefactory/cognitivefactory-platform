using Spectre.Console;
using System;
using System.Runtime.InteropServices;

namespace CognitiveFactory.Installer
{
    class Program
    {
        public static string Version = "0.1";

        static int Main(string[] args)
        {
            AnsiConsole.MarkupLine(              "[bold navy on paleturquoise1]--------------------------------------------[/]");
            AnsiConsole.MarkupLine(String.Format("[bold navy on paleturquoise1]| CognitiveFactory Platform Installer v{0} |[/]", Version));
            AnsiConsole.MarkupLine(              "[bold navy on paleturquoise1]--------------------------------------------[/]");
            AnsiConsole.WriteLine();

            AnsiConsole.MarkupLine("1. [underline]Checking operating system[/] :");
            AnsiConsole.WriteLine();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {                
                try
                {
                    var winver = Environment.OSVersion.Version;
                    if (winver.Major < 10)
                    {
                        AnsiConsole.MarkupLine(String.Format("[red]Sorry, Windows 10 is required[/] (your version = \"{0}\")", winver));
                        return 1;
                    }
                    else if(winver.Build < 18362)
                    {
                        AnsiConsole.MarkupLine(String.Format("[red]Sorry, Windows 10 version 1903 (build number > 18362) is required[/] (your build = \"{0}\")", winver));
                        return 1;
                    } 
                    else
                    {
                        AnsiConsole.MarkupLine("Windows version [bold green]OK[/]");
                        AnsiConsole.WriteLine();
                        DoWindowsInstall();
                    }
                }
                catch(Exception e)
                {
                    AnsiConsole.MarkupLine("[red]Failed to check Windows version[/]");
                } 
            } 
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // https://www.cyberciti.biz/faq/how-to-check-os-version-in-linux-command-line/
                // https://github.com/dotnet/core-setup/blob/master/src/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Unix.cs

                try
                {
                    var linuxver = Environment.OSVersion.Version;
                    if (linuxver.Major < 5 || (linuxver.Major == 5 && linuxver.Minor < 4))
                    {
                        AnsiConsole.MarkupLine(String.Format("[red]Sorry, Linux kernel version > 5.4 is required[/] (your version = \"{0}\")", linuxver));
                        return 1;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("Linux kernel version [bold green]OK[/]");
                        AnsiConsole.WriteLine();
                        DoLinuxInstall();
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine("[red]Failed to check Linux version[/]");
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                AnsiConsole.MarkupLine("[red]Sorry, OSX is not yet supported[/]");
                return 1;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                AnsiConsole.MarkupLine("[red]Sorry, FreeBSD is not supported[/]");
                return 1;
            } 
            else
            {
                AnsiConsole.MarkupLine("[red]Sorry, your operating system is not supported[/]");
                return 1;
            }

            return 0;
        }

        private static void DoWindowsInstall()
        {
            
        }

        private static void DoLinuxInstall()
        {

        }
    }
}

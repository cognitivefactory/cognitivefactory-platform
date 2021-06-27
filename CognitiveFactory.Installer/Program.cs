using Spectre.Console;
using System;
using System.Runtime.InteropServices;

namespace CognitiveFactory.Installer
{
    class Program
    {
        static int Main(string[] args)
        {
            AnsiConsole.MarkupLine("[bold navy on paleturquoise1]---------------------------------------[/]");
            AnsiConsole.MarkupLine("[bold navy on paleturquoise1]| CognitiveFactory Platform Installer |[/]");
            AnsiConsole.MarkupLine("[bold navy on paleturquoise1]---------------------------------------[/]");
            AnsiConsole.WriteLine();

            AnsiConsole.MarkupLine("1. [underline]Checking operating system[/] :");
            AnsiConsole.WriteLine();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {                
                try
                {                    
                    if (!PlatformDetection.IsWindows10Version1903OrGreater)
                    {
                        var winver = Environment.OSVersion.Version;
                        AnsiConsole.MarkupLine($"[red]Sorry, Windows 10 version 1903 (build number > 18362) is required[/] (your version = \"{winver}\")");
                        return 1;
                    } 
                    else
                    {
                        var winarch = RuntimeInformation.ProcessArchitecture;
                        if (winarch != Architecture.X64)
                        {
                            AnsiConsole.MarkupLine($"[red]Sorry, the Windows subsystem from Linux is only supported on x64 systems[/] (your architecture = \"{winarch}\")");
                            return 1;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("Windows version [bold green]OK[/]");
                            AnsiConsole.WriteLine();
                        }
                    }
                }
                catch(Exception e)
                {
                    AnsiConsole.MarkupLine("[red]Failed to check Windows version[/]");
                    return 1;
                }

                DoWindowsInstall();
            } 
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // https://www.cyberciti.biz/faq/how-to-check-os-version-in-linux-command-line/
                // https://github.com/dotnet/core-setup/blob/master/src/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Unix.cs

                try
                {                    
                    if (!PlatformDetection.IsUbuntu1804OrHigher)                            
                    {
                        var linuxdistro = PlatformDetection.GetDistroInfo();
                        AnsiConsole.MarkupLine($"[red]Sorry, Ubuntu version 1804 is required[/] (your distribution = \"{linuxdistro.Id} {linuxdistro.VersionId}\")");
                        return 1;
                    }
                    else
                    {
                        var linuxver = Environment.OSVersion.Version;
                        if (linuxver.Major < 5 || (linuxver.Major == 5 && linuxver.Minor < 4))
                        {
                            AnsiConsole.MarkupLine($"[red]Sorry, Linux kernel version > 5.4 is required[/] (your version = \"{linuxver}\")");
                            return 1;
                        }
                        else
                        {

                            AnsiConsole.MarkupLine("Linux version [bold green]OK[/]");
                            AnsiConsole.WriteLine();
                        }
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine("[red]Failed to check Linux version[/]");
                    return 1;
                }

                DoLinuxInstall();
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

            Console.WriteLine($"Running as admin ? {AdminHelpers.IsProcessElevated()}");
            Console.ReadLine();

            return 0;
        }

        private static void DoWindowsInstall()
        {
            Windows.Wsl.CheckVersion();
        }

        private static void DoLinuxInstall()
        {

        }
    }
}

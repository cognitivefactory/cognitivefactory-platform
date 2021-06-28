using Spectre.Console;
using System;
using System.Runtime.InteropServices;

namespace CognitiveFactory.Installer
{
    class Program
    {
        static int Main(string[] args)
        {
            AnsiConsole.MarkupLine("[bold navy on grey93] ----------------------------------- [/]");
            AnsiConsole.MarkupLine("[bold navy on grey93] CognitiveFactory Platform Installer [/]");
            AnsiConsole.MarkupLine("[bold navy on grey93] ----------------------------------- [/]");
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

                return DoWindowsInstall();
            } 
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
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

                return DoLinuxInstall();
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
        }

        private static int DoWindowsInstall()
        {
            AnsiConsole.MarkupLine("2. [underline]Checking Windows Subsystem for Linux[/] :");
            AnsiConsole.WriteLine();

            switch(Windows.Wsl.CheckWSLVersion())
            {
                case -1:
                    AnsiConsole.MarkupLine("[red]Windows Subsystem For Linux 2 is not installed[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("Please go to [yellow underline]https://docs.microsoft.com/en-us/windows/wsl/install-win10[/] and follow the instructions");
                    AnsiConsole.MarkupLine("Select the following Linux distribution : [white]Ubuntu 20.04[/]");
                    AnsiConsole.WriteLine();
                    return 1;
                case 0:
                    AnsiConsole.MarkupLine("Windows Subsystem For Linux 2 seems to be activated, but [red]no Linux distribution is installed[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("Please go to the URL below and select the following Linux distribution : [white]Ubuntu 20.04[/]");
                    AnsiConsole.MarkupLine("[yellow underline]https://docs.microsoft.com/en-us/windows/wsl/install-win10#step-6---install-your-linux-distribution-of-choice[/]");
                    AnsiConsole.WriteLine();
                    return 1;
                case 1:                
                    AnsiConsole.MarkupLine("Windows Subsystem For Linux 2 seems to be activated, but [red]your default Linux distribution is using WSL 1[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("Please use one of the commands below to migrate your distribution to WSL 2 or select another distribution :");
                    AnsiConsole.MarkupLine("[white]wsl --set-version <Distribution> 2[/] or  [white]wsl --set-default <Distribution>[/]");
                    AnsiConsole.WriteLine();
                    return 1;
                case 2:
                    AnsiConsole.MarkupLine("Windows Subsystem For Linux [bold green]OK[/]");
                    AnsiConsole.WriteLine();
                    break;
            }

            Windows.Wsl.CheckKernelVersion();

            AnsiConsole.MarkupLine("3. [underline]Checking Docker Desktop[/] :");
            AnsiConsole.WriteLine();

            return 0;
        }

        private static int DoLinuxInstall()
        {
            Console.WriteLine($"Running as admin ? {AdminHelpers.IsProcessElevated()}");
            Console.ReadLine();

            return 0;
        }
    }
}

using Spectre.Console;
using System;

namespace CognitiveFactory.Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Render(new BarChart()
                .Width(60)
                .Label("[green bold underline]Number of fruits[/]")
                .CenterLabel()
                .AddItem("Apple", 12, Color.Yellow)
                .AddItem("Orange", 54, Color.Green)
                .AddItem("Banana", 33, Color.Red));
            Console.ReadLine();
        }
    }
}

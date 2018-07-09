using System;
using GildedRose.Interfaces;

namespace GildedRose.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Separator()
        {
            Log("###########################################\n");
        }
    }
}

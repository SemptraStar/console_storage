using System;
using System.Linq;
using ConsoleStorage.Models;
using System.Globalization;

namespace ConsoleStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleInterface = new ConsoleInterface();
            consoleInterface.MainMenu();
        }
    }
}

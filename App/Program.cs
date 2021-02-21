using System;
using System.Text.RegularExpressions;
using App.X86;

using static App.X86.Registers;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Processor processor = new Processor();

            Console.WriteLine(DateTime.Now);
            for (int i = 0; i<100_000_000; i++)
            {
                processor.Inc(eax);
            }
            Console.WriteLine(DateTime.Now);
        }
    }

    public class Interpreter
    {

        public void ExecuteCommand(string line)
        {
            char[] whitespace = new char[] { ' ', '\t', ','};
            string[] arguments = line.Split(whitespace, StringSplitOptions.RemoveEmptyEntries);
            if (arguments.Length == 0)
            {
                return;
            }

            string command = arguments[0];
            
            
        }

        private void Mov(string[] arguments)
        {

        }
    }
}

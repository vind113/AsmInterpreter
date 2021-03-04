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

            processor.Mov(al, new byte[] { 12, 12 });
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

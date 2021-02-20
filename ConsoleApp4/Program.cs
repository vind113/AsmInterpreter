using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp4
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

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


    public class Registers
    {
        // https://ru.wikipedia.org/wiki/Регистр_флагов
        [Flags]
        private enum Flags : long
        {
            ZERO = 0,
            // FLAGS
            CF = 1,
            PF = CF << 2,
            AF = PF << 2,
            ZF = AF << 2,
            SF = ZF << 1,
            TF = SF << 1,
            IF = TF << 1,
            DF = IF << 1,
            OF = DF << 1,
            IOPL1 = OF << 1,
            IOPL2 = IOPL1 << 1,
            NT = IOPL2 << 1,
            // EFLAGS
            RF = NT << 2,
            VM = RF << 1,
            AC = VM << 1,
            VIF = AC << 1,
            VIP = VIF << 1,
            ID = VIP << 1
            // RFLAGS
        }

        private readonly Dictionary<string, int> _registers = new Dictionary<string, int>()
        {
            {"eax", 0 },
            {"ebx", 0 },
            {"ecx", 0 },
            {"edx", 0 }
        };

        private Flags _flagsRegister = Flags.ZERO;

        public void Mov(string registerName, int value)
        {
            if (!_registers.ContainsKey(registerName))
            {
                throw new ArgumentException("invalid register");
            }

            _registers[registerName] = value;
        }

        public void Inc(string registerName)
        {
            if (!_registers.ContainsKey(registerName))
            {
                throw new ArgumentException("invalid register");
            }

            _registers[registerName]++;
        }

        public void Dec(string registerName)
        {
            if (!_registers.ContainsKey(registerName))
            {
                throw new ArgumentException("invalid register");
            }

            _registers[registerName]--;
        }

        public bool NotZero(string registerName)
        {
            if (!_registers.ContainsKey(registerName))
            {
                throw new ArgumentException("invalid register");
            }

            return _registers[registerName] != 0;
        }

        private void SetFlag(Flags flags)
        {
            _flagsRegister |= flags;
        }
    }
}

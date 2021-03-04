using System.Collections.Generic;
using System.Collections.Immutable;
using static App.X86.Registers;

namespace App.X86
{
    public class Processor
    {
        private static readonly ImmutableHashSet<Registers> _hRegisterNames = ImmutableHashSet.CreateRange(new List<Registers>() { ah, bh, ch, dh });

        private static readonly ImmutableHashSet<Registers> _8BitRegisterNames = ImmutableHashSet.CreateRange(new List<Registers>() { al, bl, cl, dl, ah, bh, ch, dh });
        private static readonly ImmutableHashSet<Registers> _16BitRegisterNames = ImmutableHashSet.CreateRange(new List<Registers>() { ax, bx, cx, dx });
        private static readonly ImmutableHashSet<Registers> _32BitRegisterNames = ImmutableHashSet.CreateRange(new List<Registers>() { eax, ebx, ecx, edx });
        private static readonly ImmutableHashSet<Registers> _64BitRegisterNames = ImmutableHashSet.CreateRange(new List<Registers>() { rax, rbx, rcx, rdx });

        private static readonly IReadOnlyDictionary<Registers, Registers> _hRegisterNameToFullRegisterName = new Dictionary<Registers, Registers>()
        {
            {ah, rax },
            {bh, rbx },
            {ch, rcx },
            {dh, rdx }
        };

        private readonly Dictionary<Registers, Register> _generalPurposeRegisters = new Dictionary<Registers, Register>();

        private readonly FlagsRegister flagsRegister = new FlagsRegister();

        public Processor()
        {
            this.InitializeRegister(new List<Registers> { rax, eax, ax, al });
            this.InitializeRegister(new List<Registers> { rbx, ebx, bx, bl });
            this.InitializeRegister(new List<Registers> { rcx, ecx, cx, cl });
            this.InitializeRegister(new List<Registers> { rdx, edx, dx, dl });

            this.InitializeRegister(new List<Registers> { rbp, ebp, bp, bpl });

            this.InitializeRegister(new List<Registers> { rsi, esi, si, sil });
            this.InitializeRegister(new List<Registers> { rdi, edi, di, dil });

            this.InitializeRegister(new List<Registers> { rsp, esp, sp, spl });
        }

        private void InitializeRegister(List<Registers> names)
        {
            Register register = new Register();
            foreach (Registers name in names)
            {
                _generalPurposeRegisters.Add(name, register);
            }
        }

        public void Mov(Registers name, byte value)
        {
            if (_hRegisterNames.Contains(name))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[name];
                _generalPurposeRegisters[fullRegisterName].MovH(value);
            }
            else
            {
                _generalPurposeRegisters[name].Mov(value);
            }
        }

        public void Mov(Registers name, ushort value)
        {
            _generalPurposeRegisters[name].Mov(value);
        }

        public void Mov(Registers name, uint value)
        {
            _generalPurposeRegisters[name].Mov(value);
        }

        public void Mov(Registers name, ulong value)
        {
            _generalPurposeRegisters[name].Mov(value);
        }

        public void Inc(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[name];
                _generalPurposeRegisters[fullRegisterName].IncH();
            }
            else
            {
                _generalPurposeRegisters[name].Inc();
            }
        }

        public void Dec(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[name];
                _generalPurposeRegisters[fullRegisterName].DecH();
            }
            else
            {
                _generalPurposeRegisters[name].Dec();
            }
        }
    }
}

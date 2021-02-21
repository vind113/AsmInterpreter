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

        private Flags _flagsRegister = Flags.ZERO;

        public Processor()
        {
            this.InitializeRegister(new List<Registers> { rax, eax, ax, al });
            this.InitializeRegister(new List<Registers> { rbx, ebx, bx, bl });
            this.InitializeRegister(new List<Registers> { rcx, ecx, cx, cl });
            this.InitializeRegister(new List<Registers> { rdx, edx, dx, dl });
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
                ulong newValue = this._generalPurposeRegisters[fullRegisterName].Value & 0xFFFF_FFFF_FFFF_00FF | ((ulong)(value << 8));
                _generalPurposeRegisters[fullRegisterName].Value = newValue;
            }
            else
            {
                ulong newValue = (_generalPurposeRegisters[name].Value & 0xFFFF_FFFF_FFFF_FF00) | value;
                _generalPurposeRegisters[name].Value = newValue;
            }
        }

        public void Mov(Registers name, ushort value)
        {
            ulong newValue = (_generalPurposeRegisters[name].Value & 0xFFFF_FFFF_FFFF_0000) | value;
            _generalPurposeRegisters[name].Value = newValue;
        }

        public void Mov(Registers name, uint value)
        {
            ulong newValue = (_generalPurposeRegisters[name].Value & 0xFFFF_FFFF_0000_0000) | value;
            _generalPurposeRegisters[name].Value = newValue;
        }

        public void Mov(Registers name, ulong value)
        {
            _generalPurposeRegisters[name].Value = value;
        }

        public void Inc(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[name];
                _generalPurposeRegisters[fullRegisterName].Value += 1 << 8;
            }
            else
            {
                _generalPurposeRegisters[name].Value++;
            }
        }

        public void Dec(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[name];
                _generalPurposeRegisters[fullRegisterName].Value -= 1 << 8;
            }
            else
            {
                _generalPurposeRegisters[name].Value--;
            }
        }

        private void SetFlag(Flags flags)
        {
            _flagsRegister |= flags;
        }
    }
}

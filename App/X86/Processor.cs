using System;
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

        public void Mov(Registers destination, byte value)
        {
            if (_hRegisterNames.Contains(destination))
            {
                Registers fullRegisterName = _hRegisterNameToFullRegisterName[destination];
                _generalPurposeRegisters[fullRegisterName].MovH(value);
            }
            else
            {
                _generalPurposeRegisters[destination].Mov(value);
            }
        }

        public void Mov(Registers destination, ushort value)
        {
            _generalPurposeRegisters[destination].Mov(value);
        }

        public void Mov(Registers destination, uint value)
        {
            _generalPurposeRegisters[destination].Mov(value);
        }

        public void Mov(Registers destination, ulong value)
        {
            _generalPurposeRegisters[destination].Mov(value);
        }

        public void Mov(Registers destination, Registers source)
        {
            if (_8BitRegisterNames.Contains(source))
            {
                if (_hRegisterNames.Contains(destination))
                {
                    _generalPurposeRegisters[destination].MovH(Convert.ToByte(_generalPurposeRegisters[source].Value));
                }
                else
                {
                    _generalPurposeRegisters[destination].Mov(Convert.ToByte(_generalPurposeRegisters[source].Value));
                }
            }
            else if (_16BitRegisterNames.Contains(source))
            {
                _generalPurposeRegisters[destination].Mov(Convert.ToUInt16(_generalPurposeRegisters[source].Value));
            }
            else if (_32BitRegisterNames.Contains(source))
            {
                _generalPurposeRegisters[destination].Mov(Convert.ToUInt32(_generalPurposeRegisters[source].Value));
            }
            else if (_64BitRegisterNames.Contains(source))
            {
                _generalPurposeRegisters[destination].Mov(_generalPurposeRegisters[source].Value);
            } 
            else
            {
                throw new ArgumentException($"Source register \"{source}\" is not a valid register for this operation");
            }
        }

        public void Mov(Registers destination, byte[] value)
        {
            if (_8BitRegisterNames.Contains(destination) && value.Length == 1)
            {
                if (_hRegisterNames.Contains(destination))
                {
                    _generalPurposeRegisters[destination].MovH(Convert.ToByte(value));
                }
                else
                {
                    _generalPurposeRegisters[destination].Mov(Convert.ToByte(value));
                }
            }
            else if (_16BitRegisterNames.Contains(destination) && value.Length == 2)
            {
                _generalPurposeRegisters[destination].Mov(Convert.ToUInt16(value));
            }
            else if (_32BitRegisterNames.Contains(destination) && value.Length == 4)
            {
                _generalPurposeRegisters[destination].Mov(Convert.ToUInt32(value));
            }
            else if (_64BitRegisterNames.Contains(destination) && value.Length == 8)
            {
                _generalPurposeRegisters[destination].Mov(Convert.ToUInt64(value));
            }
            else
            {
                throw new ArgumentException($"Destination register \"{destination}\" and byte array \"{string.Join(',', value)}\" are not valid combination for this command");
            }
        }

        public void Inc(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                name = _hRegisterNameToFullRegisterName[name];
            }

            Register register = _generalPurposeRegisters[name];
            if (_hRegisterNames.Contains(name))
            {
                register.IncH();
            }
            else
            {
                register.Inc();
            }

            if (register.Value == 0)
            {
                flagsRegister.SetZeroFlag();
            }
        }

        public void Dec(Registers name)
        {
            if (_hRegisterNames.Contains(name))
            {
                name = _hRegisterNameToFullRegisterName[name];
            }

            Register register = _generalPurposeRegisters[name];
            if (_hRegisterNames.Contains(name))
            {
                register.DecH();
            }
            else
            {
                register.Dec();
            }

            if (register.Value == 0)
            {
                flagsRegister.SetZeroFlag();
            }
        }
    }
}

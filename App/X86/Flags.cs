using System;

namespace App.X86
{
    // https://ru.wikipedia.org/wiki/Регистр_флагов
    [Flags]
    public enum Flags : long
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
}

using System;
using System.Collections.Generic;
using System.Text;

namespace App.RAM
{
    public class Memory
    {
        private const int MEMORY_SIZE = 32 * 1024 * 1024;

        private readonly byte[] memory = new byte[MEMORY_SIZE];
    }
}

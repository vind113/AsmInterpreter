namespace App.X86
{
    class Register
    {
        public ulong Value { get; set; } = 0;

        public override string ToString()
        {
            return $"0x{Value:X16}";
        }
    }
}

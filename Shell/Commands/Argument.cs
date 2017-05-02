using System;

namespace Shell
{
    /// <summary>
    /// Represent entity for command's argument
    /// </summary>
    public class Argument: CommandLineObject
    {
        public String Content { get; }
        public TypeCode Type { get; }

        public Argument(String arg, TypeCode type)
        {
            Content = arg;
            Type = type;
            base.IsArgument = true;
        }
    }
}

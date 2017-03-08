using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// Represent entity for command's argument
    /// </summary>
    class Argument: CommandLineObject
    {
        public String Content { get; }
        public TypeCode Type { get; }

        public Argument(String arg, TypeCode type)
        {
            Content = arg;
            Type = type;
            base.isArgument = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Argument: CommandLineObject
    {
        public String content { get; }
        public TypeCode currentType { get; }

        public Argument(String arg, TypeCode type)
        {
            content = arg;
            currentType = type;
            base.isArgument = true;
        }
    }
}

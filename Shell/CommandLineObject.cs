﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class CommandLineObject
    {
        public virtual bool isCommand { get; set; }
        public virtual bool isArgument { get; set; }
    }
}

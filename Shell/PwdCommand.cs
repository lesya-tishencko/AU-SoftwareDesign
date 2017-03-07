using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shell
{
    class PwdCommand: Command
    {
        public PwdCommand(): base("pwd", Command.NoArgsCount) { }

        public override void CreateOutput()
        {
            base.output = Directory.GetCurrentDirectory();
            base.CreateOutput();
        }

        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
        }
    }
}

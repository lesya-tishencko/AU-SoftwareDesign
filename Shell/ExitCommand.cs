using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class ExitCommand: Command
    {
        public ExitCommand(): base("exit", NoArgsCount) { }

        public override void CreateOutput()
        {
            base.output = "";

            if (base.args.Count() != base.argsCount)
            {
                CreateError("Было задано " + base.args.Count() + " аргументов, данная команда проигнорирует их");
            }
        }

        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
            Environment.Exit(0);
        }
    }
}

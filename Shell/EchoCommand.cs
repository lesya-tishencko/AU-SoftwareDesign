using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class EchoCommand: Command
    {
        public EchoCommand(): base("echo", Command.EndlessArgsCount) { }

        public override void CreateOutput()
        {
            base.output = "";
            if (base.args.Count() == Command.NoArgsCount)
                CreateError("Аргументы не заданы");

            foreach (Argument arg in base.args)
            {
                if (arg.currentType != TypeCode.String)
                    continue;
                base.output += arg.content;
            }
            if (base.output == "")
                CreateError("Некорректный тип аргументов"); 
            else
                base.CreateOutput();
        }

        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
            base.args.Clear();
        }
    }
}

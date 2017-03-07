using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shell
{
    class CatCommand: Command
    {
        public CatCommand(): base("cat", Command.EndlessArgsCount) { }

        public override void CreateOutput()
        {
            base.output = "";
            if (base.args.Count() == Command.NoArgsCount)
                CreateError("Аргументы не заданы");

            foreach (Argument arg in base.args)
            {
                if (arg == null || arg.currentType != TypeCode.String)
                    continue;
                if (!(new FileInfo(arg.content).Exists))
                    continue;
                StreamReader file = new StreamReader(arg.content);
                base.output += file.ReadToEnd();
                file.Close();
            }

            if (base.output == "")
                CreateError("Файл не найден");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shell
{
    class WcCommand: Command
    {
        public WcCommand(): base("wc", 1) { }

        public override void CreateOutput()
        {
            base.output = "";
            if (base.args.Count() != base.argsCount)
                CreateError("Неправильно заданное количество аргументов, требуется " + base.argsCount + ", получено " + base.args.Count());

            if (base.args.First() == null || base.args.First().currentType == TypeCode.String)
            {
                String content = null;
                long bytes = 0;

                FileInfo fInfo = new FileInfo(base.args.First().content);
                
                if (fInfo.Exists)
                {
                    StreamReader file = new StreamReader(base.args.First().content);
                    content = file.ReadToEnd();
                    bytes = fInfo.Length;
                    file.Close();
                }
                else
                {
                    content = base.args.First().content;
                    bytes = content.Length;
                }
                
                long lines = content.Split('\n').Count();
                long words = content.Split(' ', '\n').Count();

                base.output = lines.ToString() + " " + words.ToString() + " " + bytes.ToString();

               base.CreateOutput();
            }
            else
                CreateError("Некорректный тип аргументов");
        }

        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
            base.args.Clear();
        }
    }
}

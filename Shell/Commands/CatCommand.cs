using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shell
{
    /// <summary>
    /// Command for printing file
    /// </summary>
    class CatCommand: Command
    {
        public CatCommand(): base("cat", Command.EndlessArgsCount) { }

        /// <summary>
        /// Opens all setted files, concat it's to one line 
        /// </summary>
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

        /// <summary>
        /// Executes command of printing files (prints files to console)
        /// </summary>
        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
            base.args.Clear();
        }
    }
}

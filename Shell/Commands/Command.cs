using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Shell
{ 
    class Command: CommandLineObject
    {
        public static int EndlessArgsCount = -1;
        public static int NoArgsCount = 0;

        protected List<Argument> args;
        protected int argsCount;
        protected String output;
        public String Name { get; }

        public Command(String name = "", int argsCount = 0)
        {
            Name = name;
            this.argsCount = argsCount;
            args = new List<Argument>();
            output = "";
            base.isCommand = true;
        }

        public void AddArgument(Argument arg)
        {
            args.Add(arg);
        }

        public virtual void CreateOutput()
        {
            ArgumentStorer.AddArgument(Name, new Argument(output, TypeCode.String));
        }

        protected virtual void CreateError(String message)
        {
            if (output != "")
                output += '\n';

            output += Name + ": " + message;
            ArgumentStorer.AddArgument(Name, new Argument("Error " + output, TypeCode.String));
        }

        public virtual void Execute()
        {
            String commandArgs = "";
            foreach (Argument arg in args)
            {
                if (arg.currentType != TypeCode.String)
                {
                    CreateError("Некорректный тип аргументов");
                    return;
                }
                commandArgs += arg.content;
            }
            try
            {
                Process unknownProcess = Process.Start(Name, commandArgs);
            }
            catch (System.Exception)
            {
                CreateError("Комманда не найдена");
                Console.WriteLine(output);
                args.Clear();
            }
           
        }

    }
}

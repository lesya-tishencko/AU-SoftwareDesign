using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Shell
{ 
    /// <summary>
    /// Represents entity for command execution
    /// </summary>
    public class Command: CommandLineObject
    {
        public const int EndlessArgsCount = -1;
        public const int NoArgsCount = 0;

        protected List<Argument> args = new List<Argument>();
        protected int argsCount;
        protected String output = "";
        public String Name { get; }

        public Command(String name = "", int argsCount = 0)
        {
            Name = name;
            this.argsCount = argsCount;
            base.IsCommand = true;
        }


        /// <summary>
        /// Adds argument to command
        /// </summary>
        public void AddArgument(Argument arg) => args.Add(arg);


        /// <summary>
        /// Creates output and writes it to storer
        /// </summary>
        public virtual void CreateOutput()
        {
            ArgumentStorer.Add(Name, new Argument(output, TypeCode.String));
            args.Clear();
        }

        /// <summary>
        /// Creates wrapper for error and writes it to storer
        /// </summary>
        protected virtual void CreateError(String message)
        {
            if (output != "")
            {
                output += '\n';
            }

            output += $"{Name}: {message}";
            ArgumentStorer.Add(Name, new Argument("Error " + output, TypeCode.String));
            args.Clear();
        }

        /// <summary>
        /// Executes current command
        /// </summary>
        public virtual void Execute()
        {
            /* Procedure Execute in ancestor class is used for execution unknown 
            command or signalization about command not found */
            String commandArgs = "";
            foreach (Argument arg in args)
            {
                if (arg.Type != TypeCode.String)
                {
                    CreateError("Некорректный тип аргументов");
                    return;
                }
                commandArgs += arg.Content;
            }
            try
            {
                Process unknownProcess = Process.Start(Name, commandArgs);
            }
            /* I can't find way to set execution of unknown programm  without catching exceptions */
            catch (System.ComponentModel.Win32Exception)
            {
                CreateError("Команда не найдена");
                Console.WriteLine(output);
                args.Clear();
            }
           
        }
    }
}

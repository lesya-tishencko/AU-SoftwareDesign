using System;
using System.Linq;

namespace Shell
{
    /// <summary>
    /// Command for exiting programm
    /// </summary>
    public class ExitCommand: Command
    {
        public ExitCommand(): base("exit", NoArgsCount) { }

        /// <summary>
        /// Checks absence of argument
        /// </summary>
        public override void CreateOutput()
        {
            base.output = "";

            if (base.args.Count() != base.argsCount)
            {
                CreateError("Было задано " + base.args.Count() + " аргументов, данная команда проигнорирует их");
            }
        }

        /// <summary>
        /// Executes command of exiting programm
        /// </summary>
        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Shell
{
    /// <summary>
    /// Command for printing current directory
    /// </summary>
    class PwdCommand: Command
    {
        public PwdCommand(): base("pwd", Command.NoArgsCount) { }

        /// <summary>
        /// Gets name of current directory
        /// </summary>
        public override void CreateOutput()
        {
            base.output = Directory.GetCurrentDirectory();
            base.CreateOutput();
        }

        /// <summary>
        /// Prints name of current directory to console
        /// </summary>
        public override void Execute()
        {
            CreateOutput();
            Console.WriteLine(base.output);
        }
    }
}

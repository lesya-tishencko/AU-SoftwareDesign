using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// Class, represents command with keys
    /// </summary>
    public class CommandWithKey: Command
    {
        public CommandWithKey(Command main, String key = "")
        {
            mainCommand = main;
            keyParam = key;
        }

        public override void AddArgument(Argument arg) => mainCommand.AddArgument(arg);

        public override void CreateOutput() => mainCommand.CreateOutput();

        public override void Execute() => mainCommand.Execute();

        /// <summary>
        /// Sets key's params
        /// </summary>
        public virtual void SetKeyParam(String param) => keyParam = param;

        protected Command mainCommand;
        protected String keyParam;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    static class Shell
    {
        static public void Start()
        {
            CommandStorer.AddCommand("pwd", new PwdCommand());
            CommandStorer.AddCommand("echo", new EchoCommand());
            CommandStorer.AddCommand("cat", new CatCommand());
            CommandStorer.AddCommand("wc", new WcCommand());
            CommandStorer.AddCommand("exit", new ExitCommand());

            while (true)
            {
                String input = Console.ReadLine();
                if (input == "")
                    continue;

                var comResult = (new Expression(input)).Interpret();
                if (comResult != null) {
                    Command currentCommand = comResult.First() as Command;
                    currentCommand.Execute();
                }
            }
        }
    }
}

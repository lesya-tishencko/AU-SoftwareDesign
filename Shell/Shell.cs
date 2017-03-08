using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// Represents main command line's entity
    /// </summary>
    public static class Shell
    {
        static public void InitDictionary()
        {
            CommandStorer.AddCommand("pwd", new PwdCommand());
            CommandStorer.AddCommand("echo", new EchoCommand());
            CommandStorer.AddCommand("cat", new CatCommand());
            CommandStorer.AddCommand("wc", new WcCommand());
            CommandStorer.AddCommand("exit", new ExitCommand());
        }

        static public void Start()
        {
            InitDictionary();
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

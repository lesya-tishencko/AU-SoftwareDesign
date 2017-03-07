using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    /// <summary>
    /// Represents store for named commands
    /// </summary>
    static class CommandStorer
    {
        private static IDictionary<String, Command> commandsDict = new Dictionary<String, Command>();

        public static void AddCommand(String key, Command value)
        {
            if (commandsDict.ContainsKey(key))
                commandsDict[key] = value;
            else
                commandsDict.Add(new KeyValuePair<String, Command>(key, value));
        }

        public static Command FindCommand(String key)
        {
            if (!commandsDict.ContainsKey(key))
                return null;

            return commandsDict[key];
        }
    }
}

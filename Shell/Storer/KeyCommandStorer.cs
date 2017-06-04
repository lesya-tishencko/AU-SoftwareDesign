using System;
using System.Collections.Generic;

namespace Shell
{
    /// <summary>
    /// Represents store for commands with keys
    /// </summary>
    public static class KeyCommandStorer
    {
        private static IDictionary<Command, IApplicationArguments> varDict 
            = new Dictionary<Command, IApplicationArguments>();

        public static void Add(Command keyCommand, IApplicationArguments args)
        {
            if (varDict.ContainsKey(keyCommand))
            {
                varDict[keyCommand] = args;
            }
            else {
                varDict.Add(new KeyValuePair<Command, IApplicationArguments>(keyCommand, args));
            }
        }

        public static IApplicationArguments Find(Command key) => varDict.ContainsKey(key) ? varDict[key] : null; 
    }
}

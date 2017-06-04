using System;
using System.Collections.Generic;

namespace Shell
{
    public static class KeyCommandStorer
    {
        private static IDictionary<Command, ApplicationArguments> varDict 
            = new Dictionary<Command, ApplicationArguments>();

        public static void Add(Command keyCommand, ApplicationArguments args)
        {
            if (varDict.ContainsKey(keyCommand))
            {
                varDict[keyCommand] = args;
            }
            else {
                varDict.Add(new KeyValuePair<Command, ApplicationArguments>(keyCommand, args));
            }
        }

        public static ApplicationArguments Find(Command key) => varDict.ContainsKey(key) ? varDict[key] : null; 
    }
}

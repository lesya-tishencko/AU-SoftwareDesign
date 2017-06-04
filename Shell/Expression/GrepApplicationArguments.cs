using CommandLine;

namespace Shell
{
    public class GrepApplicationArguments : IApplicationArguments
    {
        /// <summary>
        /// Class option for cli parser
        /// </summary>
        class Options
        {
            [Option('i', DefaultValue = false, Required = false)]
            public bool I { get; set; }

            [Option('w', DefaultValue = false, Required = false)]
            public bool W { get; set; }

            [Option('A', DefaultValue = 0)]
            public int A { get; set; }
        }

        Options option = new Options();

        /// <summary>
        /// Generates grep command with keys
        /// </summary>
        public Command generateCommand(Command main)
        {
            Command result = main;
            if (option.I)
                result = new IGrepCommand(result as GrepCommand);
            if (option.W)
            {
                var grepCom = result as GrepCommand;
                result = grepCom == null 
                    ? new WGrepCommand(result as GrepWithKeys) 
                    : new WGrepCommand(grepCom);
            }
                
            if (option.A > 0)
            {
                var grepCom = result as GrepCommand;
                var local = grepCom == null
                    ? new AGrepCommand(result as GrepWithKeys)
                    : new AGrepCommand(grepCom);

                local.SetKeyParam(option.A.ToString());
                result = local;
            }
            return result;
        }

        /// <summary>
        /// Tries to parse string with key and returns unparsed string
        /// </summary>
        public string parse(string arg)
        {
            var result = arg;
            var keyArgs = arg.Split();
            if (CommandLine.Parser.Default.ParseArguments(keyArgs, option))
            {
                if (option.A > 0)
                {
                    int sIndex = result.IndexOf("-A");
                    int trim = sIndex + 2;
                    while (result[trim] == ' ')
                        trim++;
                    int lIndex = result.IndexOf(' ', trim);
                    result = lIndex != -1
                        ? result.Remove(sIndex + 2, lIndex - sIndex - 1)
                        : result.Remove(sIndex + 2);
                    result = result.Replace("-A", "");
                }
                if (option.I)
                    result = result.Replace("-i", "");
                if (option.W)
                    result = result.Replace("-w", "");
            }
            return result;
        }
    }
}

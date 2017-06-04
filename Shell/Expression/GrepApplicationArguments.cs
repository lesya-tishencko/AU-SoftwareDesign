using CommandLine;

namespace Shell
{
    public class GrepApplicationArguments : ApplicationArguments
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
        public override Command generateCommand(Command main)
        {
            Command result = main;
            if (option.I)
                result = new iGrepCommand(result as GrepCommand);
            if (option.W)
            {
                var grepCom = result as GrepCommand;
                result = grepCom == null 
                    ? new wGrepCommand(result as GrepWithKeys) 
                    : new wGrepCommand(grepCom);
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
        public override string parse(string arg)
        {
            var result = arg;
            var keyArgs = arg.Split();
            if (CommandLine.Parser.Default.ParseArguments(keyArgs, option))
            {
                if (option.A > 0)
                {
                    int s_index = result.IndexOf("-A");
                    int trim = s_index + 2;
                    while (result[trim] == ' ')
                        trim++;
                    int l_index = result.IndexOf(' ', trim);
                    result = l_index != -1
                        ? result.Remove(s_index + 2, l_index - s_index)
                        : result.Remove(s_index + 2);
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

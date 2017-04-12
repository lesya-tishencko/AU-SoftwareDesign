using System.Text.RegularExpressions;

namespace Shell
{
    public class iGrepCommand: GrepWithKeys
    {
        public iGrepCommand(GrepCommand grep): base(grep) { }

        public override void CreateOutput()
        {
            grep.AdditionStart = "(?i)";
            base.CreateOutput();
        }
    }
}

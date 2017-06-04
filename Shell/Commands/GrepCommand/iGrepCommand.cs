using System.Text.RegularExpressions;

namespace Shell
{
    /// <summary>
    /// Grep command, matched any register
    /// </summary>
    public class iGrepCommand: GrepWithKeys
    {
        public iGrepCommand(GrepCommand grep): base(grep) {
            (mainCommand as GrepCommand).AdditionStart = "(?i)";
        }
        public iGrepCommand(GrepWithKeys grep): base(grep) {
            (mainCommand as GrepCommand).AdditionStart += "(?i)";
        }

        public override void CreateOutput()
        {
            base.CreateOutput();
            (mainCommand as GrepCommand).AdditionStart = "";
        }

        public override void Execute()
        {
            base.Execute();
            (mainCommand as GrepCommand).AdditionStart = "";
        }
    }
}

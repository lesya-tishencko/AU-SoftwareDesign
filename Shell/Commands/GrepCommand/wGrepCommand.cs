namespace Shell
{
    /// <summary>
    /// Grep command, matched only at words bound
    /// </summary>
    public class WGrepCommand: GrepWithKeys
    {
        public WGrepCommand(GrepCommand grep): base(grep) {
            (mainCommand as GrepCommand).AdditionStart = "\\b";
            (mainCommand as GrepCommand).AdditionEnd = "\\b";
        }

        public WGrepCommand(GrepWithKeys grep) : base(grep) {
            (mainCommand as GrepCommand).AdditionStart += "\\b";
            (mainCommand as GrepCommand).AdditionEnd += "\\b";
        }

        public override void CreateOutput()
        {
            base.CreateOutput();
            (mainCommand as GrepCommand).AdditionStart = "";
            (mainCommand as GrepCommand).AdditionEnd = "";
        }

        public override void Execute()
        {
            base.Execute();
            (mainCommand as GrepCommand).AdditionStart = "";
            (mainCommand as GrepCommand).AdditionEnd = "";
        }
    }
}

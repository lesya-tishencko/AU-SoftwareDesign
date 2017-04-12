namespace Shell
{
    public class wGrepCommand: GrepWithKeys
    {
        public wGrepCommand(GrepCommand grep): base(grep) { }

        public override void CreateOutput()
        {
            grep.AdditionStart = "\b";
            grep.AdditionEnd = "\b";
            base.CreateOutput();
        }
    }
}

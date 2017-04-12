namespace Shell
{
    public class GrepWithKeys: GrepCommand
    {
        public GrepWithKeys(GrepCommand grep)
        {
            this.grep = grep;
        }

        public override void CreateOutput()
        {
            grep.CreateOutput();
        }

        public override void Execute()
        {
            grep.Execute();
        }

        protected GrepCommand grep;
    }
}

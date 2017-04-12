namespace Shell
{
    public class AGrepCommand: GrepWithKeys
    {
        public AGrepCommand(GrepCommand grep, int n): base(grep) {
            this.n = n;
        }

        public override void CreateOutput()
        {
            grep.CountPrintedString = n;
            base.CreateOutput();
        }

        private int n;
    }
}

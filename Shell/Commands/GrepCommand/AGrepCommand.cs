namespace Shell
{
    /// <summary>
    /// Gpep command, printed n string after mathing
    /// </summary>
    public class AGrepCommand: GrepWithKeys
    {
        public AGrepCommand(GrepCommand grep) : base(grep) { }

        public AGrepCommand(GrepWithKeys grep): base(grep) { }

        public override void SetKeyParam(string param)
        {
            base.SetKeyParam(param);
            (mainCommand as GrepCommand).CountPrintedString = int.Parse(keyParam);
        }

        public override void CreateOutput()
        {
            base.CreateOutput();
            (mainCommand as GrepCommand).CountPrintedString = 0;
        }

        public override void Execute()
        {
            base.Execute();
            (mainCommand as GrepCommand).CountPrintedString = 0;
        }
    }
}

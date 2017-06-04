namespace Shell
{
    /// <summary>
    /// Command for grep with keys
    /// </summary>
    public class GrepWithKeys: CommandWithKey
    {
        public GrepWithKeys(GrepCommand grep): base(grep) { }

        public GrepWithKeys(GrepWithKeys grep) : base(grep.mainCommand) { }
    }
}

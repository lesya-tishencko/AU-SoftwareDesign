namespace Shell
{
    /// <summary>
    /// Abstract class for parsing commands with keys
    /// </summary>
    public abstract class ApplicationArguments {
        public abstract Command generateCommand(Command main);
        public abstract string parse(string arg);
    }
}

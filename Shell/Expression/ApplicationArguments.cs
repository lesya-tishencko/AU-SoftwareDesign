namespace Shell
{
    /// <summary>
    /// Interface for parsing commands with keys
    /// </summary>
    public interface IApplicationArguments {
        Command generateCommand(Command main);
        string parse(string arg);
    }
}

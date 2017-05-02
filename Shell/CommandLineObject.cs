namespace Shell
{
    /// <summary>
    /// Parent(Wrapper) for Command and Argument
    /// </summary>
    public class CommandLineObject
    {
        public virtual bool IsCommand { get; set; }
        public virtual bool IsArgument { get; set; }
    }
}

namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents active object
    /// </summary>
    public interface IActive
    {
        /// <summary>
        /// Set action for alive object
        /// </summary>
        bool Act(Core.Enemy monster, Systems.CommandSystem commandSystem);
    }
}

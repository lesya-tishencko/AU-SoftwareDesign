namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents active object
    /// </summary>
    public interface IActive
    {
        bool Act(Core.Enemy monster, Systems.CommandSystem commandSystem);
    }
}

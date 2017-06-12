using RogueSharp;
using RLNET;

namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents drawable object
    /// </summary>
    public interface IDrawable
    {
        RLColor Color { get; set; }
        char Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }

        /// <summary>
        /// Draw to map console
        /// </summary>
        void Draw(RLConsole console, IMap map);
    }
}

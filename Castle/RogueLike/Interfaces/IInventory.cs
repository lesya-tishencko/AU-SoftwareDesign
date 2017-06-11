namespace RogueLike.Interfaces
{
    /// <summary>
    /// Represents entity for attributes
    /// </summary>
    public interface IInventory
    {
        void PutOn();
        void TakeOff();

        string Name { get; }
        /// <summary>
        /// How many gold you need buy this attribute
        /// </summary>
        int Cost { get; }
        char Key { get; }
    }
}
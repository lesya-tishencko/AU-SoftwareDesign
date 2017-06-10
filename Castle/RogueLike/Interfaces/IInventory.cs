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
        // how many gold you need buy this attribute
        int Cost { get; }
        // time of wearing
        int MaxTime { get; } 
    }
}
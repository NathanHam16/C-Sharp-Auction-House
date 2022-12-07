namespace AuctionHouse
{

    /// <summary>
    /// All objects displayed implements this interface
    /// </summary>
    public interface InterfaceDisplay
    {

        /// <summary>
        /// The displayable object's title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Display the displayable object
        /// </summary>
        public void Display();
    }
}

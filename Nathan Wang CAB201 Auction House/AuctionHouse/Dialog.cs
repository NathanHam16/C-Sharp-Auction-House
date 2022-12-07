namespace AuctionHouse
{

    /// <summary>
    /// Abstract class for most displayable objects
    /// </summary>
    public abstract class Dialog : InterfaceDisplay
    {

        /// <summary>
        /// Implement title property from Interface
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Reference to user database, only accessible to derived and containing classes
        /// </summary>
        protected UserDatabase Database { get; }

        /// <summary>
        /// Reference to product database, only accessible to derived and containing classes
        /// </summary>
        protected ProductDatabase ProductDatabase { get; }

        /// <summary>
        /// Initialise Dialog with title and user database
        /// </summary>
        /// <param name="title">Title of menu displayed</param>
        /// <param name="database">Reference to the user database where user interactions take place</param>
        /// <param name="productDatabase">Reference to the product database where product interactions take place</param>
        public Dialog(string title, UserDatabase database, ProductDatabase productDatabase)
        {
            Title = title;
            Database = database;
            ProductDatabase = productDatabase;
        }

        /// <summary>
        /// Abstract implementation of display method from interface class
        /// </summary>
        public abstract void Display();
    }
}

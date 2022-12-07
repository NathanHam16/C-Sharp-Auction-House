namespace AuctionHouse
{
    /// <summary>
    /// Program entry point for Auction House
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            UserDatabase database = new UserDatabase();
            ProductDatabase productDatabase = new ProductDatabase();
            MainMenu menu = new MainMenu(database, productDatabase);
            menu.Display();
            database.Save();
            productDatabase.Save();
            Environment.Exit(0);
        }
    }
}

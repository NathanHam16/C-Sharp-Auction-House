using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Main menu for auction house inherited from menu class
    /// </summary>
    public class MainMenu : Menu
    {
        public const string TITLE = "Main Menu";
        private const string EXIT = "Exit";

        /// <summary>
        /// Initialises new MainMenu object and displays welcome message, uses display method from Menu
        /// </summary>
        /// <param name="database">Reference to user database</param>
        /// <param name="productDatabase">Reference to product database</param>
        public MainMenu(UserDatabase database, ProductDatabase productDatabase) : base(TITLE, database, productDatabase, 
            new RegisterMenu(database, productDatabase), 
            new SignInMenu(database, productDatabase), 
            new ExitDialog(EXIT))
        {
            Welcome();
        }

        /// <summary>
        /// Simple welcome method which is only run at the start, therefore encapsulated
        /// </summary>
        private void Welcome()
        {
            WriteLine("+------------------------------+");
            WriteLine("| Welcome to the Auction House |");
            WriteLine("+------------------------------+");
        }

    }
}

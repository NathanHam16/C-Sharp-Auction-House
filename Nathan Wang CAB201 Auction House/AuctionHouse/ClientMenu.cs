using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Client Menu for auction house derived from menu
    /// </summary>
    public class ClientMenu : Menu
    {
        private const string TITLE = "Client Menu";
        private const string LOGOFF = "Log off";

        /// <summary>
        /// Initialises new Client Menu object, uses display method from Menu
        /// </summary>
        /// <param name="database">Reference to the user database where user interactions take place</param>
        /// <param name="user">Reference to the user class</param>
        /// <param name="productDatabase">Reference to the product database where product interactions take place</param>
        public ClientMenu(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase,
            new AdvertiseProduct(database, user, productDatabase),
            new ProductList(database, user, productDatabase),
            new ProductSearch(database, user, productDatabase),
            new ViewBids(database, user, productDatabase),
            new ViewPurchases(database, user, productDatabase),
            new LogOutDialog(LOGOFF))
        {
            
        }
    }
}

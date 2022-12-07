using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Product list class derived from menu, accesses user information
    /// </summary>
    public class ProductList : Menu
    {
        private const string TITLE = "View My Product List";
        private string name;
        private string email;
        private const string Error = "You have no advertised products at the moment.";

        /// <summary>
        /// Initialise the class
        /// </summary>
        /// <param name="database">Reference to database for user</param>
        /// <param name="user">User class</param>
        /// <param name="productDatabase">Product database</param>
        public ProductList(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
            name = user.Name;
            email = user.Email;
        }

        /// <summary>
        /// Implements InterfaceDisplay to display advertised products from logged in user.
        /// Uses email to fetch products under that email in product class.
        /// </summary>
        public override void Display()
        {
            WriteLine();
            WriteLine("Product List for {0}({1})", name, email);
            string MenuFillerCustom = new string('-', 19 + name.Length + email.Length);
            WriteLine(MenuFillerCustom);

            List<Product> products = ProductDatabase.UserProducts(email);

            ProductDatabase.DisplayProducts(products, Error, 1);
        }        
    }
}

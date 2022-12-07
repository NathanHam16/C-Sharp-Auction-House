using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Advertise Product class derived from menu
    /// </summary>
    public class AdvertiseProduct : Menu
    {
        private const string TITLE = "Advertise Product";
        private string name;
        private string email;
        private string bidName;
        private string bidPrice;
        private string bidEmail;

        /// <summary>
        /// Initialise the new object
        /// </summary>
        /// <param name="database">Reference to the user database where user interactions take place</param>
        /// <param name="user">Reference to the user class</param>
        /// <param name="productDatabase">Reference to the product database where product interactions take place</param>
        public AdvertiseProduct(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
            name = user.Name;
            email = user.Email;
        }

        /// <summary>
        /// Implements InterfaceDisplay to display advertised products from database.
        /// </summary>
        public override void Display()
        {
            WriteLine();
            WriteLine("Product Advertisement for {0}({1})", name, email);
            string MenuFillerCustom = new string('-', 28 + name.Length + email.Length);
            WriteLine(MenuFillerCustom);

            WriteLine();
            Advertise(out string productName, out string productDescription, out string currency);

            bidName = "-";
            bidPrice = "-";
            bidEmail = "-";
            ProductDatabase.CreateProduct(email, productName, productDescription, currency, bidName, bidEmail, bidPrice);
            WriteLine();
            WriteLine("Successfully added product {0}, {1}, {2}.", productName, productDescription, currency);
        }

        /// <summary>
        /// Advertises the product and validates user input, then stores it to ProductDatabase with email as ID (since email is unique)
        /// </summary>
        /// <param name="productName">Product name of particular product</param>
        /// <param name="productDescription">Product description</param>
        /// <param name="currency">List price of product</param>
        private void Advertise(out string productName, out string productDescription, out string currency)
        {
            string currencyError = "      A currency value is required, e.g. $54.95, $9.99, 2314.15.";

            Name(out productName);

            while(true)
            {
                Description(out productDescription);

                if (productDescription == productName)
                {
                    WriteLine("        Product description must be different from the product name.");
                    WriteLine();
                }
                else break;
            }

            while(true)
            {
                WriteLine("Product price ($d.cc)");
                Write("> ");
                currency = ReadLine();

                VerifyCurrency verifyCurrency = new VerifyCurrency();
                if (verifyCurrency.Verify(currency)) break;
                WriteLine(currencyError);
                WriteLine();
            }
        }

        /// <summary>
        /// Validates name input using from verifyInput class, calls verifyAddress StringVerify method to reduce code written.
        /// </summary>
        /// <param name="name">Product name</param>
        private void Name(out string name)
        {
            string Name = "Product name";
            VerifyAddress verifyAddress = new VerifyAddress();
            verifyAddress.StringVerify(Name, Name, Prompt, out name);

        }

        /// <summary>
        /// Validates product description input by calling StringVerify
        /// </summary>
        /// <param name="description">Product description</param>
        private void Description(out string description)
        {
            string Description = "Product description";
            VerifyAddress verifyAddress = new VerifyAddress();
            verifyAddress.StringVerify(Description, Description, Prompt, out description);
        }
    }
}

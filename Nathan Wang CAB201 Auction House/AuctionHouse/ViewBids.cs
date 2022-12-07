using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// ViewBids class derived from menu
    /// </summary>
    public class ViewBids : Menu
    {
        private string name;
        private string email;
        private const string TITLE = "View Bids On My Products";
        private const string Error = "No bids were found.";

        /// <summary>
        /// Initialise the view bids object
        /// </summary>
        public ViewBids(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
            name = user.Name;
            email = user.Email;
        }

        /// <summary>
        /// Implements InterfaceDisplay to display bids on user's products
        /// </summary>
        public override void Display()
        {
            List<Product> products = ProductDatabase.UserProducts(email);
            List<Product> productBids = new List<Product>();

            WriteLine();
            WriteLine("List Product Bids for {0}({1})", name, email);
            string MenuFillerCustom = new string('-', 24 + name.Length + email.Length);
            WriteLine(MenuFillerCustom);

            foreach (Product product in products)
            {
                if (product.BidEmail != "-") 
                {
                    productBids.Add(product);

                }
            }

            ProductDatabase.DisplayProducts(productBids, Error, 1);

            if (productBids.Count > 0)
            {
                SellPrompt(productBids, productBids.Count(), out Product product);

                if (product != null)
                {
                    ProductDatabase.CreatePurchase(email, product.BidEmail, product.ProductName, product.ProductDescription, product.Price, product.BidPrice, product.DeliveryOption);

                    ProductDatabase.RemoveProduct(product);
                }
            }
        }

        /// <summary>
        /// Prompts the user to sell something in their view bids menu.
        /// </summary>
        /// <param name="products">List of products which have a bid on them</param>
        /// <param name="optionsCount">Number of options int</param>
        /// <param name="product">Product which user chooses to sell</param>
        private void SellPrompt(List<Product> products, int optionsCount, out Product product)
        {
            while(true)
            {
                WriteLine();
                WriteLine("Would you like to sell something (yes or no)?");
                Write(Prompt);
                string option = ReadLine();

                if (option.ToLower() == "yes")
                {
                    while(true)
                    {
                        VerifyCurrency verifyCurrency = new VerifyCurrency();
                        if (verifyCurrency.IntValidate(optionsCount, out int inputOption))
                        {
                            product = products[inputOption];
                            WriteLine();
                            WriteLine("You have sold {0} to {1} for {2}.", product.ProductName, product.BidName, product.BidPrice);
                            break;
                        }
                    }
                    break;
                }

                if (option.ToLower() == "no")
                {
                    product = null;
                    break;
                }

                else WriteLine("        Must be yes or no");
            }
        }
    }
}
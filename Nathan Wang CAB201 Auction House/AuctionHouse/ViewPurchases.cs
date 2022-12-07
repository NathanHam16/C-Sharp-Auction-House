using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Purchases class derived from menu, accesses user purchased items
    /// </summary>
    public class ViewPurchases : Menu
    {
        private const string TITLE = "View My Purchased Items";
        private string name;
        private string email;
        private const  string Error = "You have no purchased products at the moment.";
        int productNumber = 1;

        /// <summary>
        /// Initialise the class
        /// </summary>
        public ViewPurchases(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
            name = user.Name;
            email = user.Email;
        }

        /// <summary>
        /// Implements InterfaceDisplay to display user's purchased items
        /// </summary>
        public override void Display()
        {
            List<Purchase> purchases = ProductDatabase.UserPurchases(email);

            WriteLine();
            WriteLine("Purchased Items for {0}({1})", name, email);
            string MenuFillerCustom = new string('-', 18 + name.Length + email.Length);
            WriteLine(MenuFillerCustom);

            DisplayProducts(purchases);
        }

        /// <summary>
        /// Displays purchased products
        /// </summary>
        /// <param name="purchases">List of purchased products</param>
        private void DisplayProducts(List<Purchase> purchases)
        {
            if (purchases.Count == 0)
            {
                WriteLine();
                WriteLine(Error);
            }
            else
            {
                WriteLine();
                WriteLine("Item #   Seller email    Product name    Description    List price   Amt paid    Deliver option");

                purchases = purchases.OrderBy(i => i.ProductName).ThenBy(s => s.ProductDescription).ThenBy(s => s.Price).ToList();

                foreach (Purchase purchase in purchases)
                {
                    Write(productNumber + "        ");
                    Write(purchase.SellerEmail + "    ");
                    Write(purchase.ProductName + "    ");
                    Write(purchase.ProductDescription + "    ");
                    Write(purchase.ListPrice + "    ");
                    Write(purchase.Price + "    ");
                    WriteLine(purchase.DeliveryOption);

                    productNumber++;
                }
            }
        }
    }
}

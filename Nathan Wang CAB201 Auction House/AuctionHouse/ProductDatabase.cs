using static System.Console;

namespace AuctionHouse
{

    /// <summary>
    /// Database for products. Products and user databases are not loaded with one method because they have separate functions.
    /// The only relation between the two are the email, so keeping them separate reduces unnecessary complexity.
    /// </summary>
    public class ProductDatabase
    {
        private const string InvalidData = "Invalid Data or Input"; 

        /// <summary>
        /// Name of database file
        /// </summary>
        private const string FileName = "products.txt";

        /// <summary>
        /// List of product details
        /// </summary>
        public List<Product> products = new List<Product>();
        public List<Purchase> purchases = new List<Purchase>();

        /// <summary>
        /// Initialise database
        /// </summary>
        public ProductDatabase()
        {
            Load();
        }

        ///Creates a new product with parameters from product class
        public Product CreateProduct(string email, string productName, string productDescription, string price, string bidName, string bidEmail, string bidPrice)
        {
            Product product = new Product(email, productName, productDescription, price, bidName, bidEmail, bidPrice, "");
            products.Add(product);
            return product;
        }

        /// Creates a new purchase with parameters from purchase class
        public Purchase CreatePurchase(string sellerEmail, string ownerEmail, string productName, string productDescription, string listPrice, string price, string deliverOption)
        {
            Purchase purchase = new Purchase(sellerEmail, ownerEmail, productName, productDescription, listPrice, price, deliverOption);
            purchases.Add(purchase);
            return purchase;
        }

        /// <summary>
        /// Encapsulates loading products from file into user class
        /// </summary>
        private void Load()
        {
            if (File.Exists(FileName))
            {
                using StreamReader reader = new StreamReader(FileName);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    if (line == "Product") LoadProducts(reader);
                    if (line == "Purchase") LoadPurchases(reader);
                }
            }
            else Console.Error.WriteLine(InvalidData);
        }

        /// <summary>
        /// Uses text reader to parse product details
        /// </summary>
        private void LoadProducts(TextReader reader)
        {
            string email = reader.ReadLine();
            string productName = reader.ReadLine();
            string productDescription = reader.ReadLine();
            string price = reader.ReadLine();
            string bidName = reader.ReadLine();
            string bidEmail = reader.ReadLine();
            string bidPrice = reader.ReadLine();
            string deliveryOption = reader.ReadLine();
            products.Add(new Product(email, productName, productDescription, price, bidName, bidEmail, bidPrice, deliveryOption));
        }

        /// <summary>
        /// Parses purchase details
        /// </summary>
        private void LoadPurchases(TextReader reader)
        {
            string sellerEmail = reader.ReadLine();
            string ownerEmail = reader.ReadLine();
            string productName = reader.ReadLine();
            string productDescription = reader.ReadLine();
            string listPrice = reader.ReadLine();
            string price = reader.ReadLine();
            string deliverOption = reader.ReadLine();
            purchases.Add(new Purchase(sellerEmail, ownerEmail, productName, productDescription, listPrice, price, deliverOption));
        }

        /// <summary>
        /// Saves data to a text file, able to be parsed by load
        /// </summary>
        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                foreach (Product product in products)
                {
                    writer.WriteLine("Product");
                    writer.WriteLine(product.Email);
                    writer.WriteLine(product.ProductName);
                    writer.WriteLine(product.ProductDescription);
                    writer.WriteLine(product.Price);
                    writer.WriteLine(product.BidName);
                    writer.WriteLine(product.BidEmail);
                    writer.WriteLine(product.BidPrice);
                    writer.WriteLine(product.DeliveryOption);
                }
                foreach (Purchase purchase in purchases)
                {
                    writer.WriteLine("Purchase");
                    writer.WriteLine(purchase.SellerEmail);
                    writer.WriteLine(purchase.Email);
                    writer.WriteLine(purchase.ProductName);
                    writer.WriteLine(purchase.ProductDescription);
                    writer.WriteLine(purchase.ListPrice);
                    writer.WriteLine(purchase.Price);
                    writer.WriteLine(purchase.DeliveryOption);
                }
                writer.Close();
            }
        }

        /// <summary>
        /// Fetches product class belonging to particular email, returns product list belonging to user
        /// </summary>
        /// <returns>List of user products belonging to particular user</returns>
        public List<Product> UserProducts(string email)
        {
            List <Product> userProducts = new List <Product>();

            foreach (Product product in products)
            {
                if (product.EmailMatches(email))
                {
                    userProducts.Add(product);
                }
            }

            if (userProducts != null) return userProducts;
            else return null;
        }

        /// <summary>
        /// Fetches product list not belonging to user's email
        /// </summary>
        /// <returns>List of user products not belonging to user</returns>
        public List<Product> NotUserProducts(string email)
        {
            List<Product> notUserProducts = new List<Product>();

            foreach (Product product in products)
            {
                if (!product.EmailMatches(email))
                {
                    notUserProducts.Add(product);
                }
            }

            if (notUserProducts != null) return notUserProducts;
            else return null;
        }

        /// <summary>
        /// Returns list of user purchases
        /// </summary>
        /// <param name="email">user's email</param>
        /// <returns></returns>
        public List<Purchase> UserPurchases(string email)
        {
            List<Purchase> userPurchases = new List<Purchase>();

            foreach (Purchase purchase in purchases)
            {
                if (purchase.Matches(email))
                {
                    userPurchases.Add(purchase);
                }
            }
            if (userPurchases != null) return userPurchases;
            else return null;
        }

        /// <summary>
        /// Displays products in product search, viewbids, product list.
        /// </summary>
        /// <param name="products">List of products being displayed</param>
        /// <param name="Error">Error message</param>
        /// <param name="productNumber">product number to beigin incrementing from</param>
        /// <returns></returns>
        public Product DisplayProducts(List<Product> products, string Error, int productNumber)
        {
            if (products.Count == 0)
            {
                WriteLine();
                WriteLine(Error);
            }

            else
            {
                WriteLine();
                WriteLine("Item #   Product name    Description    List price   Bidder name    Bidder email    Bid amt");

                products = products.OrderBy(i => i.ProductName).ThenBy(s => s.ProductDescription).ThenBy(s => s.Price).ToList();

                foreach (Product product in products)
                {
                    Write(productNumber + "        ");
                    Write(product.ProductName + "       ");
                    Write(product.ProductDescription + "     ");
                    Write(product.Price + "    ");
                    Write(product.BidName + "   ");
                    Write(product.BidEmail + "   ");
                    WriteLine(product.BidPrice);

                    productNumber++;
                }
            }
            return null;
        }

        public Product RemoveProduct(Product product)
        {
            products.Remove(product);
            return null;
        }
    }
}

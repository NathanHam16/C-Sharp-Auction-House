using static System.Console;
using System.Globalization;

namespace AuctionHouse
{
    /// <summary>
    /// ProductSearch class derived from menu, accesses all products
    /// </summary>
    public class ProductSearch : Menu
    {
        private const string TITLE = "Search For Advertised Products";
        private string name;
        private string email;
        private const string SearchPrompt = "Please supply a search phrase (ALL to see all products)";
        private string searchPhrase;
        private const string TimePrompt = "Delivery window {0} (dd/mm/yyyy hh:mm)";
        DateTime timeStart;
        DateTime timeEnd;

        /// <summary>
        /// Initialise the Product Search object
        /// </summary>
        /// <param name="database">Database of users</param>
        /// <param name="user">Class for individual user</param>
        /// <param name="productDatabase">Database of products</param>
        public ProductSearch(UserDatabase database, User user, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
            name = user.Name;
            email = user.Email;
        }

        /// <summary>
        /// Implements InterfaceDisplay to display advertised products from whole database, apart from the user's own
        /// </summary>
        public override void Display()
        {
            // The first list uses the Not User products method to return a list of products which do not belong to the current user.
            // The second list duplucates the first list for filtering.
            List<Product> products = ProductDatabase.NotUserProducts(email);
            products = products.OrderBy(i => i.ProductName).ThenBy(s => s.ProductDescription).ThenBy(s => s.Price).ToList();
            List<Product> productsDuplicate = ProductDatabase.NotUserProducts(email);
            productsDuplicate = products.OrderBy(i => i.ProductName).ThenBy(s => s.ProductDescription).ThenBy(s => s.Price).ToList();

            WriteLine();
            WriteLine("Product Search for {0}({1})", name, email);
            string MenuFillerCustom = new string('-', 21 + name.Length + email.Length);
            WriteLine(MenuFillerCustom);
            WriteLine();

            ProcessInput(products, productsDuplicate, out int optionsCount);

            if (optionsCount != 0)
            {
                ProcessBid(products, optionsCount, out int inputOption);
            }
        }

        /// <summary>
        /// Filters list of products based on user input (search phrase)
        /// </summary>
        /// <param name="products">Products which do not belong to the logged in user</param>
        /// <param name="productsDuplicate">Products after being filtered</param>
        private void ProcessInput(List<Product> products, List<Product> productsDuplicate, out int optionsCount)
        {
            while(true)
            {
                VerifyAddress verifyAddress = new VerifyAddress();
                verifyAddress.StringVerify(SearchPrompt, "Please supply a search phrase (ALL to see all products)", Prompt, out searchPhrase);
                WriteLine("Search Results");
                WriteLine("--------------");

                if (searchPhrase == "ALL")
                {
                    const string Error = "There are no advertised products at the moment.";
                    ProductDatabase.DisplayProducts(products, Error, 1);
                    WriteLine();
                    optionsCount = products.Count();
                    break;
                }

                // if product name contains search phrase then add to copied list and display.
                else
                {
                    const string Error = "There are no advertised products which match your search criteria.";
                    products.Clear();

                    foreach (Product product in productsDuplicate)
                    {
                        if (product.ProductName.Contains(searchPhrase))
                        { 
                            products.Add(product);
                        }
                        else if (product.ProductDescription.Contains(searchPhrase))
                        {
                            products.Add(product);
                        }
                    }
                    ProductDatabase.DisplayProducts(products, Error, 1);
                    optionsCount = products.Count();
                    WriteLine();
                }
                if (products.Count > 0)
                {
                    WriteLine();
                    break;
                }
            }
        }

        /// <summary>
        /// Method which prompts and processes the user bid (If applicable)
        /// </summary>
        /// <param name="products">List of products after being filtered</param>
        /// <param name="optionsCount">Counts the number of options supplied</param>
        /// <param name="inputOption">Outputs input option</param>
        private void ProcessBid(List<Product> products, int optionsCount, out int inputOption)
        {
            while(true)
            {
                WriteLine("Would you like to place a bid on any of these items (yes or no)?");
                Write(Prompt);
                string option = ReadLine();

                if (option.ToLower() == "yes")
                {
                    BidDetails(products, optionsCount, out Product product, out string currency, out inputOption);

                    product.BidName = name;
                    product.BidEmail = email;
                    product.BidPrice = currency;
                    break;

                }

                else if (option.ToLower() == "no")
                {
                    inputOption = 0;
                    break;
                }

                else
                {
                    WriteLine("        Must be yes or no");
                    Write(Prompt);

                }
            }
        }

        /// <summary>
        /// Processes the Bid Details and outputs necessary variables to place a bid
        /// </summary>
        /// <param name="product">Product which the user selected</param>
        /// <param name="currency">Price which the user bids on the product</param>
        private void BidDetails(List<Product> products, int optionsCount, out Product product, out string currency, out int inputOption)
        {
            string productName;
            string productPrice;
            string productBid;

            while (true)
            {
                VerifyCurrency verifyCurrency = new VerifyCurrency();
                if (verifyCurrency.IntValidate(optionsCount, out inputOption))
                {
                    productName = products[inputOption].ProductName;
                    productPrice = products[inputOption].Price;
                    productBid = products[inputOption].BidPrice;
                    product = products[inputOption];

                    if (productBid == "-") productBid = "0.00";

                    break;
                }
            }

            while(true)
            {
                WriteLine();
                WriteLine("Bidding for {0} (regular price {1}), current highest bid ${2}.", productName, productPrice, productBid); 
                WriteLine();
                WriteLine("How much do you bid?");

                Write(Prompt);
                currency = ReadLine();

                VerifyCurrency verifyCurrency = new VerifyCurrency();
                if (verifyCurrency.Verify(currency))
                {
                    decimal value = decimal.Parse(currency, NumberStyles.Currency);
                    decimal currentBid = decimal.Parse(productBid, NumberStyles.Currency);

                    if (value > currentBid)
                    {
                        WriteLine();
                        WriteLine("Your bid of {0} for {1} is placed.", currency, productName);
                        DeliveryPrompt(out string deliverOption);
                        products[inputOption].DeliveryOption = deliverOption;
                        break;
                    }
                    WriteLine("        Bid amount must be greater than {0}", currency);
                }
                else WriteLine("      A currency value is required, e.g. $54.95, $9.99, 2314.15.");
            }
        }


        /// <summary>
        /// This does not use the menu method because seperate classes for click and collect and home delivery are simple enough that they do not need to be created.
        /// </summary>
        /// <param name="deliverOption">String deliverOption which is added to purchase class</param>
        private void DeliveryPrompt(out string deliverOption)
        {
            WriteLine();
            WriteLine("Delivery Instructions");
            WriteLine("---------------------");
            WriteLine("(1) Click and Collect");
            WriteLine("(2) Home Delivery");

            while (true)
            {
                WriteLine();
                WriteLine("Please select an option between 1 and 2");
                Write(Prompt);

                ProcessDeliveryOption(out int opt, 1, 2);

                if (opt == 1)
                {
                    while (true)
                    {
                        WriteLine();
                        WriteLine(TimePrompt, "start");
                        Write(Prompt);
                        string input = ReadLine();
                        VerifyTime verifyTime = new VerifyTime();
                        if (verifyTime.Verify(input))
                        {
                            timeStart = DateTime.Parse(input);
                            break;
                        }
                    }

                    while (true)
                    {
                        WriteLine();
                        WriteLine(TimePrompt, "end");
                        Write(Prompt);
                        string input = ReadLine();
                        VerifyTime verifyTime = new VerifyTime();
                        if (verifyTime.VerifyEnd(input))
                        {
                            timeEnd = DateTime.Parse(input);
                            break;
                        }
                    }
                    TimeSpan timeOfDay = timeStart.TimeOfDay;
                    string dateStart = timeStart.Date.ToString("dd/MM/yyyy");
                    TimeSpan timeOfDayEnd = timeEnd.TimeOfDay;
                    string dateEnd = timeEnd.Date.ToString("dd/MM/yyyy");

                    WriteLine();
                    WriteLine("Thank you for your bid. If successful, the item will be provided via collection between {0} on {1} and {2} on {3}", timeOfDay, dateStart, timeOfDayEnd, dateEnd);
                    deliverOption = $"Collection between {timeOfDay} on {dateStart} and {timeOfDayEnd} on {dateEnd}.";
                    break;
                }

                if (opt == 2)
                {
                    string[] addressdetails;
                    string unit;
                    string streetNumber;
                    string streetName;
                    string streetSuffix;
                    string city;
                    string state;
                    string postCode;

                    WriteLine();
                    WriteLine("Please provide your delivery address.");

                    while (true)
                    {
                        WriteLine();
                        WriteLine("Unit number (0 = none):");
                        Write(Prompt);

                        VerifyAddress verifyAddress = new VerifyAddress();
                        verifyAddress.Verify(Prompt);
                        verifyAddress.Fetch(out string address);
                        Database.UpdateUser(email, address, "address");

                        addressdetails = address.Split(',');
                        unit = addressdetails[0];
                        streetNumber = addressdetails[1];
                        streetName = addressdetails[2];
                        streetSuffix = addressdetails[3];
                        city = addressdetails[4];
                        state = addressdetails[5];
                        postCode = addressdetails[6];

                        WriteLine("Thank you for your bid. If successful, the item will be provided via delivery to {0}{1} {2} {3}, {4} {5} {6}", unit, streetNumber, streetName, streetSuffix, city, state.ToUpper(), postCode);
                        deliverOption = $"Delivery to {unit}{streetNumber} {streetName} {streetSuffix}, {city} {state} {postCode}.";
                        break;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Processes the delivery option validation
        /// </summary>
        /// <param name="option">user input</param>
        /// <param name="low">lower boundary</param>
        /// <param name="high">upper boundary</param>
        private void ProcessDeliveryOption(out int option, int low, int high)
        {
            while(true)
            {
                string userInput = ReadLine();

                if (int.TryParse(userInput, out option) && option >= low && option <= high) break;

                else WriteLine("The supplied value is not a valid input.");
            }
        }
    }
}
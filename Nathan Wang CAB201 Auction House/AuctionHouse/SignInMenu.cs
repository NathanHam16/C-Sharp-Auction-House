using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Sign In dialog derived from menu class
    /// </summary>
    public class SignInMenu : Menu
    {
        private const string TITLE = "Sign In";
        private const string InvalidLogin = "      This {0} does not match our records.";

        /// <summary>
        /// Initialise menu object 
        /// </summary>
        /// <param name="database">Reference to user database</param>
        /// <param name="productDatabase">Product database</param>
        public SignInMenu(UserDatabase database, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
        }

        /// <summary>
        /// Override Display method from Dialog
        /// </summary>
        public override void Display()
        {
            DisplayMenu(TITLE);

            Email(out string email);

            Password(email, out string name, out User user);

            if (user.Address == "")
            {
                AddressCollection(name, email);
            }

            Menu clientMenu = new ClientMenu(Database, user, ProductDatabase);
            clientMenu.Display();
        }

        /// <summary>
        /// Private email method which prompts and processes email input
        /// </summary>
        private void Email(out string email)
        {
            while(true)
            {
                DisplaySinglePrompt("email address");

                email = ReadLine();

                if (Database.EmailExists(email) == null)
                {
                    WriteLine(InvalidLogin, "email");
                }

                else break;
            }
        }

        /// <summary>
        /// Private password method which prompts and verifies password matches account detail
        /// </summary>
        private void Password(string email, out string name, out User user)
        {
            while (true)
            {
                DisplaySinglePrompt("password");

                string password = ReadLine();

                user = Database.PasswordMatch(email, password);

                if (user == null)
                {
                    WriteLine(InvalidLogin, "password");
                }
                else
                {
                    name = user.Name;
                    break;
                }                
            }
        }

        /// <summary>
        /// Private method which prompts the user for their address. This could not be seperated in it's own class, since it needs to access the database and the menu classes.
        /// Creating its own class and deriving it from the database requires a new instance of the database to be created with reference to the database.
        /// </summary>
        private void AddressCollection(string name, string email)
        {
            DisplayAddress(name ,email);

            Address(email);
        }

        /// <summary>
        /// Fetches and processes user input
        /// </summary>
        private void Address(string email)
        {
            WriteLine("Please provide your home address.");
            string[] addressdetails;
            string unit;
            string streetNumber;
            string streetName;
            string streetSuffix;
            string city;
            string state;
            string postCode;

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

                WriteLine("Address has been updated to {0}{1} {2} {3}, {4} {5} {6}", unit, streetNumber, streetName, streetSuffix, city, state.ToUpper(), postCode);

                break; 
            }
        }

        /// <summary>
        /// Displays address title with customised MenuFiller length
        /// </summary>
        private void DisplayAddress(string name, string email)
        {
            const string TITLE = "Personal Details for {0}({1})";
            const string MenuFiller = "-----------------------{0}";
            int UserDetailsLength;

            // The length of user's details are implemented in the string length 
            // for the MenuFiller to ensure that both are the same length.
            UserDetailsLength = name.Length + email.Length;
            string MenuFillerCustom = new string('-', UserDetailsLength);

            WriteLine();
            WriteLine(TITLE, name, email);
            WriteLine(MenuFiller, MenuFillerCustom);
            WriteLine();
        }
    }
}

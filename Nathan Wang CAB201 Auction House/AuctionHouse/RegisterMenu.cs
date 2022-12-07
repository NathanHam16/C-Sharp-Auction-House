using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Register dialog inherited from menu class to use its methods and variables
    /// </summary>
    public class RegisterMenu : Menu
    {
        private const string TITLE = "Register";
        private const string MenuTitle = "Registration";
        private const string Success = "Client {0}({1}) has successfully registered at the Auction House.";

        /// <summary>
        /// Initialise the dialog object
        /// </summary>
        /// <param name="database">User database</param>
        /// <param name="productDatabase">Product Database</param>
        public RegisterMenu(UserDatabase database, ProductDatabase productDatabase) : base(TITLE, database, productDatabase)
        {
        }

        /// <summary>
        /// Implements InterfaceDisplay and creates new user, overrides display method from inherited class
        /// </summary>
        public override void Display()
        {
            DisplayMenu(MenuTitle);

            Name(out string name);

            Email(out string email);

            Password(out string password);

            WriteLine();
            WriteLine(Success, name, email);
            
            Database.CreateUser(name, email, password);
        }

        /// <summary>
        /// Displays and validates name method
        /// </summary>
        /// <param name="name">string user name</param>
        private void Name(out string name)
        {
            while (true)
            {
                DisplaySinglePrompt("name");

                name = ReadLine();

                VerifyName verifyName = new VerifyName();

                // Prompt is not used from GeneralError variable in VerifyInput because the verifyName class is used to verify both
                // name and street name, with their prompts being different shown in the video.
                if (verifyName.Verify(name) == false) WriteLine("       The supplied value is not a valid name");

                else break;
            }
        }

        /// <summary>
        /// Displays, validates email, and checks if email exists
        /// </summary>
        private void Email(out string email)
        {
            while (true)
            {
                const string Invalid = "      The supplied value is not a valid email address.";
                const string ExistingEmail = "      The supplied address is already in use.";

                DisplaySinglePrompt("email address");

                email = ReadLine();

                VerifyEmail verifyEmail = new VerifyEmail();

                if (!verifyEmail.Verify(email)) WriteLine(Invalid);

                else if (!(Database.EmailExists(email) == null)) WriteLine(ExistingEmail);

                else if (Database.EmailExists(email) == null) break;
            }
        }

        /// <summary>
        /// Displays and validates password
        /// </summary>
        private void Password(out string password)
        {
            while (true)
            {
                PasswordPrompt();

                password = ReadLine();

                VerifyPassword verifyPassword = new VerifyPassword();

                if (verifyPassword.Verify(password)) break;
            }
        }

        /// <summary>
        /// Simple method which displays password prompt, increases readability
        /// </summary>
        private void PasswordPrompt()
        {
            WriteLine();
            WriteLine("Please choose a password");
            WriteLine("* At least 8 characters");
            WriteLine("* No white space characters");
            WriteLine("* At least one upper-case letter");
            WriteLine("* At least one lower-case letter");
            WriteLine("* At least one digit");
            WriteLine("* At least one special character");
            Write(Prompt);
        }
    }
}
    

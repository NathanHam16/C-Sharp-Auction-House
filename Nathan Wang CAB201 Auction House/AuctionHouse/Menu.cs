using static System.Console;

namespace AuctionHouse
{

    /// <summary>
    /// Inheriting from dialog to customise the menu displayed
    /// </summary>
    public class Menu : Dialog
    {
        public const string Heading = "Please select an option between {0} and {1}";
        public const string MenuPrompt = "Please enter your {0}";
        public const string Prompt = "> ";
        private string MenuFiller;
        private const string Error = "        The supplied value is not a valid input";

        /// <summary>
        /// Local copy of menu options
        /// </summary>
        private InterfaceDisplay[] options;

        /// <summary>
        /// Initialise new menu object with title and options displayed to user
        /// </summary>
        /// <param name="title">Title of menu</param>
        /// <param name="database">Reference to user database</param>
        /// <param name="productDatabase">Reference to product database</param>
        /// <param name="options">InterfaceDisplay Options</param>
        public Menu(string title, UserDatabase database, ProductDatabase productDatabase, params InterfaceDisplay[] options) : base(title, database, productDatabase)
        {
            this.options = new InterfaceDisplay[options.Length];
            Array.Copy(options, this.options, options.Length);
        }

        /// <summary>
        /// Polymorphism and overrides dialog display for unique implementation of menu by using interface display method calling
        /// </summary>
        public override void Display()
        {
            while(true)
            {
                DisplayMenu(Title);
                DisplayOptions();
                InterfaceDisplay opt;
                ProcessOption(out opt, 1, options.Length);

                if (opt != null) opt.Display();

                if (opt is ExitDialog) break;

                if (opt is LogOutDialog) break;
            }
        }

        /// <summary>
        /// Displays menu options
        /// </summary>
        private void DisplayOptions()
        {
            for (int i = 0; i < options.Length; i++)
            {
                WriteLine("({0}) {1}", i + 1, options[i].Title);
            }
            WriteLine();
            WriteLine(Heading, 1, options.Length);
            Write(Prompt);
        }

        /// <summary>
        /// Displays menu title, also customises the length of the menufiller according to menutitle
        /// </summary>
        /// <param name="Menu">name of menu</param>
        public void DisplayMenu(string Menu)
        {
            MenuFiller = new string('-', Menu.Length);
            WriteLine();
            WriteLine(Menu);
            WriteLine(MenuFiller);
        }

        /// <summary>
        /// Processes user input (options) for menu options
        /// </summary>
        /// <param name="opt">Interface option</param>
        /// <param name="low">lower boundary for user input constraint</param>
        /// <param name="high">upper boundary</param>
        public void ProcessOption(out InterfaceDisplay opt, int low, int high)
        {
            int option;
            string userInput = ReadLine();

            if (int.TryParse(userInput, out option) && option >= low && option <= high)
            {
                opt = options[option - 1];
            }

            else
            {
                WriteLine(Error);
                opt = null;
            }
        }

        /// <summary>
        /// Displays single line prompt, used by register and sign in menu
        /// </summary>
        /// <param name="input">the single line prompt displayed</param>
        public void DisplaySinglePrompt(string input)
        {
            WriteLine();
            WriteLine(MenuPrompt, input);
            Write(Prompt);
        }
    }
}
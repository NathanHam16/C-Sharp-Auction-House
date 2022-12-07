using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Derived class of verifyInput, verifies address
    /// </summary>
    internal class VerifyAddress : VerifyInput
    {
        private const string AddressError = "       {0} must be {1}";
        public string Address = "";

        /// <summary>
        /// Boolean method which returns true if all address details are verified
        /// </summary>
        public override bool Verify(string Prompt)
        {
            Unit(Prompt, out string unit);

            StreetNumber(Prompt, out string streetNumber);

            StreetName(Prompt, out string streetName);

            StreetSuffix(Prompt, out string streetSuffix);

            City(Prompt, out string city);

            State(Prompt, out string state);

            PostCode(Prompt, out string postCode);

            WriteLine();

            Address = String.Join(",", unit, streetNumber, streetName, streetSuffix, city, state.ToLower(), postCode);

            return true;
        }

        public void Fetch(out string address)
        {
            address = Address;
        }

        /// <summary>
        /// While loop which verifies unit
        /// </summary>
        /// <param name="Prompt">Prompt varaible from menu class</param>
        private void Unit(string Prompt, out string unit)
        {
            while(true)
            {
                string input = ReadLine();

                if (input == "" | !input.All(char.IsDigit))
                {
                    WriteLine(AddressError, "Unit number", "a non-negative integer");
                    WriteLine();
                    WriteLine("Unit Number (0 = none):");
                    Write(Prompt);
                }
                else if (input == "0")
                {
                    unit = "";
                    break;
                }
                else
                {
                    unit = input + "/";
                    break;
                }
            }
        }

        /// <summary>
        /// Prompts and verifies street number based of user input
        /// </summary>
        private void StreetNumber(string Prompt, out string streetNumber)
        {
            AddressPrompt("Street number:", Prompt);
            
            while(true)
            {
                string input = ReadLine(); 
                if (input == "" | !input.All(char.IsDigit))
                {
                    WriteLine(AddressError, "Street number", "a positive integer");
                    WriteLine();
                    WriteLine("Street number:");
                    Write(Prompt);
                }
                else if (input == "0")
                {
                    WriteLine(AddressError, "Street number", "greater than 0");
                    WriteLine();
                    WriteLine("Street number:");
                    Write(Prompt);
                }
                else
                {
                    WriteLine();
                    streetNumber = input;
                    break;
                }
            }
        }

        /// <summary>
        /// Prompts and verifies street name. Same verification as verifyName so this method creates new instance of that class.
        /// </summary>
        private void StreetName(string Prompt, out string streetName)
        {
            StringVerify("Street name", "Street name:", Prompt, out streetName);

        }

        /// <summary>
        /// Prompts and verifies street suffix. Demonstration video and task sheet shows shortened versions,
        /// while the test cases use full length suffix (Drive). Thus, this is interpreted to simply be a non-blank text string.
        /// </summary>
        private void StreetSuffix(string Prompt, out string streetSuffix)
        {
            StringVerify("Street suffix", "Street suffix:", Prompt, out streetSuffix);
        }

        /// <summary>
        /// Prompts and verifies City
        /// </summary>
        private void City(string Prompt, out string city)
        {
            StringVerify("City", "City:", Prompt, out city);
        }

        /// <summary>
        /// Prompts and verifies postcode. Must be integer between 1000 and 9999 inclusive
        /// </summary>
        private void PostCode(string Prompt, out string postCode)
        {
            AddressPrompt("Postcode (1000 .. 9999):", Prompt);

            while (true)
            {
                string input = ReadLine();
                if (int.TryParse(input, out int number) && number >= 1000 && number <= 9999)
                {
                    postCode = input;
                    break;
                }

                WriteLine(AddressError, "Postcode", "an integer between 1000 and 9999.");
                Write(Prompt);
            }
        }

        /// <summary>
        /// Prompts and verifies state/territory
        /// </summary>
        private void State(string Prompt, out string state)
        {
            string[] states = { "act", "nsw", "nt", "qld", "sa", "tas", "vic", "wa"};
            WriteLine("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA):");
            Write(Prompt);

            while(true)
            {
                string input = ReadLine();
                string lowercase = input.ToLower();

                if (states.Any(lowercase.Contains))
                {
                    state = input;
                    break;
                }

                else
                {
                    WriteLine(AddressError, "State", "one of the above.");
                }
            }
        }

        /// <summary>
        /// Method which displays prompt for address variant, used for other prompts in client menu
        /// </summary>
        public void AddressPrompt(string addressPrompt, string Prompt)
        {
            WriteLine();
            WriteLine(addressPrompt);
            Write(Prompt);
        }

        /// <summary>
        /// General verification for non blank text string, creates instance of verifyName. Is used in other parts of software
        /// </summary>
        public void StringVerify(string ErrorName, string Message, string Prompt, out string address)
        {
            while(true)
            {
                WriteLine(Message);
                Write(Prompt);
                string input = ReadLine();
                VerifyName verifyName = new VerifyName();
                bool textString = verifyName.Verify(input);

                if (textString == false)
                {
                    WriteLine(AddressError, ErrorName, "a non-empty string.");
                    WriteLine();
                }
                else
                {
                    WriteLine();
                    address = input;
                    break;
                }
            }
        }
    }
}

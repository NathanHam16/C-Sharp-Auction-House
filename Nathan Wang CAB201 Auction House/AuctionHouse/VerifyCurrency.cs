using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// A class derived from verifyInput which verifies the currency
    /// </summary>
    internal class VerifyCurrency : VerifyInput
    {
        /// <summary>
        /// Validates currency input, public because used in other classes.
        /// </summary>
        /// <param name="currency">User input</param>
        /// <returns></returns>
        public override bool Verify(string currency)
        { 
                //First non blank symbol
                int offset = currency.TakeWhile(c => char.IsWhiteSpace(c)).Count();

                if (!(currency.Contains('$') && currency.Contains('.'))) return false;

                if (!(currency[offset] == '$')) return false;

                // Character after $ must be digit
                if (!char.IsDigit(currency[offset + 1])) return false;

                int dollar = currency.IndexOf('$');
                string subStringDollar = currency.Substring(dollar);

                int period = currency.IndexOf('.');
                string subStringPeriod = currency.Substring(period);

                //No intervening spaces
                if (subStringDollar.Any(char.IsWhiteSpace)) return false;

                // Period is followed by exactly 2 decimal digits
                int digitCount = subStringPeriod.Count(char.IsDigit);
                if (!(digitCount == 2)) return false;

                //Cannot contain letters
                if (currency.Any(char.IsLetter)) return false;

                else return true;
        }

        /// <summary>
        /// Validates an integer, used in product search and view bids classes
        /// </summary>
        /// <param name="optionsCount">Number of options available</param>
        /// <param name="inputOption">User chooses option</param>
        /// <returns></returns>
        public bool IntValidate(int optionsCount, out int inputOption)
        {
            WriteLine();
            WriteLine("Please enter a non-negative integer between 1 and {0}:", optionsCount);
            Write("> ");

            string input = ReadLine();
            string Error = "       Please enter a non-negative integer within the range specified";

            if (input == "" | !input.All(char.IsDigit))
            {
                WriteLine(Error);
                inputOption = 0;
                return false;
            }

            else
            {
                inputOption = int.Parse(input) - 1;
                if (inputOption < 0 | inputOption > optionsCount)
                {
                    WriteLine(Error);
                    return false;
                }
                else return true;
            }
        }
    }
}

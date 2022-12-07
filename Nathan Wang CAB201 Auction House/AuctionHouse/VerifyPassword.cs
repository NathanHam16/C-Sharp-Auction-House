using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Derived class of verifyInput to override Verify method and use superclass variable.
    /// </summary>
    internal class VerifyPassword : VerifyInput
    {
        /// <summary>
        /// Overrides verify method to check password. 
        /// </summary>
        public override bool Verify(string password)
        {
            bool uppercase = false;
            bool lowercase = false;
            bool digit = false;
            bool specialChar = false;
            bool minimum = false;
            bool whiteSpace = true;
            int minLength = 8;

            if (password.Length >= minLength) minimum = true;

            if (password.Any(char.IsUpper)) uppercase = true;

            if (password.Any(char.IsLower)) lowercase = true;

            if (password.Any(char.IsDigit)) digit = true; //has a special character if not a letter or digit

            if (password.Any(char.IsPunctuation) || password.Any(char.IsSymbol)) specialChar = true;

            if (password.Any(char.IsWhiteSpace)) whiteSpace = false;

            if (uppercase && lowercase && digit && specialChar && minimum && whiteSpace) return true;

            else
            {
                WriteLine(GeneralError, "password");
                return false;
            }
        }
    }
}

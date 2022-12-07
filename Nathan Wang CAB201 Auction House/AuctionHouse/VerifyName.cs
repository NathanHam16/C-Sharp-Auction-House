using static System.Console;

namespace AuctionHouse
{
    /// <summary>
    /// Derived class of verifyInput to override Verify method and use superclass variable.
    /// </summary>
    internal class VerifyName : VerifyInput
    {
        /// <summary>
        /// Overrides verify method to check name. Invalid if name is null, contains digit, or special chars.
        /// </summary>
        public override bool Verify(string name)
        {
            if (name.All(char.IsWhiteSpace) | String.IsNullOrEmpty(name)) return false;

            // Return false if first or last character of string is ''
            else if (name[0] == ' ' | name[name.Length - 1] == ' ') return false;

            return true;
        }
    }
}

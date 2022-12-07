using static System.Console;


namespace AuctionHouse
{
    /// <summary>
    /// Abstract class for verifying any user input.
    /// Derived classes of this have accessibility becuase they have no derived classes themselves.
    /// </summary>
    public abstract class VerifyInput
    {

        public const string GeneralError = "      The supplied value is not a valid {0}.";

        /// <summary>
        /// Abstract verify method which is customised according to individual classes
        /// </summary>
        /// <param name="input">THe input which needs to be verified</param>
        /// <returns></returns>
        public abstract bool Verify(string input);
    }

}

using static System.Console;
namespace AuctionHouse
{
    /// <summary>
    /// Verifies time, derived from verify input
    /// </summary>
    internal class VerifyTime : VerifyInput
    {
        private const string Error = "        Please enter a valid date and time.";
        private const string DeliveryWindow = "        Delivery window {0} must be at least one hour {1}.";
        private DateTime deliveryStart;

        /// <summary>
        /// Overrides verify method, returns true if input is valid
        /// </summary>
        public override bool Verify(string input)
        {
            if (DateTime.TryParse(input, out DateTime time))
            {
                DateTime currentTime = DateTime.Now;

                if (time >= currentTime.AddHours(1))
                {
                    deliveryStart = time;
                    return true;
                }
                else WriteLine(DeliveryWindow, "start", "in the future");

            }
            else WriteLine(Error);
            return false;
        }

        /// <summary>
        /// Verifys delivery window end
        /// </summary>
        public bool VerifyEnd(string input)
        {
            if (DateTime.TryParse(input, out DateTime timeEnd))
            {
                if (timeEnd >= deliveryStart.AddHours(1))
                {
                    return true;
                }
                else WriteLine(DeliveryWindow, "end", "later than the start");
            }
            else WriteLine(Error);
            return false;

        }
    }
}

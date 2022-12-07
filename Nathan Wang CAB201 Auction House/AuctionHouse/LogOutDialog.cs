namespace AuctionHouse
{
    public class LogOutDialog : InterfaceDisplay
    {
        /// <summary>
        /// Title displayed
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Initialise Exit Dialog, title is displayed in main menu
        /// </summary>
        /// <param name="title">Title of Log out dialog</param>
        public LogOutDialog(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Implements InterfaceDisplay method
        /// </summary>
        public void Display()
        {
        }
    }
}

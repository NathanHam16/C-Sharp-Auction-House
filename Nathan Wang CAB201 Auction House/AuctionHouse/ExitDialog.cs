using static System.Console;    

namespace AuctionHouse
{
    /// <summary>
    /// Internal Exit dialog class derived from Interface
    /// </summary>
    internal class ExitDialog : InterfaceDisplay
    {
        /// <summary>
        /// Title displayed
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Initialise Exit Dialog, title is displayed in main menu
        /// </summary>
        /// <param name="title">Title of ExitDialog</param>
        public ExitDialog( string title)
        {
            Title = title;
        }

        /// <summary>
        /// Implement Interface and displays exit message
        /// </summary>
        public void Display()
        {
            WriteLine("+--------------------------------------------------+");
            WriteLine("| Good bye, thank you for using the Auction House! |");
            WriteLine("+--------------------------------------------------+");
        }

    }
}

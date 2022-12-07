
namespace AuctionHouse
{
    /// <summary>
    /// Product class which stores information on each product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Public email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Public name
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// public description
        /// </summary>
        public string ProductDescription { get; }

        /// <summary>
        /// Public price
        /// </summary>
        public string Price { get; }

        /// <summary>
        /// Name of highest bidder
        /// </summary>
        public string BidName { get; set; }

        /// <summary>
        /// Email of highest bidder
        /// </summary>
        public string BidEmail { get; set; }
        
        /// <summary>
        /// Price of highest bid
        /// </summary>
        public string BidPrice { get; set; }

        /// <summary>
        /// Delivery Option can be home delivery or click and collect
        /// </summary>
        public string DeliveryOption { get; set; }

        /// <summary>
        /// Initialise a new product object with name, email, and password upon registration.
        /// </summary>
        /// <param name="email">Owner's email attached to product</param>
        public Product(string email, string productName, string productDescription, string price, string bidName, string bidEmail, string bidPrice, string deliveryOption)
        {
            Email = email;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            BidName = bidName;
            BidEmail = bidEmail;
            BidPrice = bidPrice;
            DeliveryOption = deliveryOption;
        }

        // Returns true if Email equals existing email in product class / database
        public bool EmailMatches(string email)
        {
            if (Email == null) return false;
            return Email.Equals(email);
        }
    }
}

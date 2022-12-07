using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse
{
    /// <summary>
    /// Purchase class for all items purchased by any user, links to product database
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Public Selleremail
        /// </summary>
        public string SellerEmail { get; }

        /// <summary>
        /// public owner email
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
        public string ListPrice { get; }

        /// <summary>
        /// Public price
        /// </summary>
        public string Price { get; }

        /// <summary>
        /// Public deliver option
        /// </summary>
        public string DeliveryOption { get; set; }

        /// <summary>
        /// Initialise new purchase object with its fields as parameters.
        /// </summary>
        /// <param name="sellerEmail">Email of the seller</param>
        /// <param name="email">Email of the owner</param>
        /// <param name="listPrice">Price listed by the seller</param>
        /// <param name="price">Price bought</param>
        public Purchase(string sellerEmail, string email, string productName, string productDescription, string listPrice, string price, string deliveryOption)
        {
            SellerEmail = sellerEmail;
            Email = email;
            ProductName = productName;
            ProductDescription = productDescription;
            ListPrice = listPrice;
            Price = price;
            DeliveryOption = deliveryOption;
        }

        /// <summary>
        /// Returns true if Email equals existing email in product class / database
        /// </summary>
        public bool Matches(string email)
        {
            if (Email == null) return false;
            return Email.Equals(email);
        }
    }
}

using System;

namespace AuctionHouse
{
    /// <summary>
    /// User class storing user details
    /// </summary>
    public class User
    {
        /// <summary>
        /// Public name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Public password 
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Public Email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Public address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Initialise a new user with name, email, and password upon registration. Address is initially set to '-' in createuser method in user database
        /// </summary>
        public User(string name, string email, string password, string address)
        {
            Name = name;
            Email = email;
            Password = password;
            Address = address;
        }

        /// <summary>
        /// Returns true if Email equals existing email in User class / database
        /// </summary>
        public bool Matches(string email)
        {
            if (Email == null) return false;
            return Email.Equals(email); 
        }
    }
}

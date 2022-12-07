using System;
using System.IO;
using static System.Console;

namespace AuctionHouse
{
    /// <summary>
	/// Database class for user info
	/// </summary>
    public class UserDatabase 
    {
		private const string InvalidData = "Invalid Data or Input";

		/// <summary>
		/// Name of database file
		/// </summary>
		private const string FileName = "database.txt";

		/// <summary>
		/// List of user details
		/// </summary>
		List<User> users = new List<User>();

		/// <summary>
		/// Initialise database
		/// </summary>
		public UserDatabase()
		{
			Load();
        }

		/// <summary>
		/// Creates a new user
		/// </summary>
		public User CreateUser(string name, string email, string password)
		{
			User user = new User(name, email, password, "");
			users.Add(user);
			return user;
		}

		/// <summary>
		/// Encapsulates loading database from file into user class
		/// </summary>
		private void Load()
        {
			if (File.Exists(FileName))
			{
				using StreamReader reader = new StreamReader(FileName);
				while (!reader.EndOfStream) 
                {
					string line = reader.ReadLine();

					if (line == null) break;
					LoadUser(reader);
                }
			}
			else Console.Error.WriteLine(InvalidData);
		}

		/// <summary>
		/// Uses text reader to parse user details
		/// </summary>
		/// <param name="reader"></param>
		private void LoadUser(TextReader reader)
        {
			string name = reader.ReadLine();
			string email = reader.ReadLine();
			string password = reader.ReadLine();
			string address = reader.ReadLine();

			users.Add(new User(name, email, password, address));
		}

		/// <summary>
		/// Saves data to a text file, able to be parsed by load
		/// </summary>
		public void Save()
		{
			using (StreamWriter writer = new StreamWriter(FileName))
			{
				foreach (User user in users)
				{ 
					writer.WriteLine("User");
					writer.WriteLine(user.Name);
					writer.WriteLine(user.Email.ToString());
					writer.WriteLine(user.Password.ToString());
					writer.WriteLine(user.Address.ToString());
				}	
				writer.Close();
			}
		}

		/// <summary>
		/// Checks existing email
		/// </summary>
		public User EmailExists(string email)
		{ 
			foreach (User user in users)
            {
				if (user.Matches(email))
				{
					return user;
				}
;           }
			return null;
        }

		/// <summary>
		/// Checks if password entered matches email
		/// </summary>
		public User PasswordMatch(string email, string password)
        {
			User user = EmailExists(email);
			if (user.Password == password)
            {
				return user;
            }
			return null;
        }

		/// <summary>
		/// Updates the user's address
		/// </summary>
		public User UpdateUser(string email, string update, string type)
        {
			User user = EmailExists(email);

			if (type == "address")
            {
				user.Address = update;
				return user;
            }
			return null;

        }
	}
}

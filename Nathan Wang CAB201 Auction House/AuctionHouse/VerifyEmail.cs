namespace AuctionHouse
{
    // Derived class to verify email
    internal class VerifyEmail : VerifyInput
    {
        /// <summary>
        /// Overrides method to verify email. Comments are used for complex logic when necessary. returns true if valid
        /// </summary>
        public override bool Verify(string email)
        {
            if (!email.Contains('@')) return false;

            if ((email[0] == '@') || (email[email.Length - 1]) == '@') return false; 

            if (email.Count(freq => freq == '@') > 1) return false; 

            int index = email.IndexOf('@');
            string prefix = email.Substring(0, index);
            string suffix = email.Substring(index + 1);
            int prefixEnd = prefix[prefix.Length - 1];

            // If prefix is valid
            if (!(prefix.Contains('_') || prefix.Contains('-') || prefix.Contains('.') || prefix.Any(char.IsDigit) || prefix.Any(char.IsLetter))) return false; 

            if ((prefixEnd == '_') || (prefixEnd == '-') || (prefixEnd == '.')) return false;

            if (!suffix.Contains('.')) return false;

            if ((suffix[0] == '.') || (suffix[suffix.Length - 1] == '.')) return false;

            // If suffix is valid
            if (!(suffix.Contains('-') || suffix.Contains('.') || suffix.Any(char.IsDigit) || suffix.Any(char.IsLetter))) return false; 

            // This code block uses charArray to reverse suffix. The dot is found and substring is checked for numbers.
            char[] charArray = suffix.ToCharArray();
            Array.Reverse(charArray); 
            int lastDot = Array.IndexOf(charArray, '.');
            string reversedSuffix = new string(charArray);
            string lastDotString = reversedSuffix.Substring(0, lastDot);

            //if there are any numbers
            if (lastDotString.Any(char.IsNumber)) return false;

            else return true;
        }
    }
}

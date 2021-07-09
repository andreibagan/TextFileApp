using System.Collections.Generic;

namespace DataAccessLibrary.Models
{
    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public List<string> EmailAddresses { get; set; } = new List<string>();
    }
}

using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<ContactModel> ReadAllRecords(string textFile)
        {
            if(!File.Exists(textFile))
            {
                return new List<ContactModel>();
            }

            List<ContactModel> output = new List<ContactModel>();
            var lines = File.ReadAllLines(textFile);

            foreach (var line in lines)
            {
                ContactModel contact = new ContactModel();
                var values = line.Split(',');

                if (values.Length < 4)
                {
                    throw new Exception($"Invalid row of data: {line}");
                }

                contact.FirstName = values[0];
                contact.LastName = values[1];
                contact.EmailAddresses = values[2].Split(';').ToList();
                contact.PhoneNumbers = values[3].Split(';').ToList();

                output.Add(contact);
            }

            return output;
        }

        public void WriteAllRecords(List<ContactModel> contacts, string textFile)
        {
            List<string> lines = new List<string>();

            foreach (var c in contacts)
            {
                lines.Add($"{c.FirstName},{c.LastName},{String.Join(';', c.EmailAddresses)},{String.Join(';', c.PhoneNumbers)}");
            }

            File.WriteAllLines(textFile, lines);
        }
    }
}

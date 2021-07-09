using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextFileUI
{
    class Program
    {
        private static IConfiguration _config;
        private static string _textFile;
        private static TextFileDataAccess _db = new TextFileDataAccess();

        static void Main(string[] args)
        {
            InitializeConfiguration();
            _textFile = _config.GetValue<string>("TextFile:Path");

            ContactModel user1 = new ContactModel();
            user1.FirstName = "Andrei";
            user1.LastName = "Bagan";
            user1.EmailAddresses.Add("andrei.bagan1@mail.ru");
            user1.EmailAddresses.Add("andrei.bagan2@mail.ru");
            user1.EmailAddresses.Add("andrei.bagang@mail.ru");
            user1.PhoneNumbers.Add("+375292334228");
            user1.PhoneNumbers.Add("+375333622646");

            ContactModel user2 = new ContactModel();
            user2.FirstName = "Evgeniy";
            user2.LastName = "Strelkov";
            user2.EmailAddresses.Add("evgeniy1@mail.ru");
            user2.EmailAddresses.Add("evgeniy2@mail.ru");
            user2.EmailAddresses.Add("evgeniyg@mail.ru");
            user2.PhoneNumbers.Add("+375297653545");
            user2.PhoneNumbers.Add("+375337546547");

            List<ContactModel> contacts = new List<ContactModel>
            {
                user1,
                user2
            };

            //CreateContact(user1);
            //CreateContact(user2);
            //GetAllContacts();
            //UpdateContactsFirstName("Andrew", "Andrei");
            //UpdateContactsFirstName("Evgeniy", "Jenya");
            //RemovePhoneNumberFromUser("Strelkov", "+375297653545");
            RemoveUser("Bagan");
            GetAllContacts();

            Console.WriteLine("Text File");
            Console.ReadLine();
        }

        private static void GetAllContacts()
        {
            var contacts = _db.ReadAllRecords(_textFile);

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName}");

                foreach (var email in contact.EmailAddresses)
                {
                    Console.WriteLine(email);
                }

                foreach (var phone in contact.PhoneNumbers)
                {
                    Console.WriteLine(phone);
                }
            }
        }

        private static void CreateContact(ContactModel contact)
        {
            var contacts = _db.ReadAllRecords(_textFile);

            contacts.Add(contact);

            _db.WriteAllRecords(contacts, _textFile);
        }

        private static void UpdateContactsFirstName(string oldFirstName, string newFirstName)
        {
            var contacts = _db.ReadAllRecords(_textFile);
            contacts.Where(c => c.FirstName == oldFirstName).First().FirstName = newFirstName;
            _db.WriteAllRecords(contacts, _textFile);
        }

        private static void RemovePhoneNumberFromUser(string contactLastName, string phoneNumber)
        {
            var contacts = _db.ReadAllRecords(_textFile);
            contacts.Where(c => c.LastName == contactLastName).First().PhoneNumbers.Remove(phoneNumber);
            _db.WriteAllRecords(contacts, _textFile);   
        }

        private static void RemoveUser(string lastName)
        {
            var contacts = _db.ReadAllRecords(_textFile);
            contacts.Remove(contacts.Where(c => c.LastName == lastName).First());
            _db.WriteAllRecords(contacts, _textFile);
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }
    }
}

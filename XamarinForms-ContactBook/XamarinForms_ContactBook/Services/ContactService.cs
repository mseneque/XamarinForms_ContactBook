using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XamarinForms_ContactBook.Models;

namespace XamarinForms_ContactBook.Services
{
    class ContactService
    {
        private ObservableCollection<Contact> _contacts;

        public ContactService()
        {
            _contacts = new ObservableCollection<Contact>()
            {
                new Contact { IsBlocked = false, Id = 1, FirstName = "Matt", LastName = "Seneque"},
                new Contact { IsBlocked = false, Id = 2, FirstName = "Some", LastName = "Guy"}
            };
        }

        // Get
        public IEnumerable<Contact> GetContacts()
        {
            // todo: Implement search
            return _contacts;
        }

        // Create
        public bool CreateContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException();

            _contacts.Add(contact);
            return true;
        }

        // Delete
        public void DeleteContact(int contactId)
        {
            _contacts.Remove(_contacts.Single(c => c.Id == contactId));
        }

        // Update
        public void UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}

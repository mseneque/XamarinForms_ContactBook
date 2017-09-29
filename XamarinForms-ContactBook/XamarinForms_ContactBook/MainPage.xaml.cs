using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinForms_ContactBook.Models;
using XamarinForms_ContactBook.Services;

namespace XamarinForms_ContactBook
{
    public partial class MainPage : ContentPage
    {
        private ContactService _contactService;

        public MainPage()
        {
            _contactService = new ContactService();

            InitializeComponent();

            PopulateContactBook(_contactService.GetContacts());


        }

        // Get
        private void PopulateContactBook(IEnumerable<Contact> contacts)
        {
            contactBook.ItemsSource = contacts;
        }

        // Create
        private async void AddContact_OnClicked(object sender, EventArgs e)
        {
            var page = new ContactDetailPage(new Contact());

            // Subscribing to the ContactAdded event
            page.ContactAdded += (source, contact) =>
            {
                _contactService.CreateContact(contact);
            };

            await Navigation.PushAsync(page);

        }

        // Delete
        private async void DeleteItem_OnClicked(object sender, EventArgs e)
        {
            var contact = (sender as MenuItem).CommandParameter as Contact;

            if (await DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
                _contactService.DeleteContact(contact.Id);
        }

        // Update
        private async void ContactBook_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // to stop item selected loop after setting it to null
            if (contactBook.SelectedItem == null)
                return;

            contactBook.SelectedItem = null;

            var selectedContact = e.SelectedItem as Contact;

            // Subscribing to the ContactUpdated event
            var page = new ContactDetailPage(selectedContact);
            page.ContactUpdated += (source, contact) =>
            {
                //_contactService.UpdateContact(contact);
                if (selectedContact != null)
                {
                    selectedContact.Id = contact.Id;
                    selectedContact.FirstName = contact.FirstName;
                    selectedContact.LastName = contact.LastName;
                    selectedContact.Phone = contact.Phone;
                    selectedContact.Email = contact.Email;
                    selectedContact.IsBlocked = contact.IsBlocked;
                }
            };

            await Navigation.PushAsync(page);

        }
    }
}

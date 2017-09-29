
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms_ContactBook.Models;
using XamarinForms_ContactBook.Services;

namespace XamarinForms_ContactBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailPage : ContentPage
    {
        // EventHandler<Contact> is a delegate that can reference a method with 
        // the following signature:
        //
        // void MethodName(object source, Contact args); 

        public event EventHandler<Contact> ContactAdded;
        public event EventHandler<Contact> ContactUpdated;

        private ContactService _contactService = new ContactService();
        private Contact _contact;

        public ContactDetailPage()
        {
            _contact = new Contact();
            BindingContext = _contact;
            InitializeComponent();

        }

        public ContactDetailPage(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            // Can do a temporary bind to a new Contact, so it the contact doesn't get updated when the back button is presed.
            BindingContext = new Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                Email = contact.Email,
                IsBlocked = contact.IsBlocked
            };

            InitializeComponent();
        }

        private async void Save_OnTapped(object sender, EventArgs e)
        {
            var contact = BindingContext as Contact;

            if (String.IsNullOrWhiteSpace(contact.FullName))
            {
                await DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            // Temporary hack to handle the difference between a create and an update
            if (contact.Id == 0)
            {
                // Create
                contact.Id = 1;
                ContactAdded?.Invoke(this, contact);
            }
            else
            {
                // Update
                ContactUpdated?.Invoke(this, contact);
            }

            await Navigation.PopAsync();
        }
    }
}
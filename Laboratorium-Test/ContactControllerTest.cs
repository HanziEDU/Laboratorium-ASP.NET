﻿using Laboratorium_3___App.Controllers;
using Laboratorium_3___App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Laboratorium_Test
{
    public class ContactControllerTest
    {
        private ContactController _controller;
        private IContactService _service;


        public ContactControllerTest()
        {
            // Initialize the service with the YourDateTimeProviderImplementation
            var dateTimeProvider = new DateTimeProviderImplementation();
            _service = new MemoryContactService(dateTimeProvider);

            // Add test data
            _service.add(new Contact() { Id = 1 });
            _service.add(new Contact() { Id = 2 });

            _controller = new ContactController(_service);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DetailsTestForExistingContacts(int id)
        {
            var result = _controller.Details(id);
            Assert.IsType<ViewResult>(result);
            var view = result as ViewResult;
            var model = view.Model as Contact;
            Assert.Equal(id, model.Id);
        }



        [Fact]
        public void Add_ValidContact_ReturnsNewContactId()
        {
            var contact = new Contact { Id = 3, Name = "John" };
            _service.add(contact);


            var retrievedContact = _service.FindByID(contact.Id);
            Assert.NotNull(retrievedContact);
            Assert.Equal(contact.Id, retrievedContact.Id);
            Assert.Equal(contact.Name, retrievedContact.Name);
        }


        [Fact]
        public void Delete_ExistingContact_RemovesContact()
        {
            IContactService contactService = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            contactService.add(contact);
            contactService.RemoveByID(contact.Id);

            Assert.Null(contactService.FindByID(contact.Id));
        }

        [Fact]
        public void Update_ExistingContact_UpdatesContact()
        {
            IContactService contactService = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            contactService.add(contact);
            var updatedContact = new Contact { Id = 1, Name = "Updated John" };
            contactService.Update(updatedContact);

            var retrievedContact = contactService.FindByID(updatedContact.Id);
            Assert.NotNull(retrievedContact);
            Assert.Equal(updatedContact.Name, retrievedContact.Name);
        }

        [Fact]
        public void FindAll_ReturnsAllContacts()
        {
            IContactService contactService = new MemoryContactService(new DateTimeProviderImplementation());
            var contact1 = new Contact { Id = 1, Name = "John" };
            var contact2 = new Contact { Id = 2, Name = "Alice" };
            contactService.add(contact1);
            contactService.add(contact2);

            var contacts = contactService.FindAll();

            Assert.Equal(2, contacts.Count);
        }

        [Fact]
        public void FindById_ExistingContact_ReturnsContact()
        {
            IContactService contactService = new MemoryContactService(new DateTimeProviderImplementation());
            var contact = new Contact { Id = 1, Name = "John" };
            contactService.add(contact);
            var retrievedContact = contactService.FindByID(contact.Id);

            Assert.NotNull(retrievedContact);
            Assert.Equal(contact.Id, retrievedContact.Id);
        }

        [Fact]
        public void FindById_NonExistingContact_ReturnsNull()
        {
            IContactService contactService = new MemoryContactService(new DateTimeProviderImplementation());
            var retrievedContact = contactService.FindByID(100); // Assuming contact with ID 100 doesn't exist

            Assert.Null(retrievedContact);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.Web.ViewModels;
using FilRouge.Web.Entities;

namespace FilRouge.Web.ViewModels.Maps
{
    public static class ContactMap
    {
        /// <summary>
        /// Convertir un "Contact" en "ContactViewModel"
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static ContactViewModel MapToContactViewModel(this Contact contact)
        {
            var contactViewModel = new ContactViewModel();
            if (contact == null)
            {
                return contactViewModel;
            }

            contactViewModel = new ContactViewModel
            {
                UserId = contact.UserId,
                Name = contact.Name,
                Prenom = contact.Prenom,
                Tel = contact.Tel,
                Email = contact.Email,
                Type = contact.Type
            };
            return contactViewModel;
        }

        /// <summary>
        /// Convertir un "ContactViewModel" en "Contact"
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public static Contact MapToContact(this ContactViewModel contactViewModel)
        {
            var contact = new Contact();
            if (contactViewModel == null)
            {
                return contact;
            }

            contact = new Contact
            {
                UserId = contactViewModel.UserId,
                Name = contactViewModel.Name,
                Prenom = contactViewModel.Prenom,
                Tel = contactViewModel.Tel,
                Email = contactViewModel.Email,
                Type = contactViewModel.Type
            };
            return contact;
        }
    }
}
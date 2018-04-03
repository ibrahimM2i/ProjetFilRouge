using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Services
{
    public class ContactService
    {
        /// <summary>
        /// Récuperer tous les contacts
        /// </summary>
        /// <returns>une liste de contact</returns>
        public List<ContactViewModel> GetAllContact()
        {
            List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
            using (var db = new FilRougeDBContext())
            {
                var contacts = db.Contact.ToList();
                foreach (var contact in contacts)
                {
                    contactViewModels.Add(contact.MapToContactViewModel());
                }
            }
            return contactViewModels;
        }

        /// <summary>
        /// Récuperer un contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactViewModel GetContactById(int id)
        {
            var contactViewModel = new ContactViewModel();
            using (var db = new FilRougeDBContext())
            {
                var contact = db.Contact.Find(id);
                contactViewModel = contact.MapToContactViewModel();
            }
            return contactViewModel;
        }

        /// <summary>
        /// Ajouter un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public int AddContact(ContactViewModel contactViewModel)
        {
            var id = 0;
            using (var db = new FilRougeDBContext())
            {
                var contact = contactViewModel.MapToContact();
                db.Contact.Add(contact);
                db.SaveChanges();
                id = contact.UserId;
            }
            return id;
        }

        /// <summary>
        /// Modifier un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public int EditContact(ContactViewModel contactViewModel)
        {
            var id = 0;
            using (var db = new FilRougeDBContext())
            {
                var contact = db.Contact.Find(contactViewModel.UserId);
                contact.Name = contactViewModel.Name;
                contact.Prenom = contactViewModel.Prenom;
                contact.Tel = contactViewModel.Tel;
                contact.Email = contactViewModel.Email;
                contact.Type = contactViewModel.Type;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                id = contact.UserId;
            }
            return id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
                var contacts = db.Users.ToList();
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
        public ContactViewModel GetContactById(string id)
        {
            var contactViewModel = new ContactViewModel();
            using (var db = new FilRougeDBContext())
            {
                var contact = db.Users.Find(id);
                contactViewModel = contact.MapToContactViewModel();
            }
            return contactViewModel;
        }


        /// <summary>
        /// Ajouter un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public string AddContact(ContactViewModel contactViewModel, ApplicationUserManager manager)
        {
            var id = string.Empty;
            using (var db = new FilRougeDBContext())
            {
                /*var contact = contactViewModel.MapToContact();
                db.Users.Add(contact);
                db.SaveChanges();
                id = contact.Id;*/

                var user = new Contact { UserName = contactViewModel.UserName, Email = contactViewModel.UserName, Name = contactViewModel.Name, PhoneNumber = contactViewModel.PhoneNumber };
                var result = manager.CreateAsync(user, contactViewModel.Password).Result;
                if (result.Succeeded)
                {
                    id = user.Id;
                    var role = manager.AddToRole(user.Id, contactViewModel.RoleName).Succeeded;
                }

            }
            return id;
        }

        /// <summary>
        /// Modifier un contact
        /// </summary>
        /// <param name="contactViewModel"></param>
        /// <returns></returns>
        public string EditContact(ContactViewModel contactViewModel, ApplicationUserManager manager)
        {
            var id = string.Empty;
            using (var db = new FilRougeDBContext())
            {
                //var contact = db.Users.Find(contactViewModel.Id);

                var contact = manager.Users.FirstOrDefault(u => u.Id == contactViewModel.Id);

                contact.UserName = contactViewModel.UserName;
                contact.PhoneNumber = contactViewModel.PhoneNumber;
                contact.Email = contactViewModel.UserName;
                contact.Name = contactViewModel.Name;
                var resultatRetirerPass = manager.RemovePassword(contact.Id);
                //var resultPass = manager.ChangePassword(contact.Id contact.PasswordHash);
                var resultPass = manager.ChangePasswordAsync(contact.Id, null, contactViewModel.Password).Result;
                //contact.Roles = contactViewModel.SesRoles;
                //db.Entry(contact).State = EntityState.Modified;

                IdentityResult result = manager.UpdateAsync(contact).Result;
                //db.SaveChanges();
                id = contact.Id;
            }
            return id;
        }

        public List<SelectListItem> GetListItemsRoles()
        {

            var listeRoles = new List<SelectListItem>();


            using (var db = new FilRougeDBContext())
            {
                var roles = db.Roles.ToList();

                foreach (var role in roles)
                {
                    listeRoles.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Name

                    });
                }

                return listeRoles;
            }
        }
    }
}
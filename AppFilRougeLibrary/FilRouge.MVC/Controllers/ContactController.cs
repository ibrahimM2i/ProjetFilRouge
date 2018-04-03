using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;

namespace FilRouge.MVC.Controllers
{
    public class ContactController : Controller
    {
        private ContactService _contactService = new ContactService();
        // GET: Contact
        public ActionResult Index()
        {
            var contacts = _contactService.GetAllContact();
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddContact(contactViewModel);
                return RedirectToAction("Index", "Contact");
            }
            return View(contactViewModel);
        }

        public ActionResult Edit(int id)
        {
            ContactViewModel contactViewModel = _contactService.GetContactById(id);
            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _contactService.EditContact(contactViewModel);
                return RedirectToAction("Index", "Contact");
            }
            return View(contactViewModel);
        }

        public ActionResult Details(int id)
        {
            ContactViewModel contactViewModel = _contactService.GetContactById(id);
            return View(contactViewModel);
        }

    }
}
using System.Web.Mvc;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;

namespace FilRouge.MVC.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private ContactService _contactService = new ContactService();
        // GET: Contact
        public ActionResult Index()
        {
            var contacts = _contactService.GetAllContact();
            return View(contacts);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
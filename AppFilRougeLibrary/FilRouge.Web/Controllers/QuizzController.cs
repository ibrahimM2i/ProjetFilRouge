using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilRouge.Web.Controllers
{
    public class QuizzController : Controller
    {
        // GET: Quizz
        public ActionResult Index()
        {
            return View();
        }
    }
}
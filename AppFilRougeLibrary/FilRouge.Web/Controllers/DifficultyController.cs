using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilRouge.Web.Services;
using FilRouge.Web.ViewModels;

namespace FilRouge.Web.Controllers
{
    public class DifficultyController : Controller
    {

        private readonly DifficultyServices _difficultyServices = new DifficultyServices();

        // GET: Difficulty
        public ActionResult Index()
        {
            var difficulties = _difficultyServices.GetAllDifficulties();
            return View(difficulties);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new DifficultyMasterViewModel());
        }

        [HttpPost]
        public ActionResult Add(DifficultyMasterViewModel difficultyViewModel)
        {
            if (ModelState.IsValid)
            {
                _difficultyServices.AddDifficulty(difficultyViewModel);
                return RedirectToAction("Index");
            }
            else
                return View(difficultyViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var difficulty = _difficultyServices.GetDifficultyById(id);
            return View(difficulty);
        }

        [HttpPost]
        public ActionResult Edit(DifficultyMasterViewModel difficultyViewModel)
        {
            if (ModelState.IsValid)
            {
                _difficultyServices.EditDifficulty(difficultyViewModel);
                return RedirectToAction("Index");
            }
            else
                return View(difficultyViewModel);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var difficulty = _difficultyServices.GetDifficultyById(id);
            return View(difficulty);
        }
    }
}
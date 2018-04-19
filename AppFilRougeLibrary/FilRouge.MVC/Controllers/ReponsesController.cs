using System.Collections.Generic;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;
using FilRouge.Web.ViewModels;

namespace FilRouge.Web.Controllers
{
    public class ReponsesController : Controller
    {
        private QuestionService _questionService = new QuestionService();
        private ReponsesServices _reponseService = new ReponsesServices();

        // GET: Reponse/Create/id
        [HttpGet]
        public ActionResult Create(int id)
        {
            var question = _questionService.GetQuestion(id);
            ViewBag.Question = question;
            return View(new ReponseViewModel());
        }

        // POST: Reponses/Create/id
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            var question = _questionService.GetQuestion(id).MapToQuestion();
            ViewBag.Question = question;

            List<Reponses> reponses = new List<Reponses>();

            //récupération des 4 choix et l'ajouter dans liste "reponses"
            for (int i = 1; i <= 4; i++)
            {
                Reponses reponse = new Reponses
                {
                    QuestionId = question.QuestionId,
                    Content = collection.GetValue("reponse" + i).AttemptedValue,
                    TrueReponse = collection.GetValue("BonneReponse" + i).AttemptedValue == "false" ? false : true //prend false si pas cocher
                };
                reponses.Add(reponse);
            }

			/* /!\ impossible d'utiliser la méthode Edit du serviceQuestion, car le "ViewModel" et le "Mapping" ne prend
            ** pas en compte la liste des réponses */
			//using (var db = new FilRougeDBContext())
			//{
			//    var questionAddReponse = db.Questions.Find(id);

			//    questionAddReponse.Reponses = reponses; // ajout de la liste des réponses à la question
			//    int nbRes = db.SaveChanges();
			//    if (nbRes > 0)
			//    {
			//        return RedirectToAction("Questions", "Questions");
			//    }
			//}

			var nbRes = _questionService.AddReponsesToQuestion(id, reponses);
			if (nbRes > 0)
			{
				return RedirectToAction("Questions", "Questions");
			}

			return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var reponse = _reponseService.GetReponsesById(id);
            var question = _questionService.GetQuestion(id);
            ViewBag.Question = question;
            return View(reponse);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return RedirectToAction("Questions", "Questions");
        }

    }
}
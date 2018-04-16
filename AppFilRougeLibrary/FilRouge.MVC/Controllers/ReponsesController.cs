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

        // GET: Reponse/Create/idQuestion
        public ActionResult Create(int id)
        {
            var question = _questionService.GetQuestion(id);
            ViewBag.Question = question;
            return View(new ReponseViewModel());
        }

        // POST: Reponse/Create/id
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            var question = _questionService.GetQuestion(id).MapToQuestion();
            ViewBag.Question = question;

            List<Reponses> reponses = new List<Reponses>();

            //récupération des 4 choix et l'ajouter dans liste "reponses"
            for (int i = 1; i <= 4; i++)
            {
                Reponses reponse = new Reponses();
                reponse.QuestionId = question.QuestionId;
                reponse.Content = collection.GetValue("reponse" + i).AttemptedValue;
                reponse.TrueReponse = collection.GetValue("BonneReponse" + i).AttemptedValue == "false" ? false : true;//prend false si pas cocher
                reponses.Add(reponse);
            }

            /* /!\ impossible d'utiliser la méthode Edit du serviceQuestion, car le "ViewModel" et le "Mapping" ne prend
            ** pas en compte la liste des réponses */
            using (var db = new FilRougeDBContext())
            {
                var questionAddReponse = db.Questions.Find(id);

                questionAddReponse.Reponses = reponses; // ajout de la liste des réponses à la question
                int nbRes = db.SaveChanges();
                if (nbRes > 0)
                {
                    return RedirectToAction("Questions", "Questions");
                }
            }

            return View();
        }

    }
}
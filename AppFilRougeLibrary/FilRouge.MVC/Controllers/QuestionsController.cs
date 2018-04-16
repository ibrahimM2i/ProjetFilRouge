using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Entities;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using FilRouge.MVC.ViewModels.Maps;

namespace FilRouge.MVC.Controllers
{
	public class QuestionsController : Controller
	{
		private readonly QuizzService _quizzService = new QuizzService();
		private readonly ReferencesService _referencesService = new ReferencesService();
		private readonly QuestionService _questionService = new QuestionService();
		//private readonly StudentAttendanceService -studentAttendanceService = new StudentAttendanceService();
		/// <summary>
		/// Get:  Ajouter une question
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult AddQuestion()
		{
            var technologiesListItem = _referencesService.GetListItemTechnologies();
            var questionTypeListItem = _referencesService.GetListItemQuestionType();
            var difficultiesListItem = _referencesService.GetListItemsDifficulties();

            ViewBag.Difficulties = difficultiesListItem;
            ViewBag.Technologies = technologiesListItem;
            ViewBag.QuestionType = questionTypeListItem;
            return View(new QuestionViewModel());
		}



		/// <summary>
		/// Post : Ajouter une question
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult AddQuestion(QuestionViewModel questionViewModel)
		{
			if (ModelState.IsValid)
			{
				_questionService.AddQuestion(questionViewModel);
				return RedirectToAction("Questions");
			}
			var technologiesListItem = _referencesService.GetListItemTechnologies();
			var questionTypeListItem = _referencesService.GetListItemQuestionType();
            var difficultiesListItem = _referencesService.GetListItemsDifficulties();

            ViewBag.Difficulties = difficultiesListItem;
            ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = questionTypeListItem;

			return View(questionViewModel);
		}

		public ActionResult Questions()
		{
			var questions = _questionService.GetAllQuestions();

			return View("Questions", questions);
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			QuestionViewModel questionViewModel = _questionService.GetQuestion(id);
			if (questionViewModel == null)
			{
				return HttpNotFound();
			}

            var technologiesListItem = _referencesService.GetListItemTechnologies();
            var questionTypeListItem = _referencesService.GetListItemQuestionType();
            var difficultiesListItem = _referencesService.GetListItemsDifficulties();

            ViewBag.Difficulties = difficultiesListItem;
            ViewBag.Technologies = technologiesListItem;
            ViewBag.QuestionType = questionTypeListItem;

            return View(questionViewModel);
		}
		/// <summary>
		/// Edition d'une question "GET"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			QuestionViewModel questionViewModel = _questionService.GetQuestion(id);
			if (questionViewModel == null)
			{
				return HttpNotFound();
			}

            var technologiesListItem = _referencesService.GetListItemTechnologies();
            var questionTypeListItem = _referencesService.GetListItemQuestionType();
            var difficultiesListItem = _referencesService.GetListItemsDifficulties();

            ViewBag.Difficulties = difficultiesListItem;
            ViewBag.Technologies = technologiesListItem;
            ViewBag.QuestionType = questionTypeListItem;

            return View(questionViewModel);
		}

		// POST: Questions/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit( QuestionViewModel questionViewModel)
		{
			if (ModelState.IsValid)
			{
				_questionService.EditQuestion(questionViewModel);
				return RedirectToAction("Index");
			}

            var technologiesListItem = _referencesService.GetListItemTechnologies();
            var questionTypeListItem = _referencesService.GetListItemQuestionType();
            var difficultiesListItem = _referencesService.GetListItemsDifficulties();

            ViewBag.Difficulties = difficultiesListItem;
            ViewBag.Technologies = technologiesListItem;
            ViewBag.QuestionType = questionTypeListItem;

            return View(questionViewModel);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilRouge.Web.Entities;
using FilRouge.Web.Services;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Controllers
{
	public class QuestionsController : Controller
	{
		private readonly QuizzService _quizzService = new QuizzService();
		private readonly ReferencesService _referencesService = new ReferencesService();
		//private readonly StudentAttendanceService -studentAttendanceService = new StudentAttendanceService();
		/// <summary>
		/// Get:  Ajouter une question
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult AddQuestion()
		{
			var technologiesListItem = _referencesService.GetListItemsTechnologies();
			var difficultiesListItem = _referencesService.GetListItemQuestionType();

			ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = difficultiesListItem;
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
				_quizzService.AddQuestion(questionViewModel);
				return RedirectToAction("Index", "Home");
			}
			var technologiesListItem = _referencesService.GetListItemsTechnologies();
			var difficultiesListItem = _referencesService.GetListItemQuestionType();

			ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = difficultiesListItem;

			return View(questionViewModel);
		}

		public ActionResult Questions()
		{
			var questions = _quizzService.GetAllQuestions();

			return View("Questions", questions);
		}

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			QuestionViewModel questionViewModel = _quizzService.GetQuestion(id);
			if (questionViewModel == null)
			{
				return HttpNotFound();
			}
			var technologiesListItem = _referencesService.GetListItemsTechnologies();
			var difficultiesListItem = _referencesService.GetListItemQuestionType();

			ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = difficultiesListItem;

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
			QuestionViewModel questionViewModel = _quizzService.GetQuestion(id);
			if (questionViewModel == null)
			{
				return HttpNotFound();
			}
			var technologiesListItem = _referencesService.GetListItemsTechnologies();
			var difficultiesListItem = _referencesService.GetListItemQuestionType();

			ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = difficultiesListItem;

			return View(questionViewModel);
		}

		// POST: Questions/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit( QuestionViewModel questionViewModel)
		{
			if (ModelState.IsValid)
			{
				_quizzService.EditQuestion(questionViewModel);
				return RedirectToAction("Index");
			}
			var technologiesListItem = _referencesService.GetListItemsTechnologies();
			var difficultiesListItem = _referencesService.GetListItemQuestionType();

			ViewBag.Technologies = technologiesListItem;
			ViewBag.QuestionType = difficultiesListItem;

			return View(questionViewModel);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FilRouge.Web.Entities;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Services
{
	public class QuestionService
	{
		private ReferencesService _referencesService = new ReferencesService();
		private readonly DifficultyServices _difficultyServices = new DifficultyServices();
		private readonly TechnologiesService _technologiesService = new TechnologiesService();
		private readonly TypeQuestionsService _typeQuestionsService = new TypeQuestionsService();

		#region CRUD Question 
		/// <summary>
		/// Ajouter une question dans la BDD
		/// </summary>
		/// <param name="questionsViewModel"></param>
		/// <returns></returns>
		public int AddQuestion(QuestionViewModel questionsViewModel)
		{
			var id = 0;
			using (var dbContext = new FilRougeDBContext())
			{
				var question = questionsViewModel.MapToQuestion();

				dbContext.Questions.Add(question);
				dbContext.SaveChanges();
				id = question.QuestionId;
			}

			return id;
		}

		/// <summary>
		/// Retourne une question par son "ID"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public QuestionViewModel GetQuestion(int? id)
		{
			var question = new Questions();

			using (var dbContext = new FilRougeDBContext())
			{
				question = dbContext.Questions.Find(id);
			}
			return question.MapToQuestionsViewModel();
		}

		/// <summary>
		/// Edition d'une question 
		/// </summary>
		/// <param name="questionViewModel"></param>
		/// <returns></returns>
		public int EditQuestion(QuestionViewModel questionViewModel)
		{
			var id = 0;
			using (var dbContext = new FilRougeDBContext())
			{
				var question = dbContext.Questions.Find(questionViewModel.QuestionId);
				question.Commentaire = questionViewModel.Commentaire;
				question.Content = questionViewModel.Content;
				question.Active = questionViewModel.Active;
				question.DifficultyId = questionViewModel.DifficultyId;
				question.QuestionTypeId = questionViewModel.QuestionTypeId;
				question.TechnologyId = questionViewModel.TechnologyId;

				dbContext.Entry(question).State = EntityState.Modified;
				dbContext.SaveChanges();

				id = question.QuestionId;
			}

			return id;
		}
		/// <summary>
		/// Retourne la list des questions
		/// </summary>
		/// <returns></returns>
		public List<QuestionViewModel> GetAllQuestions()
		{
			var questionViewModels = new List<QuestionViewModel>();
			using (var dbContext = new FilRougeDBContext())
			{
				var questionEntities = dbContext.Questions.ToList();
				foreach (var question in questionEntities)
				{
					questionViewModels.Add(question.MapToQuestionsViewModel());
				}
			}

			return questionViewModels;
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FilRouge.Web.Entities;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Services
{
	/// <summary>
	/// Services liés au quizz, pdf, gestion, mails, CRUD...
	/// </summary>
	public class QuizzService
	{
		// private QuizzService _quizzService = new QuizzService();
		private ReferencesService _referencesService = new ReferencesService();
		#region Properties

		#endregion
		/// <summary>
		/// Constructeur de la classe de QuizzService
		/// </summary>
		public QuizzService() { } //Constructeur
		#region Methods

		/// <summary>
		/// Méthode permettant d'obtenir un quizz en fonction de son id
		/// Fonctionne avec une fluentQuerry
		/// </summary>
		/// <param name="id">l'ID du quizz (sa clé primaire)</param>
		/// <returns>Retourne un objet Quizz</returns>
		public Quizz GetQuizz(int id)
		{
			Quizz fluentQuery = new Quizz();
			FilRougeDBContext db = new FilRougeDBContext();
			try
			{
				fluentQuery = db.Quizz.Single(e => e.QuizzId == id);
				if (fluentQuery == null)
				{
					throw new WrongIdQuizz("L'id saisie n'existe pas");
				}
				db.Dispose();
			}
			catch (FormatException)
			{
				db.Dispose();
				Console.WriteLine("Veuillez saisir un id valide");
			}
			return fluentQuery;
		}
		/// <summary>
		/// Fonction retournant tous les quizz dans une liste de Quizz
		/// Fonctionne avec une fluentQuerry
		/// </summary>
		/// <returns>Retourne tous les quizz</returns>
		public List<Quizz> GetAllQuizz()
		{
			List<Quizz> desQuizz = new List<Quizz>();
			FilRougeDBContext db = new FilRougeDBContext();
			try
			{
				IQueryable<Quizz> fluentQuery = db.Quizz.Select(e => e);
				if (fluentQuery.Count() == 0)
				{
					throw new ListQuizzEmpty("La liste des quizz est vide");
				}
				foreach (var item in fluentQuery)
				{
					desQuizz.Add(item);
				}
				db.Dispose();
			}
			catch (ListQuizzEmpty)
			{
				db.Dispose();
			}

			return desQuizz;
		}

		/// <summary>
		/// Ajouter une question dans le Quizz.
		/// </summary>
		/// <param name="questionsQuizz"></param>
		/// <param name="lesQuestions"></param>
		/// <param name="questionlibre"></param>
		/// <param name="nombrequestions"></param>
		/// <returns></returns>        
		public static List<Questions> AddQuestionToQuizz(bool questionlibre, int nombrequestions, int technoid, int difficultymasterid)
		{
			Random rand = new Random();
			List<Questions> sortedQuestionsQuizz = new List<Questions>();

			FilRougeDBContext db = new FilRougeDBContext();
			try
			{
				int nbrTotalQuestions = db.Questions.Select(e => e).Count();
				IQueryable<Questions> AllQuestionsByTechno = db.Questions.Where(e => e.TechnologyId == technoid && e.Active);
				IQueryable<DifficultyRate> RatesQuizz = db.DifficultyRates.Where(e => e.DifficultyMasterId == difficultymasterid);

				foreach (var rate in RatesQuizz)
				{//Pour gérer la répartition des questions dans le quizz
					for (int i = 0; i < Math.Floor(nombrequestions * rate.Rate); i++)
					{
						foreach (var question in AllQuestionsByTechno)
						{//Vérification par id de la présence d'une question
							if (!(sortedQuestionsQuizz.Contains(question)))
							{
								sortedQuestionsQuizz.Add(question);
							}
						}
					}
				}
			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);
				db.Dispose();
			}


			return sortedQuestionsQuizz;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="difficultyid"></param>
		/// <param name="technoid"></param>
		/// <param name="userid"></param>
		/// <param name="nomuser"></param>
		/// <param name="prenomuser"></param>
		/// <param name="questionlibre"></param>
		/// <param name="nombrequestions"></param>
		public static void CreateQuizz(int userid, int difficultymasterid, int technoid, string nomuser, string prenomuser, bool questionlibre, int nombrequestions)
		{
			List<Questions> questionsQuizz = AddQuestionToQuizz(questionlibre, nombrequestions, technoid, difficultymasterid);
			int timer = 0;
			FilRougeDBContext db = new FilRougeDBContext();
			try
			{
				Contact creatingQuizzContact = db.Contact.Single(e => e.UserId == userid);
				DifficultyMaster difficultyQuizz = db.DifficultyMasters.Single(e => e.DiffMasterId == difficultymasterid);
				Technology technoQuizz = db.Technologies.Single(e => e.TechnoId == technoid);

				Quizz unQuizz = new Quizz
				{
					ContactId = userid,
					DifficultyMasterId = difficultymasterid,
					TechnologyId = technoid,
					Timer = timer,
					PrenomUser = prenomuser,
					NomUser = nomuser,
					NombreQuestion = nombrequestions,
					EtatQuizz = 0,
					QuestionLibre = questionlibre,
					Contact = creatingQuizzContact,
					DifficultyMaster = difficultyQuizz,
					Questions = questionsQuizz,
					Technology = technoQuizz
				};
				db.SaveChanges();
				db.Dispose();
			}
			catch (AlreadyInTheQuestionsList e)
			{
				Console.WriteLine(e.Message);
				db.Dispose();
			}
			catch (NoQuestionsForYou e)
			{
				Console.WriteLine(e.Message);
				db.Dispose();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				db.Dispose();
			}
		}
		#endregion

		public Technology GetTechnology(int? id)
		{
			var technolgie = new Technology();
			using (var dbContext = new FilRougeDBContext())
			{

				technolgie = dbContext.Technologies.Find(id);
			}

			return technolgie;
		}
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

				var technologiesListItem = _referencesService.GetListItemsTechnologies();
				var difficultiesListItem = _referencesService.GetTechnologies();


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

				// ********* version fluente *****//////
				//var AttendanceEntities = dbContext.Attendance.ToList();
				//studentAttendanceViewModels.Add
				//studentAttendanceViewModels.AddRange(AttendanceEntities.Select(Attendance.AttendanceEntities()));
			}

			return questionViewModels;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilRouge.MVC.ViewModels
{
	public class QuestionViewModel
	{
		public int QuestionId { get; set; }
		[Required]
		[Display(Name = "Libellé de la question : ")]
		public string Content { get; set; }
		[Display(Name = "Remarque : ")]
		public string Commentaire { get; set; }
		[Display(Name = "Active ? ")]
		public bool Active { get; set; }
		public int QuestionTypeId { get; set; }
		public int TechnologyId { get; set; }
		public int DifficultyId { get; set; }
	}
}
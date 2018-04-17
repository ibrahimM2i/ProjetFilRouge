using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FilRouge.MVC.Entities;

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

		public TypeQuestion QuestionType { get; set; }
		public Technology Technology { get; set; }
		public Difficulty Difficulty { get; set; }
	}
}
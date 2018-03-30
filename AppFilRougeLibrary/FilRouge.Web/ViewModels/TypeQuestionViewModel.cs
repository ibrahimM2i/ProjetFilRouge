using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilRouge.Web.ViewModels
{
	public class TypeQuestionViewModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Nom étudiant : ")]
		public string Name { get; set; }
		public int TypeQuestionId { get; set; }
		public string NameType { get; set; }
	}
}
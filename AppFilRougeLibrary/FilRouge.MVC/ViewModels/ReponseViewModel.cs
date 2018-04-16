using System.ComponentModel.DataAnnotations;

namespace FilRouge.Web.ViewModels
{
    public class ReponseViewModel
    {
        public int ReponseId { get; set; }

        [Display(Name = "Valeur réponse")]
        public string Content { get; set; }

        [Display(Name = "Bonne réponse?")]
        public bool TrueReponse { get; set; }

        //public int QuestionId { get; set; }
    }
}
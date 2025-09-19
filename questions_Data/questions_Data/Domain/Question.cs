using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_Data.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Enonce { get; set; } = null!;
        public string? Reponse1 { get; set; }
        public string? Reponse2 { get; set; }
        public string? Reponse3 { get; set; }
        public string? Reponse4 { get; set; }
        public string? BonneReponse { get; set; }
        public Categorie? Categorie { get; set; }
        public int? CategorieId { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; } = null!;
    }
}

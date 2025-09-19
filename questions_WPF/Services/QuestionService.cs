using questions_Data.Domain;
//using questions_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_WPF.Services
{
    internal class QuestionService
    {
        public string Titre { get; set; } = string.Empty;

        public QuestionService() { }

        public static ObservableCollection<Question> Questions = new()
        {
            new Question()
            {
                Id = 1,
                Enonce = "Quelle est la capitale de la France ?",
                Reponse1 = "Rouen",
                Reponse2 = "Marseille",
                Reponse3 = "Paris",
                Reponse4 = "Chartres",
                BonneReponse = "Paris",
                CategorieId = 1
            },
            new Question()
            {
                Id = 2,
                Enonce = "Quel est le numéro atomique de l'hydrogène ?",
                Reponse1 = "2",
                Reponse2 = "1",
                Reponse3 = "9",
                Reponse4 = "8",
                BonneReponse = "1",
                CategorieId = 2
            },
            new Question()
            {
                Id = 3,
                Enonce = "Combiens font 2 + 2 ?",
                Reponse1 = "3",
                Reponse2 = "1000",
                Reponse3 = "12",
                Reponse4 = "4",
                BonneReponse = "4",
                CategorieId = 3
            }
        };
        public static void AddQuestion(Question question) => Questions.Add(question);

        public static void RemoveQuestion(Question question) => Questions.Remove(question);
    }   
}

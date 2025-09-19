using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace questions_WPF.Models
{
    public sealed class Question
    {
        public int Id { get; set; }
        public string Enonce { get; set; } = string.Empty;
        public string Reponse1 { get; set; } = string.Empty;
        public string Reponse2 { get; set; } = string.Empty;
        public string Reponse3 { get; set; } = string.Empty;
        public string Reponse4 { get; set; } = string.Empty;
        public string BonneReponse { get; set; } = string.Empty;
        public Categorie Categorie { get; set; }

        public Question() { }

        public Question(int id, string enonce, string reponse1, string reponse2, string reponse3, string reponse4, string bonneReponse, Categorie categorie)
        {
            this.Id = id;
            this.Enonce = enonce;
            this.Reponse1 = reponse1;
            this.Reponse2 = reponse2;
            this.Reponse3 = reponse3;
            this.Reponse4 = reponse4;
            this.BonneReponse = bonneReponse;
            this.Categorie = categorie;
        }
    }
}

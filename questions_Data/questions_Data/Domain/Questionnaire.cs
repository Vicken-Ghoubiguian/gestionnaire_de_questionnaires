using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_Data.Domain
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}

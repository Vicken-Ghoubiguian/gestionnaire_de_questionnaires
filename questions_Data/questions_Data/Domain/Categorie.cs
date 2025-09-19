using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_Data.Domain
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public string? Color { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}

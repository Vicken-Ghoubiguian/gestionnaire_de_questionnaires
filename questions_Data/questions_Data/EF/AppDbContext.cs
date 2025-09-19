using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using questions_Data.Domain;

namespace questions_Data.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Questionnaire> Questionnaires => Set<Questionnaire>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Categorie> Categories => Set<Categorie>();
        protected override void OnModelCreating(ModelBuilder b)
        {
            // Categorie
            b.Entity<Categorie>(e =>
            {
                e.ToTable("Categories");
                e.Property(x => x.Libelle).IsRequired().HasMaxLength(100);
                e.Property(x => x.Color).HasMaxLength(30);
            });

            // Questionnaire
            b.Entity<Questionnaire>(e =>
            {
                e.ToTable("Questionnaires");
                e.Property(x => x.Titre).IsRequired().HasMaxLength(200);
                e.HasMany(x => x.Questions)
                .WithOne(q => q.Questionnaire)
                .HasForeignKey(q => q.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // Question
            b.Entity<Question>(e =>
            {
                e.ToTable("Questions");
                e.Property(x => x.Enonce).IsRequired().HasMaxLength(500);
                e.Property(x => x.Reponse1).HasMaxLength(200);
                e.Property(x => x.Reponse2).HasMaxLength(200);
                e.Property(x => x.Reponse3).HasMaxLength(200);
                e.Property(x => x.Reponse4).HasMaxLength(200);
                e.Property(x => x.BonneReponse).HasMaxLength(200);
                e.HasOne(x => x.Categorie)
                .WithMany(c => c.Questions)
                .HasForeignKey(x => x.CategorieId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            // Seeding pour les tests
            b.Entity<Categorie>().HasData(
                new Categorie { Id = 1, Libelle = "Mathématiques", Color = "#FF5733" },
                new Categorie { Id = 2, Libelle = "Histoire", Color = "#33FF57" },
                new Categorie { Id = 3, Libelle = "Science", Color = "#3357FF" }
            );
            b.Entity<Questionnaire>().HasData(
                new Questionnaire { Id = 1, Titre = "Quiz Général" },
                new Questionnaire { Id = 2, Titre = "Quiz Avancé" }
            );
            b.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    Enonce = "Quelle est la capitale de la France ?",
                    Reponse1 = "Paris",
                    Reponse2 = "Londres",
                    Reponse3 = "Berlin",
                    Reponse4 = "Madrid",
                    BonneReponse = "Paris",
                    CategorieId = 2,
                    QuestionnaireId = 1
                },
                new Question
                {
                    Id = 2,
                    Enonce = "Combien font 2 + 2 ?",
                    Reponse1 = "3",
                    Reponse2 = "4",
                    Reponse3 = "5",
                    Reponse4 = "6",
                    BonneReponse = "4",
                    CategorieId = 1,
                    QuestionnaireId = 1
                },
                new Question
                {
                    Id = 3,
                    Enonce = "Quel est l'élément chimique avec le symbole 'O' ?",
                    Reponse1 = "Or",
                    Reponse2 = "Oxygène",
                    Reponse3 = "Argent",
                    Reponse4 = "Hydrogène",
                    BonneReponse = "Oxygène",
                    CategorieId = 3,
                    QuestionnaireId = 2
                }
            );
            base.OnModelCreating(b);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using questions_Data.Domain;
using questions_Data.EF;
using questions_Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_WPF
{
    public sealed class EfBridge : IDisposable
    {
        private readonly AppDbContext _db;
        private readonly QuestionnaireService _svc;

        public EfBridge()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuestionsDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            _db = new AppDbContext(options);
            _svc = new QuestionnaireService(_db);
        }

        public Task<List<Questionnaire>> GetAllQuestionnairesAsync() => _svc.GetAllAsync();
        public Task<Questionnaire?> GetQuestionnaireWithQuestionsAsync(int id) => _svc.GetByIdAsync(id);
        public Task<int> CreateQuestionnaireAsync(string? titre) => _svc.CreateAsync(titre);
        public Task UpdateQuestionnaireAsync(Questionnaire q) => _svc.UpdateAsync(q);
        public Task DeleteQuestionnaireAsync(int id) => _svc.DeleteAsync(id);

        public Task<List<Question>> ListQuestionsAsync(int questionnaireId) => _svc.ListQuestionsAsync(questionnaireId);
        public Task<int> AddQuestionAsync(int questionnaireId, Question q) => _svc.AddQuestionAsync(questionnaireId, q);
        public Task UpdateQuestionAsync(Question q) => _svc.UpdateQuestionAsync(q);
        public Task DeleteQuestionAsync(int questionId) => _svc.DeleteQuestionAsync(questionId);

        public void Dispose() => _db.Dispose();
    }
}

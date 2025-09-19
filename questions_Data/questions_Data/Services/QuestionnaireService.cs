using Microsoft.EntityFrameworkCore;
using questions_Data.Domain;
using questions_Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_Data.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly AppDbContext _db;
        public QuestionnaireService(AppDbContext db)
        {
            _db = db;
        }

        public Task<List<Questionnaire>> GetAllAsync() => _db.Questionnaires.AsNoTracking().OrderBy(q => q.Titre).ToListAsync();

        public Task<Questionnaire?> GetByIdAsync(int id) => _db.Questionnaires.Include(q => q.Questions).AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);

        public async Task<int> CreateAsync(string? titre)
        {
            var q = new Questionnaire { Titre = titre };
                        _db.Questionnaires.Add(q);
            await _db.SaveChangesAsync();
            return q.Id;
        }

        public async Task UpdateAsync(Questionnaire q)
        {
            _db.Questionnaires.Update(q);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var q = await _db.Questionnaires.FindAsync(id);
            if (q != null)
            {
                _db.Questionnaires.Remove(q);
                await _db.SaveChangesAsync();
            }
        }

        public Task<List<Question>> ListQuestionsAsync(int questionnaireId) => _db.Questions.AsNoTracking()
            .Where(q => q.QuestionnaireId == questionnaireId)
            .OrderBy(q => q.Enonce)
            .ToListAsync();

        // Opérations sur les questions

        public async Task<int> AddQuestionAsync(int questionnaireId, Question q)
        {
            q.QuestionnaireId = questionnaireId;
            _db.Questions.Add(q);
            await _db.SaveChangesAsync();
            return q.Id;
        }

        public async Task UpdateQuestionAsync(Question q)
        {
            _db.Questions.Update(q);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var q = await _db.Questions.FindAsync(questionId);
            if (q != null)
            {
                _db.Questions.Remove(q);
                await _db.SaveChangesAsync();
            }
        }
    }
}

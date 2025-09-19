using questions_Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_Data.Services
{
    public interface IQuestionnaireService
    {
        Task<List<Questionnaire>> GetAllAsync();
        Task<Questionnaire?> GetByIdAsync(int id);
        Task<int> CreateAsync(string? titre);
        Task UpdateAsync(Questionnaire q);
        Task DeleteAsync(int id);

        // Opérations sur les questions
        Task<List<Question>> ListQuestionsAsync(int questionnaireId);
        Task<int> AddQuestionAsync(int questionnaireId, Question q);
        Task UpdateQuestionAsync(Question q);
        Task DeleteQuestionAsync(int questionId);
    }
}

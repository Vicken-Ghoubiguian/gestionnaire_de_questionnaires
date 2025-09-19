using questions_WPF.ViewModel.Abstractions;
//using questions_WPF.Models;
//using questions_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using questions_WPF.Command;
using questions_Data.Domain;
using System.Windows;

namespace questions_WPF.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public ICommand NewCommand { get; set; } = null!;
        public ICommand DeleteCommand { get; set; } = null!;

        public ObservableCollection<Question> Questions { get; set; } = new();

        private Question? selectedQuestion;
        public Question? SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                if (this.selectedQuestion != value)
                {
                    this.selectedQuestion = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            //this.Questions = QuestionService.Questions;
            //this.NewCommand = new MyICommand(this.AddQuestion);
            //this.DeleteCommand = new MyICommand(this.DeleteQuestion);
        }

        public async Task LoadAsync()
        {
            try
            {
                Questions.Clear();

                if (App.CurrentQuestionnaireId is not int qid)
                {
                    MessageBox.Show("Aucun questionnaire sélectionné.");
                    return;
                }
                var list = await App.Ef.ListQuestionsAsync(qid);
                foreach (var q in list) Questions.Add(q);

                if (Questions.Count > 0)
                    SelectedQuestion = Questions[0];
            }
            catch (Exception ex)
            {
                // Gérer l'exception (log, message utilisateur, etc.)
                MessageBox.Show($"Erreur lors du chargement des questions : {ex.Message}");
            }
        }
        private async Task AddQuestion(object? param)
        {
            if (App.CurrentQuestionnaireId is not int qid)
            {
                MessageBox.Show("Aucun questionnaire sélectionné.");
                return;
            }

            var question = new Question
            {
                Enonce = "Nouvelle question",
                Reponse1 = "Réponse 1",
                Reponse2 = "Réponse 2",
                Reponse3 = "Réponse 3",
                Reponse4 = "Réponse 4",
                BonneReponse = "Réponse 1",
            };

            try
            {
                var id = await App.Ef.AddQuestionAsync(qid, question);

                var created = await App.Ef.ListQuestionsAsync(qid);
                var justAdded = created.FirstOrDefault(x => x.Id == id);

                if (justAdded != null)
                {
                    Questions.Add(justAdded);
                    SelectedQuestion = justAdded;
                }
                else
                {
                    await LoadAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la question : {ex.Message}");
            }
            //QuestionService.AddQuestion(question);
            //this.SelectedQuestion = question;
        }

        private async Task DeleteQuestion(object? param)
        {

            var toDelete = (param as Question) ?? SelectedQuestion;
            if (toDelete is null)
            {
                MessageBox.Show("Aucune question sélectionnée pour la suppression.");
                return;
            }

            await App.Ef.DeleteQuestionAsync(toDelete.Id);
            Questions.Remove(toDelete);
        }
    }
}

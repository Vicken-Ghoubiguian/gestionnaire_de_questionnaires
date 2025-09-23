//using questions_WPF.Models;
using questions_Data.Domain;
using questions_WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace questions_WPF.Pages
{
    /// <summary>
    /// Logique d'interaction pour gestionnaire_de_questions.xaml
    /// </summary>
    public partial class gestionnaire_de_questions : Page
    {
        private readonly ObservableCollection<Question> _items = new();
        public gestionnaire_de_questions()
        {
            InitializeComponent();
            QuestionsListBox.ItemsSource = _items;
            Loaded += async (_, _) => await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _items.Clear();

            if (App.CurrentQuestionnaireId is not int qid)
            {
                try
                {
                    this.NavigationService.Navigate(new Uri("View/Pages/gestionnaire_de_questionnaires.xaml", UriKind.Relative));
                }
                catch
                {
                    this.NavigationService.Navigate(new Uri("/View/Pages/gestionnaire_de_questionnaires.xaml", UriKind.Relative));
                }
                return;
            }

            var list = await App.Ef.ListQuestionsAsync(qid);
            foreach (var q in list)
                _items.Add(q);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/Pages/gestionnaire_de_questionnaires.xaml", UriKind.Relative));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (App.CurrentQuestionnaireId is not int qid)
            {
                MessageBox.Show("Aucun questionnaire sélectionné.");
                return;
            }

            var en = enonce.Text?.Trim();
            if (string.IsNullOrWhiteSpace(en))
            {
                MessageBox.Show("L'énoncé ne peut pas être vide.");
                return;
            }

            string? r1 = string.IsNullOrWhiteSpace(reponse1.Text) ? null : reponse1.Text.Trim();
            string? r2 = string.IsNullOrWhiteSpace(reponse2.Text) ? null : reponse2.Text.Trim();
            string? r3 = string.IsNullOrWhiteSpace(reponse3.Text) ? null : reponse3.Text.Trim();
            string? r4 = string.IsNullOrWhiteSpace(reponse4.Text) ? null : reponse4.Text.Trim();
            string? br = string.IsNullOrWhiteSpace(bonneReponse.Text) ? null : bonneReponse.Text.Trim();

            var choices = new[] { r1, r2, r3, r4, br }.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            if(choices.Count == 0)
            {
                MessageBox.Show("Ajoutez au moins une réponse.");
                return;
            }
            var question = new Question
            {
                Enonce = en,
                Reponse1 = r1,
                Reponse2 = r2,
                Reponse3 = r3,
                Reponse4 = r4,
                BonneReponse = br
            };

            var id = await App.Ef.AddQuestionAsync(qid, question);

            var createdList = await App.Ef.ListQuestionsAsync(qid);
            var justAdded = createdList.FirstOrDefault(x => x.Id == id);

            if (justAdded != null)
            {
                _items.Add(justAdded);
                QuestionsListBox.SelectedItem = justAdded;
            }
            else
            {
                await LoadAsync();
            }

            enonce.Clear();
            reponse1.Clear();
            reponse2.Clear();
            reponse3.Clear();
            reponse4.Clear();
            bonneReponse.Clear();

            /* Categorie categorie;

            switch(this.categorie.Text)
            {
                case "sport": categorie = Categorie.sport; break;
                case "art": categorie = Categorie.art; break;
                case "histoire": categorie = Categorie.histoire; break;
                case "sciences": categorie = Categorie.sciences; break;
                case "litterature": categorie = Categorie.litterature; break;
                case "mathematiques": categorie = Categorie.mathematiques; break;
                case "geographie": categorie = Categorie.geographie; break;
                default: categorie = Categorie.bizarre; break;
            }

            QuestionService.AddQuestion(new Models.Question(this.QuestionsListBox.Items.Count + 1, this.enonce.Text, this.reponse1.Text, this.reponse2.Text, this.reponse3.Text, this.reponse4.Text, this.bonneReponse.Text, categorie));
            this.QuestionsListBox.SelectedIndex = this.QuestionsListBox.Items.Count - 1; */
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (QuestionsListBox.SelectedItem is not Question selectedQuestion)
            {
                MessageBox.Show("Séléctionnez une question à supprimer.");
                return;
            }

            try
            {
                await App.Ef.DeleteQuestionAsync(selectedQuestion.Id);
                _items.Remove(selectedQuestion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Suppression impossible" + ex.Message);
            }
        }

        private async void Q_Save_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionsListBox.SelectedItem is not questions_Data.Domain.Question q)
            {
                MessageBox.Show("Sélectionnez une question à modifier.");
                return;
            }

            q.Enonce = enonce.Text?.Trim() ?? "";
            q.Reponse1 = string.IsNullOrWhiteSpace(reponse1.Text) ? null : reponse1.Text.Trim();
            q.Reponse2 = string.IsNullOrWhiteSpace(reponse2.Text) ? null : reponse2.Text.Trim();
            q.Reponse3 = string.IsNullOrWhiteSpace(reponse3.Text) ? null : reponse3.Text.Trim();
            q.Reponse4 = string.IsNullOrWhiteSpace(reponse4.Text) ? null : reponse4.Text.Trim();
            q.BonneReponse = string.IsNullOrWhiteSpace(bonneReponse.Text) ? null : bonneReponse.Text.Trim();

            var choices = new[] { q.Reponse1, q.Reponse2, q.Reponse3, q.Reponse4 }
                          .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            if (string.IsNullOrWhiteSpace(q.Enonce) || choices.Count == 0 ||
                string.IsNullOrWhiteSpace(q.BonneReponse) || !choices.Contains(q.BonneReponse))
            {
                MessageBox.Show("Complétez l’énoncé, une réponse et la bonne réponse.");
                return;
            }

            await App.Ef.UpdateQuestionAsync(q);

            var keepIndex = QuestionsListBox.SelectedIndex;
            await LoadAsync();
            QuestionsListBox.SelectedIndex = Math.Min(keepIndex, QuestionsListBox.Items.Count - 1);

            MessageBox.Show("Enregistré ✅");
        }
    }
}

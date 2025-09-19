using questions_WPF.Models;
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
using questions_Data.Domain;
using questions_Data.Services;

namespace questions_WPF.Pages
{
    /// <summary>
    /// Logique d'interaction pour gestionnaire_de_questionnaires.xaml
    /// </summary>
    public partial class gestionnaire_de_questionnaires : Page
    {
        ObservableCollection<Questionnaire> Questionnaires { get; } = new();
        public gestionnaire_de_questionnaires()
        {
            InitializeComponent();

            QuestionnairesListBox.ItemsSource = Questionnaires;
            Loaded += async (_, _) => await LoadAsync();

            //this.Questionnaires = new() { newQuestionService1, newQuestionService2, newQuestionService3, newQuestionService4, newQuestionService5 };
            //this.QuestionnairesListBox.ItemsSource = Questionnaires;
        }

        private async Task LoadAsync()
        {
            try
            {
                Questionnaires.Clear();
                var all = await App.Ef.GetAllQuestionnairesAsync();
                foreach (var q in all) Questionnaires.Add(q);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de chargement : {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/Pages/gestionnaire_de_questions.xaml", UriKind.Relative));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var enonce = string.IsNullOrWhiteSpace(titreQuestionnaire.Text)
            ? "Sans titre" 
            : titreQuestionnaire.Text.Trim();

            var id = await App.Ef.CreateQuestionnaireAsync(enonce);
            await LoadAsync();

            var created = Questionnaires.FirstOrDefault(x => x.Id == id);
            if (created != null) QuestionnairesListBox.SelectedItem = created;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.QuestionnairesListBox.SelectedItem is not Questionnaire selected)
            {
                MessageBox.Show("Séléctionnez un questionnaire.");
                return;
            }

            if (MessageBox.Show($"Supprimer « {selected.Titre} » ?",
                "Confirmer", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            await App.Ef.DeleteQuestionnaireAsync(selected.Id);
            Questionnaires.Remove(selected);
            //var questionnaires = this.QuestionnairesListBox.ItemsSource as ObservableCollection<QuestionService>;

            //if (questionnaires != null && this.QuestionnairesListBox.SelectedItem is QuestionService selectedQuestionnaire)
            //{
            //    Questionnaires.Remove(selectedQuestionnaire);
            //}
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionnairesListBox.SelectedItem is not questions_Data.Domain.Questionnaire selected)
            {
                MessageBox.Show("Sélectionnez un questionnaire.");
                return;
            }

            App.CurrentQuestionnaireId = selected.Id;

            var uri = new Uri("View/Pages/gestionnaire_de_questions.xaml", UriKind.Relative);
            if (this.NavigationService != null)
                this.NavigationService.Navigate(uri);
            else
                ((System.Windows.Navigation.NavigationWindow)Application.Current.MainWindow).Navigate(uri);
        }
    }
}

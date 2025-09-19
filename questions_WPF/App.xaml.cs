using System.Configuration;
using System.Data;
using System.Windows;

namespace questions_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EfBridge Ef { get; } = new EfBridge();

        public static int? CurrentQuestionnaireId { get; set; }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}

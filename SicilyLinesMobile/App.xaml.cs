using SicilyLinesMobile.Views;
namespace SicilyLinesMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Connexion());
        }
    }
}

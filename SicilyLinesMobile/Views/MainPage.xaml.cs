namespace SicilyLinesMobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnInfoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoClient());
        }

        private async void OnReservationsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservation());
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("AuthToken");

            await DisplayAlert("Déconnexion", "Vous avez été déconnecté avec succès.", "OK");

            Application.Current.MainPage = new NavigationPage(new Connexion());
        }
    }
}

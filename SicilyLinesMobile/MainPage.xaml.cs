using Microsoft.Maui.Storage;

namespace SicilyLinesMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Récupérer le token depuis les préférences
            string token = Preferences.Get("AuthToken", string.Empty);

            // Si un token est trouvé, l'afficher dans le Label
            if (!string.IsNullOrEmpty(token))
            {
                TokenLabel.Text = $"Votre token : {token}";
            }
            else
            {
                TokenLabel.Text = "Token non trouvé. Veuillez vous connecter.";
            }
        }
        private async void OnInfoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoClient());
        }

        private async void OnReservationsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservation());
        }
    }
}

using System.Net.Http.Headers;
using Newtonsoft.Json;
using SicilyLinesMobile.DTOs.Responses;
using SicilyLinesMobile.DTOs.Requests;

namespace SicilyLinesMobile.Views
{
    public partial class InfoClient : ContentPage
    {
        public InfoClient()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string token = GetAuthToken();
            if (token == null) return;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:7221/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await client.GetAsync("api/client/get");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var clientInfo = JsonConvert.DeserializeObject<ClientResponse>(content);

                        NomEntry.Text = clientInfo?.Nom;
                        PrenomEntry.Text = clientInfo?.Prenom;
                        EmailEntry.Text = clientInfo?.Email;
                        CodePostalEntry.Text = clientInfo?.Cp;
                        VilleEntry.Text = clientInfo?.Ville;
                        AdresseEntry.Text = clientInfo?.Adresse;
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Échec de la récupération des informations", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Erreur lors de la communication avec l'API : {ex.Message}", "OK");
                }
            }
        } 

        // Méthode pour le bouton "Modifier mes informations"
        private async void OnModifyButtonClicked(object sender, EventArgs e)
        {
            var updateRequest = new UpdateClientRequest
            {
                Adresse = AdresseEntry.Text,
                Cp = CodePostalEntry.Text,
                Ville = VilleEntry.Text
            };

            string token = GetAuthToken();
            if (token == null) return;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:7221/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var jsonContent = JsonConvert.SerializeObject(updateRequest);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("api/client/update", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Succès", "Vos informations ont été modifiées avec succès", "OK");
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Échec de la modification des informations", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Erreur lors de la communication avec l'API : {ex.Message}", "OK");
                }
            }
        }

        // Méthode pour récupérer le token d'authentification
        private string GetAuthToken()
        {
            string token = Preferences.Get("AuthToken", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                DisplayAlert("Erreur", "Token d'authentification manquant", "OK");
                return null;
            }

            return token;
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("AuthToken");

            await DisplayAlert("Déconnexion", "Vous avez été déconnecté avec succès.", "OK");

            Application.Current.MainPage = new NavigationPage(new Connexion());
        }
    }
}

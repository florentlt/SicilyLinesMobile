using Newtonsoft.Json;
using System.Text;
using SicilyLinesMobile.DTOs.Responses;

namespace SicilyLinesMobile.Views
{
    public partial class Connexion : ContentPage
    {
        private const string ApiUrl = "https://localhost:7221/api/auth/login";


        public Connexion()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string emailText = email.Text;
            string passText = pass.Text;

            // Conversion en Json des données entrées 
            var loginData = new { email = emailText, password = passText };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Envoi de la requête POST
                    HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Connexion réussie
                        var responseData = JsonConvert.DeserializeObject<LoginResponse>(result);
                        if (responseData != null)
                        {
                            // Stockage du token dans les préférences
                            Preferences.Set("AuthToken", responseData.Token);

                            await DisplayAlert("Succès", responseData.Message, "OK");

                            // Redirection vers la page principale
                            Application.Current.MainPage = new NavigationPage(new MainPage());                        }
                    }
                    else
                    {
                        // Gestion d'un échec de la connexion
                        var responseData = JsonConvert.DeserializeObject<LoginResponse>(result);
                        if (responseData != null)
                        {
                            await DisplayAlert("Erreur", responseData.Message, "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gestion des erreurs réseau ou serveur
                    await DisplayAlert("Erreur", $"Problème de connexion : {ex.Message}", "OK");
                }
            }
        }
    }
}

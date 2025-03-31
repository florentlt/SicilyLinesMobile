using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using Newtonsoft.Json;
using SicilyLinesMobile.Models;

namespace SicilyLinesMobile
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

            // R�cup�rer le token d'authentification
            string token = Preferences.Get("AuthToken", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                // Si le token est vide ou null, afficher un message d'erreur
                await DisplayAlert("Erreur", "Token d'authentification manquant", "OK");
                return;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    // Configurer l'URL de l'API et ajouter l'en-t�te d'authentification
                    client.BaseAddress = new Uri("https://localhost:7221/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Effectuer la requ�te GET pour r�cup�rer les informations du client
                    var response = await client.GetAsync("api/client/get");

                    if (response.IsSuccessStatusCode)
                    {
                        // Lire la r�ponse et la d�s�rialiser en objet Client
                        var content = await response.Content.ReadAsStringAsync();
                        var clientInfo = JsonConvert.DeserializeObject<Client>(content);

                        // Mettre � jour les entr�es avec les donn�es r�cup�r�es
                        NomEntry.Text = clientInfo.Nom;
                        PrenomEntry.Text = clientInfo.Prenom;
                        EmailEntry.Text = clientInfo.Email;
                        CodePostalEntry.Text = clientInfo.Cp;
                        VilleEntry.Text = clientInfo.Ville;
                        AdresseEntry.Text = clientInfo.Adresse;
                    }
                    else
                    {
                        // Si la r�ponse n'est pas un succ�s, afficher un message d'erreur
                        await DisplayAlert("Erreur", "�chec de la r�cup�ration des informations", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // En cas d'exception (ex : probl�me r�seau), afficher un message d'erreur
                    await DisplayAlert("Erreur", $"Erreur lors de la communication avec l'API : {ex.Message}", "OK");
                }
            }
        }


        // M�thode pour le bouton "Modifier mes informations"
        private async void OnModifyButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Modification", "Vos informations ont �t� modifi�es !", "OK");
        }
    }
}

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

            // Récupérer le token d'authentification
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
                    // Configurer l'URL de l'API et ajouter l'en-tête d'authentification
                    client.BaseAddress = new Uri("https://localhost:7221/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Effectuer la requête GET pour récupérer les informations du client
                    var response = await client.GetAsync("api/client/get");

                    if (response.IsSuccessStatusCode)
                    {
                        // Lire la réponse et la désérialiser en objet Client
                        var content = await response.Content.ReadAsStringAsync();
                        var clientInfo = JsonConvert.DeserializeObject<Client>(content);

                        // Mettre à jour les entrées avec les données récupérées
                        NomEntry.Text = clientInfo.Nom;
                        PrenomEntry.Text = clientInfo.Prenom;
                        EmailEntry.Text = clientInfo.Email;
                        CodePostalEntry.Text = clientInfo.Cp;
                        VilleEntry.Text = clientInfo.Ville;
                        AdresseEntry.Text = clientInfo.Adresse;
                    }
                    else
                    {
                        // Si la réponse n'est pas un succès, afficher un message d'erreur
                        await DisplayAlert("Erreur", "Échec de la récupération des informations", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // En cas d'exception (ex : problème réseau), afficher un message d'erreur
                    await DisplayAlert("Erreur", $"Erreur lors de la communication avec l'API : {ex.Message}", "OK");
                }
            }
        }


        // Méthode pour le bouton "Modifier mes informations"
        private async void OnModifyButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Modification", "Vos informations ont été modifiées !", "OK");
        }
    }
}

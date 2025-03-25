using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using SicilyLinesMobile.Models;
using System.Diagnostics;

namespace SicilyLinesMobile
{
    public partial class Connexion : ContentPage
    {
        private const string ApiUrl = "https://localhost:7221/Login";

        public Connexion()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string emailText = email.Text;  
            string passText = pass.Text; 

            var loginData = new { email = emailText, password = passText };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Envoi de la requête POST avec les données dans le corps
                    HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Si la connexion est réussie, on récupère la réponse et redirige l'utilisateur
                        var responseData = JsonConvert.DeserializeObject<LoginResponse>(result);
                        if (responseData != null)
                        {
                            await DisplayAlert("Succès", responseData.Message, "OK");

                            // Redirection vers la page principale (à adapter selon ta logique)
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            // Si responseData est null, afficher un message d'erreur
                            await DisplayAlert("Erreur", "Réponse du serveur invalide.", "OK");
                        }
                    }
                    else
                    {
                        // Si l'authentification échoue
                        var errorData = JsonConvert.DeserializeObject<LoginError>(result);
                        if (errorData != null)
                        {
                            errorMessage.Text = errorData.Error;
                            errorMessage.IsVisible = true;
                        }
                        else
                        {
                            // Si errorData est null, afficher un message d'erreur générique
                            errorMessage.Text = "Erreur";
                            errorMessage.IsVisible = true;
                            Debug.WriteLine(result);
                        }
                    }


                }
                catch (Exception ex)
                {
                    // Si la requête échoue (erreur réseau, serveur, etc.)
                    errorMessage.Text = $"Erreur : {ex.Message}";
                    errorMessage.IsVisible = true;
                }

            }
        }
    }
}

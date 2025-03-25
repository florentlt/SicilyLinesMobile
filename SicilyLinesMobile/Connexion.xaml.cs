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
                    // Envoi de la requ�te POST avec les donn�es dans le corps
                    HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Si la connexion est r�ussie, on r�cup�re la r�ponse et redirige l'utilisateur
                        var responseData = JsonConvert.DeserializeObject<LoginResponse>(result);
                        if (responseData != null)
                        {
                            await DisplayAlert("Succ�s", responseData.Message, "OK");

                            // Redirection vers la page principale (� adapter selon ta logique)
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            // Si responseData est null, afficher un message d'erreur
                            await DisplayAlert("Erreur", "R�ponse du serveur invalide.", "OK");
                        }
                    }
                    else
                    {
                        // Si l'authentification �choue
                        var errorData = JsonConvert.DeserializeObject<LoginError>(result);
                        if (errorData != null)
                        {
                            errorMessage.Text = errorData.Error;
                            errorMessage.IsVisible = true;
                        }
                        else
                        {
                            // Si errorData est null, afficher un message d'erreur g�n�rique
                            errorMessage.Text = "Erreur";
                            errorMessage.IsVisible = true;
                            Debug.WriteLine(result);
                        }
                    }


                }
                catch (Exception ex)
                {
                    // Si la requ�te �choue (erreur r�seau, serveur, etc.)
                    errorMessage.Text = $"Erreur : {ex.Message}";
                    errorMessage.IsVisible = true;
                }

            }
        }
    }
}

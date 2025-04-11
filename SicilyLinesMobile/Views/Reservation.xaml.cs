using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SicilyLinesMobile.DTOs.Responses;

namespace SicilyLinesMobile.Views;

public partial class Reservation : ContentPage
{
    public ObservableCollection<ReservationResponse> Reservations { get; set; } = new();

    public Reservation()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadReservations();
    }

    private async Task LoadReservations()
    {
        string token = Preferences.Get("AuthToken", string.Empty);

        if (string.IsNullOrEmpty(token))
        {
            await DisplayAlert("Erreur", "Token d'authentification manquant", "OK");
            return;
        }

        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri("https://localhost:7221/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/client/reservations");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var reservationsList = JsonConvert.DeserializeObject<List<ReservationResponse>>(content);

                    if (reservationsList != null)
                    {
                        var upcomingReservations = new ObservableCollection<ReservationResponse>();
                        var pastReservations = new ObservableCollection<ReservationResponse>();
                        DateTime currentDate = DateTime.Now;

                        foreach (var reservation in reservationsList)
                        {
                            if (reservation.DateDepart >= currentDate)
                            {
                                upcomingReservations.Add(reservation);
                            }
                            else
                            {
                                pastReservations.Add(reservation);
                            }
                        }

                        lvUpcoming.ItemsSource = upcomingReservations;
                        lvPast.ItemsSource = pastReservations;
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Échec de la récupération des réservations", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Erreur lors de la communication avec l'API : {ex.Message}", "OK");
            }
        }
    }
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        Preferences.Remove("AuthToken");

        await DisplayAlert("Déconnexion", "Vous avez été déconnecté avec succès.", "OK");

        Application.Current.MainPage = new NavigationPage(new Connexion());
    }

}

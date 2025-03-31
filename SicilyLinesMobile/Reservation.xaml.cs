using SicilyLinesMobile.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SicilyLinesMobile;

public partial class Reservation : ContentPage
{
    public ObservableCollection<ReservationModel> Reservations { get; set; } = new();

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
                    var reservationsList = JsonConvert.DeserializeObject<List<ReservationModel>>(content);

                    if (reservationsList != null)
                    {
                        var upcomingReservations = new ObservableCollection<ReservationModel>();
                        var pastReservations = new ObservableCollection<ReservationModel>();
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

}

namespace API_Sicily.Models
{
    //Utilisation de cette classe lors de l'authentification
    public class ClientAuth
    {
        public int IdClient { get; set; }
        public string Mdp { get; set; }

        public ClientAuth(int idClient, string mdp)
        {
            IdClient = idClient;
            Mdp = mdp;
        }
    }

}

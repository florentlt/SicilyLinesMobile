namespace API_Sicily.Models
{
    public class Client
    {
        public int IdClient { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Mdp { get; set; }

        public string Adresse { get; set; }

        public string Cp { get; set; }

        public string Ville { get; set; }

        // Constructeur par défaut pour la désérialisation JSON
        public Client() { }

        // Constructeur principal
        public Client(int idClient, string nom, string prenom, string email, string mdp, string adresse, string cp, string ville)
        {
            this.IdClient = idClient;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Mdp = mdp;
            this.Adresse = adresse;
            this.Cp = cp;
            this.Ville = ville;
        }

        // Propriété Description pour affichage
        public string Description
        {
            get => $"Id : {IdClient} | Nom : {Nom} | Prenom : {Prenom} | Email : {Email} | Mot de Passe : {Mdp} | Adresse : {Adresse} | CP : {Cp} | Ville : {Ville}";
        }
    }
}

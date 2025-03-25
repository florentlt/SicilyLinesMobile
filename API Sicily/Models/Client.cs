namespace API_Sicily.Models
{
    public class Client
    {
        private int idClient;
        private string nom;
        private string prenom;
        private string email;
        private string mdp;
        private string adresse;
        private string cp;
        private string ville;

        public int IdClient { get => idClient; set => idClient = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Cp { get => cp; set => cp = value; }
        public string Ville { get => ville; set => ville = value; }

        // Constructeur principal
        public Client(int idClient, string nom, string prenom, string email, string mdp, string adresse, string cp, string ville)
        {
            this.idClient = idClient;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            this.mdp = mdp;
            this.adresse = adresse;
            this.cp = cp;
            this.ville = ville;
        }

        // Propriété Description pour affichage
        public string Description
        {
            get => $"Id : {idClient} | Nom : {nom} | Prenom : {prenom} | Email : {email} | Mot de Passe : {mdp} | Adresse : {adresse} | CP : {cp} | Ville : {ville}";
        }
    }
}
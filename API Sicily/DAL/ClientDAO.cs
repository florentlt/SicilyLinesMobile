using MySql.Data.MySqlClient;
using API_Sicily.Models;

namespace API_Sicily.DAL
{
    public class ClientDAO
    {
        // Paramètres de connexion
        private static string provider = "localhost";
        private static string dataBase = "sicilylinesmobile";
        private static string uid = "root";
        private static string mdp = "";

        private static ConnexionSql? maConnexionSql;
        private static MySqlCommand? Ocom;

        // Authentification d'un client
        public static ClientAuth? GetClientByAuth(string email)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                string query = "SELECT idClient, mdp FROM Client WHERE email = @Email";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@Email", email);

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idClient = reader.GetInt32("idClient");
                        string mdp = reader.GetString("mdp");

                        return new ClientAuth(idClient, mdp);
                    }
                }

                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération du client : " + ex.Message);
            }

            return null;
        }

        // Get des infos du client
        public static Client? GetClientById(int idClient)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                string query = "SELECT * FROM Client WHERE idClient = @IdClient";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@IdClient", idClient);

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nom = reader.GetString("nom");
                        string prenom = reader.GetString("prenom");
                        string email = reader.GetString("email");
                        string mdp = reader.GetString("mdp");
                        string adresse = reader.IsDBNull(reader.GetOrdinal("adresse")) ? "" : reader.GetString("adresse");
                        string cp = reader.IsDBNull(reader.GetOrdinal("cp")) ? "" : reader.GetString("cp");
                        string ville = reader.IsDBNull(reader.GetOrdinal("ville")) ? "" : reader.GetString("ville");

                        return new Client(idClient, nom, prenom, email, mdp, adresse, cp, ville);
                    }
                }

                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération du client : " + ex.Message);
            }

            return null;
        }


        // Update des infos client
        public static bool UpdateClientInfoById(int idClient, string adresse, string cp, string ville)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                string query = "UPDATE Client SET adresse = @Adresse, cp = @Cp, ville = @Ville WHERE idClient = @IdClient";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@Adresse", adresse);
                Ocom.Parameters.AddWithValue("@Cp", cp);
                Ocom.Parameters.AddWithValue("@Ville", ville);
                Ocom.Parameters.AddWithValue("@IdClient", idClient);

                int rowsAffected = Ocom.ExecuteNonQuery();
                maConnexionSql.closeConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour des informations du client : " + ex.Message);
            }
        }


        // Get des reservations du client
        public static List<Reservation> GetReservationsById(int idClient)
        {
            List<Reservation> reservations = new List<Reservation>();

            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                string query = @"SELECT t.libelle AS Nom_Traversee, t.DATETRAVERSEE AS Date_Depart 
                         FROM reservation r
                         JOIN traversee t ON r.IDLIAISON = t.IDLIAISON AND r.IDTRAVERSEE = t.IDTRAVERSEE
                         JOIN client c ON r.IDCLIENT = c.IDCLIENT
                         WHERE c.IDCLIENT = @IdClient";

                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@IdClient", idClient);

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nomTraversee = reader.GetString("Nom_Traversee");
                        DateTime dateDepart = reader.GetDateTime("Date_Depart");

                        reservations.Add(new Reservation
                        {
                            NomTraversee = nomTraversee,
                            DateDepart = dateDepart
                        });
                    }
                }

                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des réservations : " + ex.Message);
            }

            return reservations;
        }

    }
}

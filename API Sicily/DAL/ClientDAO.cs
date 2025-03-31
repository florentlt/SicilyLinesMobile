using System;
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
        public static Client? GetClientByEmail(string email)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                string query = "SELECT * FROM Client WHERE email = @Email";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@Email", email);

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int idClient = reader.GetInt32("idClient");
                        string nom = reader.GetString("nom");
                        string prenom = reader.GetString("prenom");
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

        // Get infos du client
        public static Client? GetClientInfoByEmail(string email)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                // Requête SQL avec les noms de colonnes
                string query = "SELECT CP, ADRESSE, VILLE, EMAIL, NOM, PRENOM FROM Client WHERE EMAIL = @Email";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@Email", email);

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Récupération des informations spécifiques
                        string cp = reader.IsDBNull(reader.GetOrdinal("CP")) ? "" : reader.GetString("CP");
                        string adresse = reader.IsDBNull(reader.GetOrdinal("ADRESSE")) ? "" : reader.GetString("ADRESSE");
                        string ville = reader.IsDBNull(reader.GetOrdinal("VILLE")) ? "" : reader.GetString("VILLE");
                        string emailFromDb = reader.GetString("EMAIL");
                        string nom = reader.GetString("NOM");
                        string prenom = reader.GetString("PRENOM");

                        // Retourner un objet Client avec ces informations
                        return new Client(0, nom, prenom, emailFromDb, "", adresse, cp, ville);
                    }
                }

                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des informations du client : " + ex.Message);
            }

            return null;
        }

        // Mettre à jour les informations du client (adresse, cp, ville)
        public static bool UpdateClientInfoByEmail(string email, string adresse, string cp, string ville)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                // Requête SQL pour mettre à jour les informations du client
                string query = "UPDATE Client SET ADRESSE = @Adresse, CP = @Cp, VILLE = @Ville WHERE EMAIL = @Email";
                Ocom = maConnexionSql.reqExec(query);
                Ocom.Parameters.AddWithValue("@Email", email);
                Ocom.Parameters.AddWithValue("@Adresse", adresse);
                Ocom.Parameters.AddWithValue("@Cp", cp);
                Ocom.Parameters.AddWithValue("@Ville", ville);

                // Exécution de la requête
                int rowsAffected = Ocom.ExecuteNonQuery();

                maConnexionSql.closeConnection();

                // Si des lignes ont été affectées, cela signifie que la mise à jour a réussi
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour des informations du client : " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
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

        // Récupération de la liste des clients
        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            try
            {
                // Initialisation de la connexion
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                // Exécution de la requête SQL
                Ocom = maConnexionSql.reqExec("SELECT * FROM Client");

                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idClient = reader.GetInt32("idClient");
                        string nom = reader.GetString("nom");
                        string prenom = reader.GetString("prenom");
                        string email = reader.GetString("email");
                        string mdp = reader.GetString("mdp");
                        string adresse = reader.IsDBNull(reader.GetOrdinal("adresse")) ? "" : reader.GetString("adresse");
                        string cp = reader.IsDBNull(reader.GetOrdinal("cp")) ? "" : reader.GetString("cp");
                        string ville = reader.IsDBNull(reader.GetOrdinal("ville")) ? "" : reader.GetString("ville");

                        clients.Add(new Client(idClient, nom, prenom, email, mdp, adresse, cp, ville));
                    }
                }

                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des clients : " + ex.Message);
            }

            return clients;
        }

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
    }
}

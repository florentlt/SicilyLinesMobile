using MySql.Data.MySqlClient;

namespace API_Sicily.DAL
{
    public class ConnexionSql
    {
        private static ConnexionSql? instance;
        private MySqlConnection connection;

        private ConnexionSql(string provider, string database, string uid, string mdp)
        {
            string connectionString = $"Server={provider};Database={database};Uid={uid};Pwd={mdp};";
            connection = new MySqlConnection(connectionString);
        }

        public static ConnexionSql getInstance(string provider, string database, string uid, string mdp)
        {
            if (instance == null)
            {
                instance = new ConnexionSql(provider, database, uid, mdp);
            }
            return instance;
        }

        public void openConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public MySqlCommand reqExec(string query)
        {
            return new MySqlCommand(query, connection);
        }
    }
}

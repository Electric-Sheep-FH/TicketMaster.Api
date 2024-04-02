using System.Data.SqlClient;

namespace test_TicketMaster.Api.Database
{
    public class Connection
    {
        private static SqlConnection _connection;

        // On met le constructeur en private pour éviter que la classe soit instanciée
        private Connection() { }

        // On créer une méthode static pour gérer l'instanciation de la classe (et de la connexion)
        public static SqlConnection GetInstance()
        {
            if (_connection == null)
                _connection = Connect();

            return _connection;
        }

        private static SqlConnection Connect()
        {
            try
            {
                var connectionString = @"Server=DESKTOP-77GB89E\SQLEXPRESS;Database=testTicketMaster;Trusted_Connection=True;Encrypt=False";

                var connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to database.");

                return connection;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while connecting to DB : {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error : {0}", ex.Message);
                throw;
            }
        }
    }
}

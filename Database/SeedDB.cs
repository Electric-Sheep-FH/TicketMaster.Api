using System.Text;
using System.Data.SqlClient;

namespace test_TicketMaster.Api.Database
{
    public class SeedDB
    {
        public static void FeedDB()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("USE testTicketMaster;");
            sb.Append("INSERT INTO Personne (nom, prenom, date_naissance, adresse, code_postal, ville, telephone_fixe, telephone_portable, email, password_account, ID_role, ID_pays) VALUES ('Doe', 'John', '1990-01-01', '123 Rue des Fleurs', 12345, 'VilleA', '0123456789', '0123456789', 'john.doe@example.com', 'password123', 1, 1)");
            string sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, Connection.GetInstance()))
            {
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Seed DB Done.");
                Console.WriteLine("Number of rows inserted : {0}", rowsAffected);
            }
        }
    }
}

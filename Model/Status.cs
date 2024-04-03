using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

namespace test_TicketMaster.Api.Model
{
    public class Status
    {
        public string Name { get; set; }

        public static void CreateStatus(string statusName)
        {
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO Statut_technicien (nom) VALUES (@statusName)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@statusName", statusName);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Status added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add status.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating Status: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating Status: {0}", ex.Message);
                throw;
            }
        }
    }
}

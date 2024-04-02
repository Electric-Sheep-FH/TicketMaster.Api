using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

namespace test_TicketMaster.Api.Model
{
    public class Country
    {
        public string Name { get; set; }

        public static void CreateCountry(string countryName)
        {
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();
                string query = "INSERT INTO Pays (nom) VALUES (@CountryName)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", countryName);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Country added succesfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add country.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating country: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating country: {0}", ex.Message);
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

namespace test_TicketMaster.Api.Model
{
    public class Users : Person
    {
        public static void CreateTechnician(string firstname, string lastname, DateOnly birthday, string adress, int postalCode, string city, string localPhone, string portablePhone, string mail, string password, int idCountry)
        {
            int userId = CreatePerson(firstname, lastname, birthday, adress, postalCode, city, localPhone, portablePhone, mail, password, idCountry, 1);

            if (userId != -1)
            {
                SqlConnection connection = null;
                try
                {
                    connection = Connection.GetInstance();

                    string query2 = @"INSERT INTO Utilisateur (ID_Personne, ID_statut) VALUES (@PersonId, @StatusID)";

                    using (var command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", userId);
                        command.Parameters.AddWithValue("@StatusID", 1);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Technician added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add Technician.");
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception while creating Technician: {0}", sqlEx.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error while creating Technician: {0}", ex.Message);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Failed to create Technician. Person ID is invalid.");
            }

        }
    }
}

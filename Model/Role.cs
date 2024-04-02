using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using test_TicketMaster.Api.Database;
using System.Data;

namespace test_TicketMaster.Api.Model
{
    public class Role
    {
        public string Name { get; set; }

        public static void CreateRole(string roleName) 
        {
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = "INSERT INTO Role (Nom) VALUES (@RoleName)";
                    
                using(var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleName", roleName);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Role added succesfully.");
                    } else
                    {
                        Console.WriteLine("Failed to add role.");
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating role: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating role: {0}", ex.Message);
                throw;
            }
        }
    }
}

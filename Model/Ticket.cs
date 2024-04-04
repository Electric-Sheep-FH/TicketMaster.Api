using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

namespace test_TicketMaster.Api.Model
{
    public class Ticket
    {
        public static int CreateTicket(int dysfunctionId, int emergencyId, int clientId) 
        {
            int ticketId = -1;

            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO Ticket (ID_dysfonctionnement,ID_categorieUrgence,ID_client) VALUES (@Dysfunction, @Emergency, @ClientID); SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Dysfunction", dysfunctionId);
                    command.Parameters.AddWithValue("@Emergency", emergencyId);
                    command.Parameters.AddWithValue("@ClientID", clientId);


                    ticketId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating Ticket : {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating Ticket : {0}", ex.Message);
                throw;
            }

            return ticketId;
        }
        
    }
}

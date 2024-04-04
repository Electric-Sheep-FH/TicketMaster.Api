using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

namespace test_TicketMaster.Api.Model
{
    public class TicketHistory
    {
        public DateTime StatusDate { get; set; }
        public string Observation { get; set; }

        public static void CreateHistoriedTicket(int dysfunctionId, int emergencyId, int clientId, string observation, int userId)
        {
            string creationDate = DateTime.Now.ToString();
            int ticketId = Ticket.CreateTicket(dysfunctionId, emergencyId, clientId);

            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO historique_ticket (date_statut, obs, ID_etat_ticket, ID_utilisateur, Ticket_Id) VALUES (@Date, @Obs, @ticketState, @userID, @ticketID);";

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Date", creationDate);
                    command.Parameters.AddWithValue("@Obs", observation);
                    command.Parameters.AddWithValue("@ticketState", 1);
                    command.Parameters.AddWithValue("@userID", userId);
                    command.Parameters.AddWithValue("@ticketID", ticketId);


                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating ticket with history : {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating ticket with history : {0}", ex.Message);
                throw;
            }
        }

        public static void UpdateExistingTicketById(int ticketId, string observation, int stateId, int userId)
        {
            string date = DateTime.Now.ToString();
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO historique_ticket (date_statut, obs, ID_etat_ticket, ID_utilisateur, Ticket_Id) VALUES (@date, @obs, @state,@userId,@tickerId);";

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@obs", observation);
                    command.Parameters.AddWithValue("@state", stateId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@tickerId", ticketId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while updating Ticket : {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while updating Ticket : {0}", ex.Message);
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;
using test_TicketMaster.Api.DTO.Responses;

namespace test_TicketMaster.Api.Model
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly Birthday { get; set; }
        public string Adress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string LocalPhoneNumber { get; set; }
        public string PortablePhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public int IdCountry { get; set; }

        public static int CreatePerson(string firstname, string lastname, DateOnly birthday, string adress, int postalCode, string city, string localPhone, string portablePhone, string mail, string password, int idRole, int idCountry)
        {
            string birthdayString = birthday.ToString("yyyy-MM-dd");
            int personId = -1;

            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO Personne (nom, prenom, date_naissance, adresse, code_postal, ville, telephone_fixe, telephone_portable, email, password_account, ID_role, ID_pays) VALUES (@Nom, @Prenom, @DateNaissance, @Adresse, @CodePostal, @Ville, @TelephoneFixe, @TelephonePortable, @Email, @PasswordAccount, @IDPays, @IDRole); SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Nom", lastname);
                    command.Parameters.AddWithValue("@Prenom", firstname);
                    command.Parameters.AddWithValue("@DateNaissance", birthdayString);
                    command.Parameters.AddWithValue("@Adresse", adress);
                    command.Parameters.AddWithValue("@CodePostal", postalCode);
                    command.Parameters.AddWithValue("@Ville", city);
                    command.Parameters.AddWithValue("@TelephoneFixe", localPhone);
                    command.Parameters.AddWithValue("@TelephonePortable", portablePhone);
                    command.Parameters.AddWithValue("@Email", mail);
                    command.Parameters.AddWithValue("@PasswordAccount", password);
                    command.Parameters.AddWithValue("@IDPays", idCountry);
                    command.Parameters.AddWithValue("@IDRole", idRole);


                    personId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating Person: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating Person: {0}", ex.Message);
                throw;
            }

            return personId;
        }
        public static List<Person> GetPersonByIdRole(int idRole)
        {
            List<Person> persons = new List<Person>();
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"SELECT * FROM Personne WHERE ID_role = @IDRole;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDRole", idRole);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var person = new Person
                            {
                                Firstname = reader["prenom"].ToString(),
                                Lastname = reader["nom"].ToString(),
                                Adress = reader["adresse"].ToString(),
                                PostalCode = Convert.ToInt32(reader["code_postal"]),
                                IdRole = Convert.ToInt32(reader["ID_role"])
                            };

                            persons.Add(person);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating Person: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating Person: {0}", ex.Message);
                throw;
            }

            return persons;
        }
        public static int GetIdRoleByPersonId(int personId)
        {
            int toReturn = -1;

            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"SELECT ID_role FROM Personne WHERE ID_Personne = @IDPerson;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDPerson", personId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            toReturn = Convert.ToInt32(reader["ID_role"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while creating Person: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while creating Person: {0}", ex.Message);
                throw;
            }

            return toReturn;
        }
        public static void DeletePersonById(int personId)
        {
            int idRole = GetIdRoleByPersonId(personId);
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();
                string query = @"";

                if (idRole == 1 || idRole == 2)
                {
                    query += @"DELETE FROM Utilisateur WHERE ID_Personne = @IDPerson; DELETE FROM Personne WHERE ID_Personne = @IDPerson;";
                }
                else
                {
                    query += @"DELETE FROM Client WHERE ID_Personne = @IDPerson; DELETE FROM Personne WHERE ID_Personne = @IDPerson;";
                }

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDPerson", personId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Exception while deleting Person: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while deleting Person: {0}", ex.Message);
                throw;
            }
        }
        public static void DeleteByIdRole(int idToDelete)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            try
            {
                connection = Connection.GetInstance();
                transaction = connection.BeginTransaction();

                // Supprimer les entrées dans la table Client liées à l'ID_role spécifié
                string deleteClientQuery = @"DELETE FROM Client WHERE ID_Personne IN (SELECT ID_personne FROM Personne WHERE ID_role = @IdToDelete)";
                using (var deleteClientCommand = new SqlCommand(deleteClientQuery, connection, transaction))
                {
                    deleteClientCommand.Parameters.AddWithValue("@IdToDelete", idToDelete);
                    deleteClientCommand.ExecuteNonQuery();
                }

                // Ensuite, supprimer les entrées dans la table Personne
                string deletePersonQuery = @"DELETE FROM Personne WHERE ID_role = @IdToDelete";
                using (var deletePersonCommand = new SqlCommand(deletePersonQuery, connection, transaction))
                {
                    deletePersonCommand.Parameters.AddWithValue("@IdToDelete", idToDelete);
                    deletePersonCommand.ExecuteNonQuery();
                }

                // Valider la transaction
                transaction.Commit();
                Console.WriteLine("Delete operation completed successfully.");
            }
            catch (SqlException sqlEx)
            {
                // Rollback la transaction en cas d'erreur
                transaction?.Rollback();
                Console.WriteLine("SQL Exception while deleting records: {0}", sqlEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Rollback la transaction en cas d'erreur
                transaction?.Rollback();
                Console.WriteLine("Unexpected error while deleting records: {0}", ex.Message);
                throw;
            }
        }
    }
}

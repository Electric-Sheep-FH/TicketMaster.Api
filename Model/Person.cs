using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;

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

        public static void CreatePerson(string firstname, string lastname, DateOnly birthday, string adress, int postalCode, string city, string localPhone, string portablePhone, string mail, string password, int idRole, int idCountry)
        {
            string birthdayString = birthday.ToString("yyyy-MM-dd");
            SqlConnection connection = null;
            try
            {
                connection = Connection.GetInstance();

                string query = @"INSERT INTO Personne (nom, prenom, date_naissance, adresse, code_postal, ville, telephone_fixe, telephone_portable, email, password_account, ID_role, ID_pays) VALUES (@Nom, @Prenom, @DateNaissance, @Adresse, @CodePostal, @Ville, @TelephoneFixe, @TelephonePortable, @Email, @PasswordAccount, @IDRole, @IDPays)";

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
                    command.Parameters.AddWithValue("@IDRole", idRole);
                    command.Parameters.AddWithValue("@IDPays", idCountry);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Person added succesfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add Person.");
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
        }
    }
}

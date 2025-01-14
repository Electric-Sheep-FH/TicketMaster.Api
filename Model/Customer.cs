﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_TicketMaster.Api.Database;
using test_TicketMaster.Api.Endpoints;

namespace test_TicketMaster.Api.Model
{
    public class Customer : Person
    {
        public static void CreateCustomer(string firstname, string lastname, DateOnly birthday, string adress, int postalCode, string city, string localPhone, string portablePhone, string mail, string password, int idCountry)
        {
            int userId = CreatePerson(firstname,lastname,birthday,adress,postalCode,city,localPhone,portablePhone,mail,password,idCountry,3);

            if (userId != -1)
            {
                SqlConnection connection = null;
                try
                {
                    connection = Connection.GetInstance();

                    string query2 = @"INSERT INTO Client (ID_Personne) VALUES (@PersonId)";

                    using (var command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", userId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Customer added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add Customer.");
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("SQL Exception while creating Customer: {0}", sqlEx.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error while creating Customer: {0}", ex.Message);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Failed to create Customer. Person ID is invalid.");
            }
        }
    }
}

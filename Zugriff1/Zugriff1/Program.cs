using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
  static void Main(string[] args)
  {
    string connectionString = "Server=PC118004\\SQLEXPRESS;" +
                              "Database=Testdb;" +
                              "Trusted_Connection=True;" +
                              "TrustServerCertificate=True;";
        
    // Benutzer hinzufügen
    AddUser(connectionString, "Max Mustermann", "max.mustermann@example.com");
        
    // Benutzer abfragen
    GetUsers(connectionString);
  }

  static void AddUser(string connectionString, string name, string email)
  {
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
      connection.Open();

      string query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
      using (SqlCommand command = new SqlCommand(query, connection))
      {
        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
        command.ExecuteNonQuery();
      }
    }
  }

  static void GetUsers(string connectionString)
  {
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
      connection.Open();

      string query = "SELECT * FROM Users";
      using (SqlCommand command = new SqlCommand(query, connection))
      {
        using (SqlDataReader reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            Console.WriteLine($"{reader["Id"]}: {reader["Name"]} - {reader["Email"]}");
          }
        }
      }
    }
  }
}
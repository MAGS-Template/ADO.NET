using System.Data.SqlClient;
using Domain_Models;
namespace BlazorADONET.Services
{
    public class DatabaseService : IDatabaseService
    {
        public List<Person> SearchPersons(string selectedOption, string searchValue)
        {
            // Implement the search logic here
            // For example:
            string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ADONET;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = $"SELECT * FROM Persons WHERE {selectedOption} = @Value";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Value", searchValue);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Person> persons = new List<Person>();
                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });
                        }
                        return persons;
                    }
                }
            }
        }
        public List<string> GetColumnNames()
        {
            List<string> columnNames = new List<string>();

            string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ADONET;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Persons'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columnNames.Add(reader["COLUMN_NAME"].ToString());
                        }
                    }
                }
            }

            return columnNames;
        }
    }z
}

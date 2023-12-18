using System;
using System.Data.SqlClient;

class Program
{
    private string firstName = "John";
    private string lastName = "Doe";

    static void Main()
    {
        Program program = new Program();
        program.Init();
    }

    private void Init()
    {
        try
        {
            // Specify the master database for creating the target database
            string masterConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string targetDatabaseName = "ADONET";  // Change the database name to "ADONET"

            // Create the target database if it does not exist
            CreateDatabase(masterConnectionString, targetDatabaseName);

            // Insert data into the Persons table
            InsertData(targetDatabaseName, firstName, lastName);

            ReadData(targetDatabaseName);

            Console.WriteLine("Data inserted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void CreateDatabase(string masterConnectionString, string databaseName)
    {
        using (SqlConnection connection = new SqlConnection(masterConnectionString))
        {
            connection.Open();

            //SQL commando som kun er 
            string checkDatabaseSql = $"SELECT database_id FROM sys.databases WHERE name = '{databaseName}'";

            using (SqlCommand command = new SqlCommand(checkDatabaseSql, connection))
            {
                object result = command.ExecuteScalar();

                if (result == null)
                {
                    // Create the database if it does not exist
                    string createDatabaseSql = $"CREATE DATABASE {databaseName}";
                    using (SqlCommand createCommand = new SqlCommand(createDatabaseSql, connection))
                    {
                        createCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    public void InsertData(string databaseName, string firstName, string lastName)
    {
        string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={databaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            CreateTable(connection);

            string sql = "INSERT INTO Persons (FirstName, LastName) VALUES (@FirstName, @LastName)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                command.ExecuteNonQuery();
            }
        }
    }

    public void CreateTable(SqlConnection connection)
    {
        string createTableSql = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Persons')
            BEGIN
                CREATE TABLE Persons
                (
                    PersonID INT PRIMARY KEY IDENTITY(1,1),
                    FirstName NVARCHAR(50) NOT NULL,
                    LastName NVARCHAR(50) NOT NULL
                )
            END";

        using (SqlCommand command = new SqlCommand(createTableSql, connection))
        {
            command.ExecuteNonQuery();
        }
    }
    public void ReadData(string databaseName)
    {
        string connectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={databaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Persons";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"PersonID: {reader["PersonID"]}, FirstName: {reader["FirstName"]}, LastName: {reader["LastName"]}");
                    }
                }
            }
        }
    }

}

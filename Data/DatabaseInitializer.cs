using Microsoft.Data.SqlClient;

namespace RecruitmentApp.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            const string query = @"
                CREATE TABLE IF NOT EXISTS Applicants (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100),
                    Email NVARCHAR(100),
                    Phone NVARCHAR(20),
                    Address NVARCHAR(255),
                    PositionApplied NVARCHAR(100),
                    CreatedAt DATETIME DEFAULT GETDATE()
                )";
            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly string _connectionString;

        public ApplicantRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Applicant>> GetApplicantsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM Applicants";
            return await connection.QueryAsync<Applicant>(query);
        }

        public async Task<Applicant?> GetApplicantByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM Applicants WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Applicant>(query, new { Id = id });
        }

        public async Task AddApplicantAsync(Applicant applicant)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"
                INSERT INTO Applicants (Name, Email, Phone, Address, PositionApplied)
                VALUES (@Name, @Email, @Phone, @Address, @PositionApplied)";
            await connection.ExecuteAsync(query, applicant);
        }

        public async Task UpdateApplicantAsync(Applicant applicant)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"
                UPDATE Applicants
                SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address, PositionApplied = @PositionApplied
                WHERE Id = @Id";
            await connection.ExecuteAsync(query, applicant);
        }

        public async Task DeleteApplicantAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM Applicants WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT COUNT(1) FROM Applicants WHERE Email = @Email";
            var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email });
            return count > 0;
        }

    }
}

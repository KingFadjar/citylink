using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public interface IApplicantRepository
    {
        Task<IEnumerable<Applicant>> GetApplicantsAsync();
        Task<Applicant?> GetApplicantByIdAsync(int id);
        Task AddApplicantAsync(Applicant applicant);
        Task UpdateApplicantAsync(Applicant applicant);
        Task DeleteApplicantAsync(int id);
        Task<bool> IsEmailExistsAsync(string email);

    }
}

using Microsoft.AspNetCore.Mvc;
using RecruitmentApp.Models;
using System.Threading.Tasks;

namespace RecruitmentApp.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IApplicantRepository _repository;

        public ApplicantsController(IApplicantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var applicants = await _repository.GetApplicantsAsync();
            return View(applicants);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                // Validasi email sudah digunakan
                if (await _repository.IsEmailExistsAsync(applicant.Email))
                {
                    ModelState.AddModelError("Email", "Email sudah digunakan. Silakan gunakan email lain.");
                    return View(applicant);
                }

                await _repository.AddApplicantAsync(applicant);
                TempData["SuccessMessage"] = "User berhasil ditambahkan!";
                TempData["UserName"] = applicant.Name;
                TempData["UserEmail"] = applicant.Email;

                return RedirectToAction(nameof(Success));
            }
            return View(applicant);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

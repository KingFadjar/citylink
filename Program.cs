using RecruitmentApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Ambil connection string dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not defined in appsettings.json.");
}

// Daftarkan IApplicantRepository dengan implementasi ApplicantRepository
builder.Services.AddSingleton<IApplicantRepository>(provider =>
    new ApplicantRepository(connectionString));

// Tambahkan MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Rute default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Applicants}/{action=Index}/{id?}");

app.Run();

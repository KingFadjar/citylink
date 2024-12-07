using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecruitmentApp.Models;

namespace RecruitmentApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Konfigurasi layanan (Dependency Injection, Database, dll.)
        public void ConfigureServices(IServiceCollection services)
        {
            // Menambahkan layanan MVC untuk controller dan view
            services.AddControllersWithViews();

            // Menambahkan layanan IApplicantRepository dengan implementasi ApplicantRepository
            string connectionString = Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' tidak didefinisikan di appsettings.json.");

            services.AddSingleton<IApplicantRepository>(provider =>
                new ApplicantRepository(connectionString));
        }

        // Konfigurasi middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Menampilkan halaman error saat pengembangan
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Menangani error pada lingkungan produksi
                app.UseHsts(); // Menambahkan HTTP Strict Transport Security
            }

            app.UseHttpsRedirection(); // Mengarahkan HTTP ke HTTPS
            app.UseStaticFiles(); // Mengizinkan akses file statis (CSS, JS, dll.)

            app.UseRouting(); // Menangani routing aplikasi

            app.UseAuthorization(); // Mengaktifkan middleware otorisasi

            // Menambahkan rute default untuk aplikasi
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Applicants}/{action=Index}/{id?}");
            });
        }
    }
}

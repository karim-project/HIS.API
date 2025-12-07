
using HIS.API.Configration;
using HIS.API.Data;
using HIS.API.Models;
using HIS.API.Services;
using HIS.API.Utitlies.DBInitilizer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace HIS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var connectionString =
       builder.Configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string"
           + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDBcontext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = false;
                option.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<ApplicationDBcontext>()
          .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login"; // Default login path
                options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Default access denied path
            });



            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddScoped<IDBInitilizer, DBInitilizer>();

            builder.Services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
            builder.Services.AddScoped<IRepository<Appointment>, Repository<Appointment>>();
            builder.Services.AddScoped<IRepository<Attachment>, Repository<Attachment>>();
            builder.Services.AddScoped<IRepository<Bed>, Repository<Bed>>();
            builder.Services.AddScoped<IRepository<Department>, Repository<Department>>();
            builder.Services.AddScoped<IRepository<DoctorSpecialty>, Repository<DoctorSpecialty>>();
            builder.Services.AddScoped<IRepository<Insurance>, Repository<Insurance>>();
            builder.Services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();
            builder.Services.AddScoped<IRepository<InvoiceItem>, Repository<InvoiceItem>>();
            builder.Services.AddScoped<IRepository<LabOrder>, Repository<LabOrder>>();
            builder.Services.AddScoped<IRepository<LabOrderItem>, Repository<LabOrderItem>>();
            builder.Services.AddScoped<IRepository<LabResult>, Repository<LabResult>>();
            builder.Services.AddScoped<IRepository<Medication>, Repository<Medication>>();
            builder.Services.AddScoped<IRepository<Patient>, Repository<Patient>>();
            builder.Services.AddScoped<IRepository<PharmacyStock>, Repository<PharmacyStock>>();
            builder.Services.AddScoped<IRepository<Prescription>, Repository<Prescription>>();
            builder.Services.AddScoped<IRepository<PrescriptionItem>, Repository<PrescriptionItem>>();
            builder.Services.AddScoped<IRepository<Room>, Repository<Room>>();
            builder.Services.AddScoped<IRepository<Specialty>, Repository<Specialty>>();
            builder.Services.AddScoped<IRepository<Visit>, Repository<Visit>>();
            builder.Services.AddScoped<IRepository<VisitNote>, Repository<VisitNote>>();
            builder.Services.AddScoped<IRepository<ApplicationUserOTP>, Repository<ApplicationUserOTP>>();
            builder.Services.AddTransient<ITokenService, Services.TokenService>();


            builder.Services.RegisterMapesterConfg();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(confi =>
            {
                confi.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7130",
                    ValidAudience = "https://localhost:7130",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2BBFD56151D59D1A5713B18BCDE5F2BBFD56151D59D1A5713B18BCDE5F"))
                };
            });

            var app = builder.Build();


            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IDBInitilizer>();
            service!.Initialize();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

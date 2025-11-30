using HIS.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HIS.API.Utitlies.DBInitilizer
{
    public class DBInitilizer : IDBInitilizer
    {
        private readonly ApplicationDBcontext _context;
        private readonly ILogger<DBInitilizer> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DBInitilizer(ApplicationDBcontext context, ILogger<DBInitilizer> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                    _context.Database.Migrate();

                if (_roleManager.Roles is null)
                {
                    _roleManager.CreateAsync(new(SD.Super_Admin_Role)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.Admin_Role)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.Employee_Role)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.Customer_Role)).GetAwaiter().GetResult();

                    _userManager.CreateAsync(new()
                    {
                        Email = "superadmin@kareem.com",
                        UserName = "SuperAdmin",
                        EmailConfirmed = true,
                        FirstName = "Super",
                        LastName = "Admin"
                    }, "Kareem@3165").GetAwaiter().GetResult();

                    var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(user!, SD.Super_Admin_Role).GetAwaiter().GetResult();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.Message}");
            }
        }
    }
}

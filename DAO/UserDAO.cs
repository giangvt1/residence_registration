// Project.DAO/UserDAO.cs
using Microsoft.EntityFrameworkCore;
using Project.Enums;
using Project.Models;
namespace Project.DAO
{
    public class UserDAO
    {
        private readonly PrnContext _context;

        public UserDAO(PrnContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUser(string email, string password, Role selectedRole)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                if (user.Role == selectedRole)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task AddUser(User newUser, string password)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        // --- Add this method to create the admin account ---
        public async Task CreateAdminAccount()
        {
            // Check if the admin user already exists
            if (!await _context.Users.AnyAsync(u => u.Email == "admin@example.com"))
            {
                var adminUser = new User
                {
                    FullName = "Admin User",
                    Email = "admin@example.com",
                    Role = Role.Admin,
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"), // HASH THE PASSWORD!
                    CurrentAddressId = 1 // Replace with a valid AddressId
                };
                //Add default address (if not exist)
                if (!await _context.Addresses.AnyAsync(a => a.AddressId == 1))
                {
                    var address = new Models.Address
                    {
                        AddressId = 1,
                        Street = "Default Street",
                        City = "Default City",
                        Country = "Default Country",
                        State = "Default State",
                        ZipCode = "Default"
                    };
                    _context.Addresses.Add(address);
                    await _context.SaveChangesAsync(); // Save address
                }
                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }
    }
}
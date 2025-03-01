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
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                if (user == null)
                    return null;

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return null;

                if (user.Role != selectedRole)
                    return null;

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task AddUser(User newUser, string password)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAdminAccount()
        {
            try
            {
                var adminEmail = "admin@system.com";
                var existingAdmin = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == adminEmail.ToLower());

                if (existingAdmin == null)
                {
                    // First, ensure we have at least one address
                    var address = await _context.Addresses.FirstOrDefaultAsync();
                    if (address == null)
                    {
                        address = new Address
                        {
                            Street = "Default Street",
                            City = "Default City",
                            State = "Default State",
                            Country = "Default Country",
                            ZipCode = "00000"
                        };
                        _context.Addresses.Add(address);
                        await _context.SaveChangesAsync();
                    }

                    var admin = new User
                    {
                        Email = adminEmail,
                        Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        FullName = "System Admin",
                        Role = Role.Admin,
                        CurrentAddressId = address.AddressId
                    };

                    _context.Users.Add(admin);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                // Log error or handle it appropriately
            }
        }
    }
}

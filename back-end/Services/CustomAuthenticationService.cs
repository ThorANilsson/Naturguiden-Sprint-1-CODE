using NaturguidenServerPrototype.Models;
using NaturguidenServerPrototype.Data;
using Microsoft.EntityFrameworkCore;

namespace NaturguidenServerPrototype.Services;

public class CustomAuthenticationService {
    private readonly ApplicationDbContext _context;

    public CustomAuthenticationService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string email, string password) {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPassword(password, user.PasswordHash)) {
            return null;
        }
        return user;
    }

    public Boolean VerifyPassword(string inputPassword, string passwordHash) {
        if (inputPassword == passwordHash) {
            return true;
        }
        return false;
    }

}
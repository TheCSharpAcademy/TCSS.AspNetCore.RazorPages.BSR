using Bogus;
using BSR.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BSR.Services;

public class UserService
{
    private readonly HomeContext _context;

    public UserService(HomeContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationUser>> GetUsers()
    {
        return await _context.AspNetUsers.ToListAsync();
    }
}

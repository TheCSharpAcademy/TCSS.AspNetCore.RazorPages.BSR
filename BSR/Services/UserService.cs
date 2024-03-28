using BSR.Models;
using Microsoft.EntityFrameworkCore;

namespace BSR.Services;

public class UserService
{
    private readonly HomeContext _context;

    public UserService(HomeContext context)
    {
        _context = context;
    }

    public async Task<UsersViewModel> GetUsers()
    {
        var users = await (from u in _context.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.Id
                           select new
                           UserViewModel
                           {
                               Id = u.Id,
                               Email = u.Email,
                               Role = r.Name
                           })
                   .ToListAsync();


        return new UsersViewModel
        {
            Users = users
        };
    }
}

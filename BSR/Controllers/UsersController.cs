using BSR.Models;
using BSR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BSR.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly ILogger<HomesController> _logger;
    private readonly UserService _userService;

    public UsersController(ILogger<HomesController> logger, UserService userService)
    {
        _logger = logger; 
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var usersViewModel = new UsersViewModel();
        usersViewModel.Users = await _userService.GetUsers();

        ViewBag.UsersCount = usersViewModel.Users.Count;

        return View(usersViewModel);
    }
}

using FixTrack.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FixTrack.Web.Controllers;

[Authorize(Roles = "Resident")]
public sealed class ResidentController : Controller
{
    private readonly UserManager<ApplicationUser> _users;

    public ResidentController(UserManager<ApplicationUser> users)
    {
        _users = users;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _users.GetUserAsync(User);
        if (user is null)
            return Challenge();

        ViewData["ResidentName"] = user.FullName;
        return View();
    }
}

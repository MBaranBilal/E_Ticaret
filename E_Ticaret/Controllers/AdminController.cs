using Microsoft.AspNetCore.Mvc;
using E_Ticaret.Models;

public class AdminController : Controller
{
    private readonly ETicaretDbContext _context;

    public AdminController(ETicaretDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users); // Users.cshtml'ye gönder
    }
}

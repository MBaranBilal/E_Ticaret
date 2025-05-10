using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if(role != "Admin")
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}

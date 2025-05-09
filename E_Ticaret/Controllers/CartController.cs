using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}

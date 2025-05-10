using Microsoft.AspNetCore.Mvc;
using E_Ticaret.ViewModels;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
	public class UserController : Controller
	{
		//Dependency ınjection yapıyoruz
		private readonly ETicaretDbContext context;
		public UserController (ETicaretDbContext context)
		{
			this.context = context;
			//this bu sınıf içerisinde tanımladığımız context i işaret eder, diğeri zaten parametreden gelen.
		}

		[HttpGet] // yanlış bilmiyorsam default olarak httpget etiketiyle geliyor, ancak yazmak daha açıklayıcı
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(UserLoginViewModel model) //login önyüzündeki form gönderildiğinde post metodu çalışmalı, buradaki model parametremiz formdan gelen email ve şifre bilgilerini taşıyacak.
		{
			if(!ModelState.IsValid)//burada modelin taşıdığı bilgilerin, koydğumuz şartları sağlayıp sağlamadığı kontrolünü yapıyoruz, [required] vs. gibi etiketleri hatırla
			{
				return View(model);
			}

			var user = context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
			//burada context.users ile aslında user tablomuzu temsil ediyoruz.
			//firstordefault fonksiyonu ilk eşleşen kaydı bulur yoksa null dönecektir.
			// u burada user tablomuzdaki satırları temsil eder diyebilirim, u => öyleki şu şartları sağlayan satır gibi
			//burada salt sorgudan ziyade LINQ yani language integreted query özelliğini kullanıyoruz ki çok kolaylaştırıyor işimizi

			if(user != null)
			{
				//burada kullanıcıyı bulduysak session başlatacağız ve tarayıcı açık olduğu sürece sunucuda verileri geçici olarak tutacak
				HttpContext.Session.SetInt32("UserId",user.UserId);
				HttpContext.Session.SetString("UserName",user.Name);
				HttpContext.Session.SetString("UserRole", user.Role == true ? "Admin" : "User");

				if(user.Role == true && model.IsAdminLogin)
				{
                    return RedirectToAction("AdminPage", "Admin");
                }
				else if (user.Role != true && model.IsAdminLogin)
				{
					ViewBag.Error = "Bu hesaba admin yetkisi tanımlı değil.";
					return View(model);
				}
				else
				{
                    return RedirectToAction("Index", "HomePage");
                }
			}

			ViewBag.Error = "Email veya şifre hatalı.";
			return View(model);
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantex.LoadingControl.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}





		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string username, string password)
		{
			var success = Membership.ValidateUser(username, password);
			if (success)
			{
				FormsAuthentication.SetAuthCookie(username, false);
				FormsAuthentication.RedirectFromLoginPage(username, false);
				return new EmptyResult();
			}

			//Failed, so show a try-again box
			return View();
		}

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			FormsAuthentication.RedirectToLoginPage();
			return new EmptyResult();
		}
	}
}
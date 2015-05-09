using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mantex.LoadingControl.Controllers
{
    public class AccountController : Controller
    {
		public Castle.Core.Logging.ILogger Logger { get; set; }

		[HttpGet]
		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOn(string username, string password)
		{
			var success = Membership.ValidateUser(username, password);
			if (success)
			{
				FormsAuthentication.SetAuthCookie(username, false);
				FormsAuthentication.RedirectFromLoginPage(username, false);
				return new EmptyResult();
			}
			else
			{
				Logger.WarnFormat("User {0} attempted login but password validation failed", username);
				ModelState.AddModelError("", "The user name or password provided is incorrect.");
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
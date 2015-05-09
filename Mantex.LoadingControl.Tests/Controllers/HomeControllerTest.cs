using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mantex.LoadingControl;
using Mantex.LoadingControl.Controllers;

namespace Mantex.LoadingControl.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Home_Index()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Home_About()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.About() as ViewResult;

			// Assert
			Assert.AreEqual("Your application description page.", result.ViewBag.Message);
		}

		[TestMethod]
		public void Home_Contact()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Contact() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}

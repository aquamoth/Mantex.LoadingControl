using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mantex.LoadingControl;
using Mantex.LoadingControl.ApiControllers;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Mantex.LoadingControl.ApiModels;

namespace Mantex.LoadingControl.Tests.ApiControllers
{
	//See: http://www.asp.net/web-api/overview/testing-and-debugging/unit-testing-controllers-in-web-api

	[TestClass]
	public class Api1ControllerTests
	{
		readonly Api1Controller _controller;

		public Api1ControllerTests()
		{
			_controller = new Api1Controller();
			_controller.Request = new HttpRequestMessage();
			_controller.Configuration = new HttpConfiguration();
			//_controller.Configuration.Routes.MapHttpRoute(
			//	name: "Default",
			//	routeTemplate: "api1/{action}/{id}",
			//	defaults: new { controller = "Api1", action = "IsAlive", id = UrlParameter.Optional }
			//);
		}

		[TestMethod]
		public void IsAlive()
		{
			bool response = _controller.IsAlive();
			Assert.IsTrue(response);
		}

		[TestMethod]
		public void Transactions()
		{
			//// Arrange
			//_controller.Request.RequestUri = new Uri("http://localhost/api1/transactions");

			//_controller.RequestContext.RouteData = new HttpRouteData(
			//		route: new HttpRoute(),
			//		values: new HttpRouteValueDictionary { { "action", "transactions" } 
			//	});

			// Act
			var transaction = new Api1Models.TransactionOperation() { Id = "42", Name = "Trans1", IsDeleted = false };
			/*var response = */_controller.Transactions(new[] { transaction });

			//// Assert
			//Assert.AreEqual("http://localhost/api/products/42", response.Headers.Location.AbsoluteUri);
			Assert.Inconclusive();
		}

		[TestMethod]
		public void CountAnalysisResults()
		{
			int count;
			count = _controller.CountAnalysisResults(null);
			Assert.Inconclusive();
		}

		[TestMethod]
		public void AnalysisResults()
		{
			Api1Models.AnalysisResults results = _controller.AnalysisResults(null, 1);
			Assert.Inconclusive();
		}

		[TestMethod]
		public void LabResults()
		{
			var labresult = new Api1Models.LabResultOperation() { Id = "4211", TransactionId = "42", Moisture = new Api1Models.AnalysisResultValue { Value = 46, StDev = 11 }, IsDeleted = false };
			_controller.LabResults(new[] { labresult });
			Assert.Inconclusive();
		}
	}
}

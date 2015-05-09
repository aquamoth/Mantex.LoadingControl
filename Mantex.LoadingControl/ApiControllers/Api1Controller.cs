using Mantex.LoadingControl.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mantex.LoadingControl.ApiControllers
{
    public class Api1Controller : ApiController
    {
		[HttpGet]
		public bool IsAlive()
		{
			return true;
		}

		//[HttpPost]
		//public void Transactions(Api1Models.TransactionOperation[] model)
		//{
		//	//Test by POSTing:
		//	//	[{ Id: 'Trans1', IsDeleted: 'False', Supplier: 'Sunnanö', ArrivalDate: '2014-04-14T13:42:11.000+2:00' }, { Id: 'Trans2', IsDeleted: 'True' }]
			
		//	throw new NotImplementedException();
		//	/*
		//	{
		//		"Message": "An error has occurred.",
		//		"ExceptionMessage": "Metoden eller åtgärden har inte implementerats.",
		//		"ExceptionType": "System.NotImplementedException",
		//		"StackTrace": "   vid Mantex.LoadingControl.ApiControllers.Api1Controller.Transactions(TransactionOperation[] model) i c:\\Users\\maas\\Workspace\\Git\\Mantex.LoadingControl\\Mantex.LoadingControl\\ApiControllers\\Api1Controller.cs:rad 38\r\n   vid lambda_method(Closure , Object , Object[] )\r\n   vid System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClassc.<GetExecutor>b__6(Object instance, Object[] methodParameters)\r\n   vid System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)\r\n   vid System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)\r\n--- Slut på stackspårningen från föregående plats där ett undantag utlöstes ---\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()\r\n   vid System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()\r\n--- Slut på stackspårningen från föregående plats där ett undantag utlöstes ---\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()\r\n   vid System.Web.Http.Controllers.ActionFilterResult.<ExecuteAsync>d__2.MoveNext()\r\n--- Slut på stackspårningen från föregående plats där ett undantag utlöstes ---\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   vid System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()\r\n   vid System.Web.Http.Dispatcher.HttpControllerDispatcher.<SendAsync>d__1.MoveNext()"
		//	}
		//	*/
		//}

		//[HttpGet]
		//public int CountAnalysisResults(string sinceTimestampToken)
		//{
		//	throw new NotImplementedException();
		//}

		//[HttpGet]
		//public Api1Models.AnalysisResults AnalysisResults(string sinceTimestampToken, int MaxResults = 50)
		//{
		//	throw new NotImplementedException();
		//}

		//[HttpPost]
		//public void LabResults(Api1Models.LabResultOperation[] model)
		//{
		//	throw new NotImplementedException();
		//}

    }
}

using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using Mantex.LoadingControl.Controllers;
using Mantex.LoadingControl.Installers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Mantex.LoadingControl.Tests.Installers
{
	[TestClass]
	public class ControllersInstallerTests
	{
		private IWindsorContainer containerWithControllers;
 
		public ControllersInstallerTests()
		{
			containerWithControllers = new WindsorContainer()
						.Install(new ControllersInstaller());
		}

		[TestMethod]
		public void All_controllers_implement_IController()
		{
			var allHandlers = GetAllHandlers(containerWithControllers);
			var controllerHandlers = GetHandlersFor(typeof(IController), containerWithControllers);

			Assert.IsTrue(allHandlers.Length > 0);
			CollectionAssert.AreEquivalent(allHandlers, controllerHandlers);
		}

		[TestMethod]
		public void All_controllers_are_registered()
		{
			// Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
			// which behaves like 'is' keyword in C# but at a Type, not instance level
			var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
			var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
			CollectionAssert.AreEquivalent(allControllers, registeredControllers);
		}

		[TestMethod]
		public void All_and_only_controllers_have_Controllers_suffix()
		{
			var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller") && !c.Name.StartsWith("Api"));
			var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
			CollectionAssert.AreEquivalent(allControllers, registeredControllers);
		}

		[TestMethod]
		public void All_and_only_controllers_live_in_Controllers_namespace()
		{
			var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace != null && c.Namespace.Contains("Controllers") && !c.Name.StartsWith("Api"));
			var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
			CollectionAssert.AreEquivalent(allControllers, registeredControllers);
		}

		[TestMethod]
		public void All_controllers_are_transient()
		{
			var nonTransientControllers = GetHandlersFor(typeof(IController), containerWithControllers)
				.Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
				.ToArray();

			Assert.IsTrue(nonTransientControllers.Length == 0);
		}

		[TestMethod]
		public void All_controllers_expose_themselves_as_service()
		{
			var controllersWithWrongName = GetHandlersFor(typeof(IController), containerWithControllers)
				.Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
				.ToArray();

			Assert.IsTrue(controllersWithWrongName.Length == 0);
		}





		private IHandler[] GetAllHandlers(IWindsorContainer container)
		{
			return GetHandlersFor(typeof(object), container);
		}

		private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
		{
			return container.Kernel.GetAssignableHandlers(type);
		}

		private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
		{
			return GetHandlersFor(type, container)
				.Select(h => h.ComponentModel.Implementation)
				.OrderBy(t => t.Name)
				.ToArray();
		}

		private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
		{
			var assembly = typeof(HomeController).Assembly;
			var exportedTypes = assembly.GetExportedTypes();
			return exportedTypes
				.Where(t => t.IsClass)
				.Where(t => t.IsAbstract == false)
				.Where(where.Invoke)
				.OrderBy(t => t.Name)
				.ToArray();
		}
	}
}

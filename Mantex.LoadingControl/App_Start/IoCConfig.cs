using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;
 
namespace Mantex.LoadingControl
{
	public class IoCConfig
	{
		private static IWindsorContainer container;
		internal static void BootstrapContainer()
		{
			container = new WindsorContainer()
				.Install(FromAssembly.This());
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		internal static void Shutdown()
		{
			container.Dispose();
		}
	}


	public class WindsorControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel kernel;

		public WindsorControllerFactory(IKernel kernel)
		{
			this.kernel = kernel;
		}

		public override void ReleaseController(IController controller)
		{
			kernel.ReleaseComponent(controller);
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
			}
			return (IController)kernel.Resolve(controllerType);
		}

	}
}
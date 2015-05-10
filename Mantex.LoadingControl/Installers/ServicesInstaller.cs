using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Mantex.LoadingControl.Installers
{
	public class ServicesInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromAssemblyContaining<Mantex.ERP.Services.TransactionLogic>()
								.InNamespace("Mantex.ERP.Services")
								//.BasedOn<IController>()
								.WithServiceDefaultInterfaces()
								.LifestylePerWebRequest());
		}
	}
}
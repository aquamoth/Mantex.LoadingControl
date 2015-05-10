using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Mantex.LoadingControl.Installers
{
	public class RepositoryInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//container.Register(Classes.FromAssemblyContaining<Mantex.ERP.Data.EF.EFRepository>()
			//					.BasedOn<Mantex.ERP.Data.IRepository>()
			//					.WithServiceBase()
			//					.LifestyleSingleton());
			container.Register(Classes.FromAssemblyContaining<Mantex.ERP.Data.Fake.FakeRepository>()
								.BasedOn<Mantex.ERP.Data.IRepository>()
								.WithServiceBase()
								.LifestyleSingleton());
		}
	}
}
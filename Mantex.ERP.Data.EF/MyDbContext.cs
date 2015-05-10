using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data.EF
{
	public partial class MyDbContext : System.Data.Entity.DbContext
	{

	}

    public partial class MyDbContext : IDbContext
    {
		public System.Data.Entity.Database Database
		{
			get { return base.Database; }
		}

		public System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity)
		{
			return base.Entry(entity);
		}

		public System.Data.Entity.IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		//public new System.Data.Entity.IDbSet<TEntity> Set<TEntity>() where TEntity : class
		//{
		//	return base.Set<TEntity>();
		//}

		public int SaveChanges()
		{
			return base.SaveChanges();
		}

		public void Dispose()
		{
			base.Dispose();
		}
	}
}

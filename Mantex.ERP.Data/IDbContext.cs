using Mantex.ERP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data
{
	public interface IRepository
	{
		ISet<Batch> Batches { get; }
		ISet<MaterialType> MaterialTypes { get; }
		ISet<Transaction> Transactions { get; }
		void SaveChanges();
	}

	public interface IDbContext : IDisposable
    {
		Database Database { get; }
		DbEntityEntry Entry(object entity);
		IDbSet<TEntity> Set<TEntity>() where TEntity : class;
		int SaveChanges();
	}
}

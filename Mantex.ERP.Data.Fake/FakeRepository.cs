using Mantex.ERP.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data.Fake
{
    public class FakeRepository : IRepository
    {
		public FakeRepository()
		{
			this.MaterialTypes = new TestDbSet<MaterialType>(createMaterialTypes());
			this.Transactions = new TestDbSet<Transaction>(createTransactions());
		}

		public IDbSet<MaterialType> MaterialTypes { get; private set; }
		public IDbSet<Transaction> Transactions { get; private set; }
		public IDbSet<Batch> Batches 
		{ 
			get
			{
				return new TestDbSet<Batch>(this.Transactions.SelectMany(t => t.Batches));
			} 
		}

		public void SaveChanges()
		{
		}


		IEnumerable<Entities.MaterialType> createMaterialTypes()
		{
			yield return new Entities.MaterialType { Id = 1, Name = "Gran" };
			yield return new Entities.MaterialType { Id = 2, Name = "Grot" };
			yield return new Entities.MaterialType { Id = 3, Name = "Returflis" };
			yield return new Entities.MaterialType { Id = 4, Name = "Returpapper" };
		}

		IEnumerable<Entities.Transaction> createTransactions()
		{
			yield return new Entities.Transaction
			{
				Id = "SF-87104",
				Name = "Everst #27",
				Description = "Starta försiktigt ifall det finns sten i.",
				MaterialTypeId = 1,
				MaterialType = this.MaterialTypes.Single(mt => mt.Id == 1),
				ShippingDate = DateTime.Today,
				ExpectedWeight = 23503,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Entities.Transaction
			{
				Id = "SF-87105",
				Name = "Alfred",
				Description = null,
				MaterialTypeId = 1,
				MaterialType = this.MaterialTypes.Single(mt => mt.Id == 1),
				ShippingDate = DateTime.Today.AddDays(1),
				ExpectedWeight = 12422,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Entities.Transaction
			{
				Id = "SF-87106",
				Name = "Lollipop",
				Description = null,
				MaterialTypeId = 1,
				MaterialType = this.MaterialTypes.Single(mt => mt.Id == 1),
				ShippingDate = DateTime.Today.AddDays(2),
				ExpectedWeight = 15201,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Entities.Transaction
			{
				Id = "SF-87107",
				Name = "Mandarin",
				Description = null,
				MaterialTypeId = 2,
				MaterialType = this.MaterialTypes.Single(mt => mt.Id == 2),
				ShippingDate = DateTime.Today.AddDays(2),
				ExpectedWeight = 17221,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Entities.Transaction
			{
				Id = "SF-87108",
				Name = "Russian Velvet",
				Description = null,
				MaterialTypeId = 1,
				MaterialType = this.MaterialTypes.Single(mt => mt.Id == 1),
				ShippingDate = DateTime.Today.AddDays(3),
				ExpectedWeight = 8282,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};

		}
	}
}

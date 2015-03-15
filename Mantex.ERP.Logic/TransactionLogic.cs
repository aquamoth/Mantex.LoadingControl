using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Logic
{
    public class TransactionLogic
    {
		public IEnumerable<Data.Transaction> GetCurrentTransactions()
		{
			yield return new Data.Transaction
			{
				Id = "SF-87104",
				Name = "Everst #27",
				Description = "Starta försiktigt ifall det finns sten i.",
				MaterialTypeId = 1,
				ShippingDate = DateTime.Today,
				ExpectedWeight = 23503,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Data.Transaction
			{
				Id = "SF-87105",
				Name = "Alfred",
				Description = null,
				MaterialTypeId = 1,
				ShippingDate = DateTime.Today.AddDays(1),
				ExpectedWeight = 12422,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Data.Transaction
			{
				Id = "SF-87106",
				Name = "Lollipop",
				Description = null,
				MaterialTypeId = 1,
				ShippingDate = DateTime.Today.AddDays(2),
				ExpectedWeight = 15201,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Data.Transaction
			{
				Id = "SF-87107",
				Name = "Mandarin",
				Description = null,
				MaterialTypeId = 2,
				ShippingDate = DateTime.Today.AddDays(2),
				ExpectedWeight = 17221,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
			yield return new Data.Transaction
			{
				Id = "SF-87108",
				Name = "Russian Velvet",
				Description = null,
				MaterialTypeId = 1,
				ShippingDate = DateTime.Today.AddDays(3),
				ExpectedWeight = 8282,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
		}

		public IEnumerable<Data.MaterialType> AvailableMaterialTypes()
		{
			yield return new Data.MaterialType { Id = 1, Name = "Gran" };
			yield return new Data.MaterialType { Id = 2, Name = "Grot" };
			yield return new Data.MaterialType { Id = 3, Name = "Returflis" };
			yield return new Data.MaterialType { Id = 4, Name = "Returpapper" };
		}

		public Data.Transaction GetActiveTransaction()
		{
			return new Data.Transaction
			{
				Id = "SF-87106",
				Name = "Lollipop",
				Description = null,
				MaterialTypeId = 1,
				ShippingDate = DateTime.Today.AddDays(2),
				ExpectedWeight = 15201,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};
		}
	}
}

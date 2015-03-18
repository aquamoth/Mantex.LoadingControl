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
			return _currentTransactions;
		}

		public IEnumerable<Data.MaterialType> AvailableMaterialTypes()
		{
			return _materialTypes;
		}

		public Data.Transaction GetActiveTransaction()
		{
			return _activeTransaction;
		}

		public void StartTransaction(string transactionId, int materialTypeId)
		{
#warning Not thread safe!
			if (_activeTransaction != null)
				throw new NotSupportedException(string.Format("Transaktion '{0}' måste stoppas först.", _activeTransaction.Id));

			var transaction = getTransaction(transactionId);
			var materialType = getMaterialType(materialTypeId);
			addNewBatch(transaction, materialType);

			_activeTransaction = transaction;
		}

		public void StopBatch(int Id)
		{
#warning Not thread safe!
			var batch = getBatch(Id);
			if (batch == null)
				throw new ArgumentException(string.Format("Batch '{0}' finns inte.", Id));

			if (batch.EndTime.HasValue)
				throw new NotSupportedException(string.Format("Batch '{0}' har redan stoppats.", Id));

			batch.EndTime = DateTime.Now;
			if (batch.Transaction == _activeTransaction)
				_activeTransaction = null;
		}

		public void FinishTransaction(string Id)
		{
			var transaction = getTransaction(Id);
			
			var isFinished = transaction.Batches.Any(b => b.IsFinished);
			if (isFinished)
				throw new NotSupportedException("Transaktionen har redan avslutats.");
		
			var batch = transaction.Batches.SingleOrDefault(b => !b.EndTime.HasValue);
			if (batch == null)
				throw new NotSupportedException("Transaktionen måste startas innan den kan avslutas.");

			batch.EndTime = DateTime.Now;
			batch.IsFinished = true;
			if (batch.Transaction == _activeTransaction)
				_activeTransaction = null;
			_currentTransactions.Remove(transaction);
		}

		public void Create(Data.Transaction model)
		{
			var transactionId = "MX-" + model.Id;
			if (_currentTransactions.Any(t => t.Id == transactionId))
				throw new ArgumentException("Transaktionsnumret finns redan.");
			var materialType = getMaterialType(model.MaterialTypeId);

			var transaction = new Data.Transaction
			{
				Id = transactionId,
				Name = model.Name,
				Description = model.Description,
				MaterialTypeId = materialType.Id,
				MaterialType = materialType,
				ShippingDate = model.ShippingDate,
				ExpectedWeight = model.ExpectedWeight,
				ShippingMethod = model.ShippingMethod,
				Supplier = model.Supplier
			};
			_currentTransactions.Add(transaction);
		}





		private Data.Transaction getTransaction(string transactionId)
		{
			var transactions = this.GetCurrentTransactions();
			var transaction = transactions.SingleOrDefault(t => t.Id == transactionId);
			if (transaction == null)
				throw new ArgumentException(string.Format("Transaktion '{0}' hittades inte.", transactionId));
			return transaction;
		}

		private Data.MaterialType getMaterialType(int materialTypeId)
		{
			var materialTypes = this.AvailableMaterialTypes();
			var materialType = materialTypes.FirstOrDefault(mt => mt.Id == materialTypeId);
			if (materialType == null)
				throw new ArgumentException("Materialtypen finns inte.");
			return materialType;
		}

		private Data.Batch getBatch(int id)
		{
			var transactions = this.GetCurrentTransactions();
			var batches = transactions.SelectMany(t => t.Batches);
			var batch = batches.SingleOrDefault(b => b.Id == id);
			return batch;
		}

		private void addNewBatch(Data.Transaction transaction, Data.MaterialType materialType)
		{
			string transactionId = transaction.Id;
			int materialTypeId = materialType.Id;
			var batch = new Data.Batch
			{
				Id = new Random().Next(1, 100000),
				MaterialTypeId = materialTypeId,
				MaterialType = materialType,
				StartTime = DateTime.Now,
				EndTime = null,
				TransactionId = transactionId,
				Transaction = transaction
			};
			transaction.Batches.Add(batch);
		}

		#region FAKE DATA LAYER

		static readonly List<Data.MaterialType> _materialTypes;
		static readonly List<Data.Transaction> _currentTransactions;
		static Data.Transaction _activeTransaction = null;

		static TransactionLogic()
		{
			_materialTypes = createMaterialTypes().ToList();
			_currentTransactions = createTransactions().ToList();
		}

		static IEnumerable<Data.MaterialType> createMaterialTypes()
		{
			yield return new Data.MaterialType { Id = 1, Name = "Gran" };
			yield return new Data.MaterialType { Id = 2, Name = "Grot" };
			yield return new Data.MaterialType { Id = 3, Name = "Returflis" };
			yield return new Data.MaterialType { Id = 4, Name = "Returpapper" };
		}

		static IEnumerable<Data.Transaction> createTransactions()
		{
			yield return new Data.Transaction
			{
				Id = "SF-87104",
				Name = "Everst #27",
				Description = "Starta försiktigt ifall det finns sten i.",
				MaterialTypeId = 1,
				MaterialType = _materialTypes.Single(mt => mt.Id == 1),
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
				MaterialType = _materialTypes.Single(mt => mt.Id == 1),
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
				MaterialType = _materialTypes.Single(mt => mt.Id == 1),
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
				MaterialType = _materialTypes.Single(mt => mt.Id == 2),
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
				MaterialType = _materialTypes.Single(mt => mt.Id == 1),
				ShippingDate = DateTime.Today.AddDays(3),
				ExpectedWeight = 8282,
				ShippingMethod = "Båt",
				Supplier = "Sunnanö"
			};

		}

		#endregion FAKE DATA LAYER
	}
}

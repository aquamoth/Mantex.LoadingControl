using Mantex.ERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mantex.LoadingControl.Models
{
	public class LoadingModels
	{
		public class IndexModel
		{
			public IEnumerable<Transaction> Transactions { get; set; }
			public IEnumerable<MaterialType> MaterialTypes { get; set; }

			public string ActiveTransactionId { get; set; }
			public int SelectedMaterialType { get; set; }
		}

		public class ProductionStatusModel
		{
			public Transaction Transaction { get; set; }

			public int BatchIndex { get; set; }
			public DateTime StartTime { get; set; }
			public int ProductionTime_Transaction { get; set; }
			public int ProductionTime_Batch { get; set; }
		}

	}
}
using Mantex.ERP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mantex.LoadingControl.Models
{
	public class LoadingModels
	{
		public class IndexModel : IndexPostModel
		{
			public IEnumerable<Transaction> Transactions { get; set; }
			public IEnumerable<MaterialType> MaterialTypes { get; set; }
			public Batch ActiveBatch { get; set; }
		}

		public class IndexPostModel
		{
			[Required(ErrorMessage = "Välj en transaktion.")]
			public string SelectedTransaction { get; set; }
			public int SelectedMaterialType { get; set; }
		}
	}
}
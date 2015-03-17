using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data
{
	public class Batch
	{
		public int Id { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }

		public string TransactionId { get; set; }
		public Transaction Transaction { get; set; }

		public int MaterialTypeId { get; set; }
		public MaterialType MaterialType { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantex.ERP.Data
{
    public class Transaction
    {
		public Transaction()
		{
			Batches = new HashSet<Batch>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public DateTime ShippingDate { get; set; }
		public string Supplier { get; set; }
		public int ExpectedWeight { get; set; }
		public string ShippingMethod { get; set; }
		public string Description { get; set; }

		public int MaterialTypeId { get; set; }
		public MaterialType MaterialType { get; set; }

		public virtual ICollection<Batch> Batches { get; set; }
    }
}

namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Batch
    {
        public Batch()
        {
            Sessions = new HashSet<Session>();
			Observations = new HashSet<Observation>();
		}

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string TransactionId { get; set; }

        public int MaterialTypeId { get; set; }

        public int LoadingPositionId { get; set; }

        public int? MachineId { get; set; }

        public DateTime StartedAt { get; set; }

        [StringLength(256)]
        public string StartedBy { get; set; }

        public DateTime? StoppedAt { get; set; }

        [StringLength(256)]
        public string StoppedBy { get; set; }

        public bool IsTransactionDone { get; set; }

        public virtual LoadingPosition LoadingPosition { get; set; }

        public virtual Machine Machine { get; set; }

        public virtual MaterialType MaterialType { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
	
		public virtual ICollection<Observation> Observations { get; set; }
	}
}

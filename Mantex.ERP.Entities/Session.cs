namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Session
    {
        public Session()
        {
            Events = new HashSet<Event>();
            Measurements = new HashSet<Measurement>();
        }

        public int Id { get; set; }

        public int BatchId { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? StoppedAt { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}

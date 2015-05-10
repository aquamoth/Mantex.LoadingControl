namespace Mantex.ERP.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	//using System.Data.Entity.Spatial;

    public partial class Event
    {
        public Guid Id { get; set; }

        public int EventTypeId { get; set; }

        public int SessionId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int BeltSectionId { get; set; }

        public int BeltRevolution { get; set; }

        public virtual BeltSection BeltSection { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual Session Session { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Entities
{
    public abstract class TrackedEntityBase : EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        [ForeignKey("ModifiedBy")]
        public int ModifiedByUserId { get; set; }
        public virtual User ModifiedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedBy { get; set; }

        public TrackedEntityStatus TrackedEntityStatus { get;set;}
    }
}

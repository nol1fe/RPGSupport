using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Entities
{
    public class Character : TrackedEntityBase
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }
        public GameSystem GameSystem { get; set; }

        public virtual ICollection<CharacterStatistic> Statistics { get; set; }
    }
}

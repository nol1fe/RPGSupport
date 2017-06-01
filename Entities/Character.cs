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
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //public string Gender { get; set; }
        public Gender Gender { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual GameSystem GameSystem { get; set; }

        public virtual ICollection<CharacterStatistic> Statistics { get; set; }

        public Character() { }

        public Character(int userId)
        {
            UserId = userId;
        }

    }
}

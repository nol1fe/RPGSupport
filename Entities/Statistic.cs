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
    public class Statistic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        [Range(0,100)]
        public int DefaultValue { get; set; }

        public virtual GameSystem GameSystem { get; set; }

        //min value 
        //max value
        //description
        //game system

    }

}

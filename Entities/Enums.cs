using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Enums
    {
        public enum GameSystem
        {
            [Display(Name = "Warhammer")]
            Warhammer = 0,
            [Display(Name = "Dungeons & Dragons")]
            DungeonsAndDragons = 1,
        }

        public enum Gender
        {
            Male = 1,
            Female = 2

        }
    }
}

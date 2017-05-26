using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CharacterStatistic
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Statistic")]
        public int StatisticId { get; set; }

        public virtual Statistic Statistic { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }

        public virtual Character Character { get; set; }

        [Range(1, 100)]
        public int CurrentValue { get; set; }

        public CharacterStatistic()
        {

        }

        public CharacterStatistic(int characterId, int statisticId)
        {
            CharacterId = characterId;
            StatisticId = statisticId;

        }
    }
}

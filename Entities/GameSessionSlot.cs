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
    public class GameSessionSlot : EntityBase
    {
        public GameSessionSlotStatus SlotStatus { get; set; }

        [ForeignKey("GameSession")]
        public int GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        public GameSessionSlot() { }
        public GameSessionSlot(int gameSessionId, int characterId)
        {
            GameSessionId = gameSessionId;
            CharacterId = characterId;
        }

    }
}

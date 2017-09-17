using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Entities
{
    public class GameSession : TrackedEntityBase
    {
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaximumPlayers { get; set; }

        public virtual Game Game { get; set; }

        public GameSessionState GameSessionState { get; set; }

        public virtual ICollection<GameSessionSlot> GameSessionSlots { get; set; }
    }
}

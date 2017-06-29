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
        public string Name { get; set; }

        public GameSessionState GameSessionState { get; set; }

        public virtual ICollection<GameSessionSlot> GameSessionSlots { get; set; }
    }
}

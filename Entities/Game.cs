using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Entities
{
    public class Game : TrackedEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public GameSystem GameSystem { get; set; }

        public virtual ICollection<GameSession> GameSessions { get; set; }
    }
}

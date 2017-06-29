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
            Male = 0,
            Female 
        }

        public enum GameSessionSlotStatus
        {
            Open = 0,
            Closed,
            Ready
        }

        public enum GameSessionState
        {
            InLobby = 0,
            InProgress,
            Scheduled,
            Finished
        }

        public enum TrackedEntityStatus
        {
            New = 0,
            Private,
            Published,
            Deleted,
            IsActive
        }
    }
}

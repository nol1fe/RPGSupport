using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Entities.Enums;

namespace RPGSupport.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedByUserName { get; set; }

        public GameSystem GameSystem { get; set; }

        public ICollection<GameSession> GameSessions { get; set; }

        public GameViewModel()
        {

        }

        public GameViewModel(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            Description = game.Description;
            GameSystem = game.GameSystem;
        }
    }
}
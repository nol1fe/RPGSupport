using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Entities.Enums;

namespace RPGSupport.Models
{
    public class GameSessionViewModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedByUserName { get; set; }
        public string GameState { get; set; }
        public int MaximumPlayers { get; set; }


        public Game Game { get; set; }
        public GameSystem GameSystem { get; set; }
        public ICollection<GameSession> GameSessions { get; set; }

        public GameSessionState GameSessionState { get; set; }


        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(GameSession gameSession)
        {
            Id = gameSession.Id;
            GameId = gameSession.GameId;
            Name = gameSession.Name;
            Description = gameSession.Description;
            MaximumPlayers = gameSession.MaximumPlayers;

        }
    }
}
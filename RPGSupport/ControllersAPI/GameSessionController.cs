using Entities;
using Interfaces.Services;
using Microsoft.AspNet.Identity;
using RPGSupport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using static Entities.Enums;

namespace RPGSupport.ControllersAPI
{
    public class GameSessionController : ApiController
    {
        private IEntityService<GameSession> gameSessionEntityService;
        private IEntityService<Game> gameEntityService;
        private IEntityService<Character> characterEntityService;
        private IEntityService<Statistic> statisticEntityService;
        private IEntityService<CharacterStatistic> characterStatisticEntityService;
        private IEntityService<User> userEntityService;


        public GameSessionController(IEntityService<GameSession> gameSessionEntityService,
            IEntityService<Character> characterEntityService,
            IEntityService<Statistic> statisticEntityService,
            IEntityService<CharacterStatistic> characterStatisticEntityService,
            IEntityService<User> userEntityService,
            IEntityService<Game> gameEntityService)
        {
            this.gameSessionEntityService = gameSessionEntityService;
            this.characterEntityService = characterEntityService;
            this.statisticEntityService = statisticEntityService;
            this.characterStatisticEntityService = characterStatisticEntityService;
            this.userEntityService = userEntityService;
            this.gameEntityService = gameEntityService;
        }


        [HttpPost]
        [Route("api/GameSession/CreateNew")]
        public HttpResponseMessage CreateNewGameSession([FromBody] GameSession gameSession)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            var newGameSession = new GameSession()
            {
                Name = gameSession.Name,
                Description = gameSession.Description,
                GameId = gameSession.GameId,
                Game = gameEntityService.GetById(gameSession.GameId),
                GameSessionState = GameSessionState.InLobby,
                GameSessionSlots = new List<GameSessionSlot>(),
                MaximumPlayers = gameSession.MaximumPlayers
            };

            gameSessionEntityService.Add(newGameSession);

            return Request.CreateResponse(HttpStatusCode.OK, newGameSession);

        }

        [HttpGet]
        [Route("api/GameSession/GetGameSessionsForGame/{gameId}")]
        public HttpResponseMessage GetGameSessionsForGame([FromUri] int gameId)
        {
            var gameSessionsList = new List<GameSessionViewModel>();
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var gameSessions = gameSessionEntityService.GetAll().Where(x => x.TrackedEntityStatus != TrackedEntityStatus.Deleted && x.GameId == gameId).ToList();

            foreach (var gameSession in gameSessions)
            {
                var gameSessionView = new GameSessionViewModel(gameSession);
                gameSessionView.Game = gameEntityService.GetById(gameSession.GameId);
                gameSessionView.CreatedByUserName = userEntityService.GetById(gameSession.CreatedByUserId).UserName;
                gameSessionView.GameSystem = gameEntityService.GetById(gameSession.GameId).GameSystem;
                gameSessionView.GameState = gameSession.GameSessionState.GetType().GetMember(gameSession.GameSessionState.ToString())
                          .First()
                          .GetCustomAttribute<DisplayAttribute>()
                          .Name;
                gameSessionsList.Add(gameSessionView);
            }

            if (gameSessions != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, gameSessionsList);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Game Sessions do not exist");

        }

        [HttpGet]
        [Route("api/GameSession/{id}")]
        public HttpResponseMessage GetSingle([FromUri]int id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var gameSession = gameSessionEntityService.GetSingle(x => x.Id == id);

            if (gameSession != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, gameSession);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "GameSession does not exist");
        }
    }
}

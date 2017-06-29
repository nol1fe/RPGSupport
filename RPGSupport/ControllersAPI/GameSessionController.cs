using Entities;
using Interfaces.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static Entities.Enums;

namespace RPGSupport.ControllersAPI
{
    public class GameSessionController : ApiController
    {
        private IEntityService<GameSession> gameSessionEntityService;
        private IEntityService<Character> characterEntityService;
        private IEntityService<Statistic> statisticEntityService;
        private IEntityService<CharacterStatistic> characterStatisticEntityService;
        private IEntityService<User> userEntityService;


        public GameSessionController(IEntityService<GameSession> gameSessionEntityService,
            IEntityService<Character> characterEntityService,
            IEntityService<Statistic> statisticEntityService,
            IEntityService<CharacterStatistic> characterStatisticEntityService,
            IEntityService<User> userEntityService)
        {
            this.gameSessionEntityService = gameSessionEntityService;
            this.characterEntityService = characterEntityService;
            this.statisticEntityService = statisticEntityService;
            this.characterStatisticEntityService = characterStatisticEntityService;
            this.userEntityService = userEntityService;
        }


        [HttpPost]
        [Route("api/GameSession/CreateNew")]
        public HttpResponseMessage CreateNewGameSession([FromBody] GameSession gameSession)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            var newGameSession = new GameSession()
            {
                Name = gameSession.Name,
                GameSessionState = GameSessionState.InLobby,
                GameSessionSlots = new List<GameSessionSlot>(),
                //Characters = new List<Character>(),
            };

            gameSessionEntityService.Add(newGameSession);

            return Request.CreateResponse(HttpStatusCode.OK, newGameSession);

        }

        [HttpGet]
        [Route("api/GameSession/")]
        public HttpResponseMessage GetAllGameSessions()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var gameSessions = gameSessionEntityService.GetAll().Where(x => x.TrackedEntityStatus != TrackedEntityStatus.Deleted).ToList();

            if (gameSessions != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, gameSessions);
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

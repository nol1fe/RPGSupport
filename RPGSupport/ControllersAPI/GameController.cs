using Entities;
using Interfaces.Services;
using Microsoft.AspNet.Identity;
using RPGSupport.Models;
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
    public class GameController : ApiController
    {
        private IEntityService<Game> gameEntityService;
        private IEntityService<GameSession> gameSessionEntityService;
        private IEntityService<Character> characterEntityService;
        private IEntityService<Statistic> statisticEntityService;
        private IEntityService<CharacterStatistic> characterStatisticEntityService;
        private IEntityService<User> userEntityService;


        public GameController(IEntityService<Game> gameEntityService,
            IEntityService<GameSession> gameSessionEntityService,
            IEntityService<Character> characterEntityService,
            IEntityService<Statistic> statisticEntityService,
            IEntityService<CharacterStatistic> characterStatisticEntityService,
            IEntityService<User> userEntityService)
        {
            this.gameEntityService = gameEntityService;
            this.gameSessionEntityService = gameSessionEntityService;
            this.characterEntityService = characterEntityService;
            this.statisticEntityService = statisticEntityService;
            this.characterStatisticEntityService = characterStatisticEntityService;
            this.userEntityService = userEntityService;
        }


        [HttpPost]
        [Route("api/Game/CreateNew")]
        public HttpResponseMessage CreateNewGame([FromBody] Game game)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            var newGame = new Game()
            {
                Name = game.Name,
                Description = game.Description,
                GameSystem = game.GameSystem,
                GameSessions = new List<GameSession>(),
            };

            gameEntityService.Add(newGame);

            return Request.CreateResponse(HttpStatusCode.OK, newGame);

        }

        [HttpGet]
        [Route("api/Game/")]
        public HttpResponseMessage GetAllGames()
        {
            var gamesList = new List<GameViewModel>();
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var games = gameEntityService.GetAll().Where(x => x.TrackedEntityStatus != TrackedEntityStatus.Deleted).ToList();

            foreach (var game in games)
            {
                var gameView = new GameViewModel(game);
                gameView.CreatedByUserName = userEntityService.GetById(game.CreatedByUserId).UserName;
                gamesList.Add(gameView);
            }

            if (gamesList.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, gamesList);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Game do not exist");

        }

        [HttpGet]
        [Route("api/Game/{id}")]
        public HttpResponseMessage GetSingle([FromUri]int id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var game = gameEntityService.GetSingle(x => x.Id == id);

            if (game != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, game);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Game does not exist");
        }

    }
}

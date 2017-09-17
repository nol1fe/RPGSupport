using Entities;
using Interfaces.Services;
using RPGSupport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using static Entities.Enums;

namespace RPGSupport.ControllersAPI
{
    public class GameSystemController : ApiController
    {
        private IEntityService<Character> characterEntityService;
        private IEntityService<Statistic> statisticEntityService;

        public GameSystemController(IEntityService<Character> characterEntityService,
            IEntityService<Statistic> statisticEntityService
            )
        {
            this.characterEntityService = characterEntityService;
            this.statisticEntityService = statisticEntityService;
        }


        [HttpGet]
        [Route("api/GameSystem/Lookup")]
        public HttpResponseMessage GetGameSystemLookup()
        {
            var result = new List<LookupModel>();

            foreach (var gameSystem in Enum.GetValues(typeof(GameSystem)))
            {
                var name = gameSystem.GetType().GetMember(gameSystem.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;

                result.Add(new LookupModel()
                {
                    Key = (int)gameSystem,
                    Value = gameSystem.ToString(),
                    Name = name
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [HttpGet]
        [Route("api/GameSystem/{id}")]
        public HttpResponseMessage GetGameSystemStatistics([FromUri]int id)
        {

            var system = (GameSystem)id;

            var statistics = statisticEntityService.GetAll().Where(x => x.GameSystem == system).ToList();
            if (statistics != null && statistics.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, statistics);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "System not found");

        }
    }
}

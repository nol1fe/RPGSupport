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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static Entities.Enums;


namespace RPGSupport.ControllersAPI
{
    public class CharacterController : ApiController

    {
        private IEntityService<Character> characterEntityService;
        private IEntityService<Statistic> statisticEntityService;
        private IEntityService<CharacterStatistic> characterStatisticEntityService;


        public CharacterController(IEntityService<Character> characterEntityService, IEntityService<Statistic> statisticEntityService, IEntityService<CharacterStatistic> characterStatisticEntityService)
        {
            this.characterEntityService = characterEntityService;
            this.statisticEntityService = statisticEntityService;
            this.characterStatisticEntityService = characterStatisticEntityService;
        }

        [HttpPost]
        [Route("api/Character/CreateNew")]
        public HttpResponseMessage CreateNewCharacter([FromBody] Character character)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();

            var newCharacter = new Character(userId)
            {
                Name = character.Name,
                Gender = character.Gender,
                Statistics = new List<CharacterStatistic>()

            };

            foreach (var stat in character.Statistics)
            {
                var characterStat = new CharacterStatistic(character.Id, stat.Id)
                {
                    CurrentValue = stat.CurrentValue,

                };
                newCharacter.Statistics.Add(characterStat);
            }

            characterEntityService.Add(newCharacter);

            return Request.CreateResponse(HttpStatusCode.OK, newCharacter);

        }
        [HttpDelete]
        [Route("api/Character/{id}")]
        public HttpResponseMessage Delete([FromUri]int id)
        {

            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            int charId = id;

                var characterFromDb = characterEntityService.GetSingle(x => x.Id == charId);
                if (characterFromDb != null)
                {

                    characterEntityService.Delete(characterFromDb);

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Character not found");

        }

        [HttpGet]
        [Route("api/Character/")]
        public HttpResponseMessage GetAllCharacters()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var characters = characterEntityService.GetAll().Where(x => x.UserId == userId).ToList();

            foreach (var character in characters)
            {
                var characterStatistics = characterStatisticEntityService.GetAll().Where(x => x.CharacterId == character.Id).ToList();

                foreach (var stat in characterStatistics)
                {
                    var statistic = statisticEntityService.GetSingle(x => x.Id == stat.StatisticId);
                    stat.Statistic = statistic;
                }

                character.Statistics = characterStatistics;

            }


            return Request.CreateResponse(HttpStatusCode.OK, characters);

        }

        [HttpGet]
        [Route("api/Character/{id}")]
        public HttpResponseMessage GetSingle([FromUri]int id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var character = characterEntityService.GetSingle(x => x.Id == id,
                y=>y.Statistics, 
                y=>y.Statistics.Select(z=>z.Statistic));

            if (character != null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, character);

            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Character not found");

        }

        [HttpPut]
        [Route("api/Character/{id}")]
        public async Task<HttpResponseMessage> Put([FromBody]Character character)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var characterFromDb = characterEntityService.GetSingle(x => x.Id == character.Id);
                    characterFromDb.Name = character.Name;
                    characterFromDb.Gender = character.Gender;
                    await characterEntityService.UpdateAsync(characterFromDb);

                    var characterStatisticsFromDb = characterStatisticEntityService.GetAll().Where(x => x.CharacterId == characterFromDb.Id).ToList();
                    foreach (var statFromDb in characterStatisticsFromDb)
                    {
                        var statFromBody = character.Statistics.FirstOrDefault(x => x.StatisticId == statFromDb.StatisticId);
                        statFromDb.CurrentValue = statFromBody.CurrentValue;

                        await characterStatisticEntityService.UpdateAsync(statFromDb);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, true);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "ModelState Not Valid");

        }


        [HttpGet]
        [Route("api/Character/Gender/Lookup")]
        public HttpResponseMessage GetCharacterGenderLookup()
        {
            var result = new List<LookupModel>();
            foreach (var gender in Enum.GetValues(typeof(Gender)))
            {
                result.Add(new LookupModel()
                {
                    Key = (int)gender,
                    Value = gender.ToString()
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [HttpGet]
        [Route("api/Character/GameSystem/Lookup")]
        public HttpResponseMessage GetCharacterGameSystemLookup()
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
        [Route("api/Character/GameSystem/{id}")]
        public HttpResponseMessage GetGameSystemStatistics([FromUri]int id)
        {

            var system = (GameSystem)id;

            var statistics = statisticEntityService.GetAll().Where(x=>x.GameSystem == system).ToList();
            if (statistics != null && statistics.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, statistics);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "System not found");

        }

    }
}
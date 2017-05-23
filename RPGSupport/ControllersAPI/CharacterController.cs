using Entities;
using Interfaces.Services;
using Microsoft.AspNet.Identity;
using RPGSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static Entities.Enums;

namespace RPGSupport.ControllersAPI
{
    public class CharacterController : ApiController

    {
        private IEntityService<Character> characterEntityService;

        public CharacterController(IEntityService<Character> characterEntityService)
        {
            this.characterEntityService = characterEntityService;
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
                UserId = userId

            };

            


            characterEntityService.Add(newCharacter);
            
            return Request.CreateResponse(HttpStatusCode.OK, newCharacter);

        }

        [HttpPost]
        [Route("api/Character/GameSystemStatistics")]
        public HttpResponseMessage GameSystemSelect([FromBody] Character system)
        {
            //string systemName = system.GameSystem.ToString();
            //var selectedGameSystem = Enum.Parse(typeof(GameSystem), systemName);

            var selectedSystem = (GameSystem)Enum.Parse(typeof(GameSystem), system.GameSystem.ToString());

            switch (selectedSystem)
            {
                case GameSystem.Warhammer:
                    var newStatistics = new List<Statistic>() {
                        new Statistic() { Id=1, Name="WW"},
                        new Statistic() { Id=2, Name="US"},
                        new Statistic() { Id=3, Name="Odp"},

                    };

                    return Request.CreateResponse(HttpStatusCode.OK, newStatistics);
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "System not found");

        }

        [HttpGet]
        [Route("api/Character/GetAll")]
        public HttpResponseMessage GetAllCharacters()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var characters = characterEntityService.GetAll().Where(x => x.UserId == userId);
            return Request.CreateResponse(HttpStatusCode.OK, characters);

        }

        [Route("api/Character/{id}")]
        public async Task<HttpResponseMessage> Put ([FromBody]Character character)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var characterFromDb = characterEntityService.GetSingle(x => x.Id == character.Id);
                    characterFromDb.Name = character.Name;
                    characterFromDb.Gender = character.Gender;
 
                    await characterEntityService.UpdateAsync(characterFromDb);
                    return Request.CreateResponse(HttpStatusCode.OK, true);

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Character Not Found");


        }
    }
}
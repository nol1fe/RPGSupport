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

        [HttpGet]
        [Route("api/Character/GetAll")]
        public HttpResponseMessage GetAllCharacters()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var characters = characterEntityService.GetAll().Where(x => x.UserId == userId);
            return Request.CreateResponse(HttpStatusCode.OK, characters);

        }

        [Route("api/Character")]
        public async Task<HttpResponseMessage> Put ([FromBody]Character value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await characterEntityService.UpdateAsync(value);
      
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
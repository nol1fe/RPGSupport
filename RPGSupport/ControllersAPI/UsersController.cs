using DataAccess.UnitOfWork;
using Entities;
using Interfaces.Services;
using Interfaces.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RPGSupport.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RPGSupport
{
    [Authorize]
    public class UsersController : ApiController
    {

        private IEntityService<User> userEntityService;

        public UsersController(IEntityService<User> userEntityService)
        {
            this.userEntityService = userEntityService;
        }

        [Route("api/Users")]
        public HttpResponseMessage GetAllUsers()
        {
            var result = userEntityService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/Users/{id}")]
        public HttpResponseMessage GetUser([FromUri]int id)
        {
            var user = userEntityService.GetById(id);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " User Not Found");
            }

        }

      
        [Route("api/Users/GetCurrentUser")]
        public HttpResponseMessage GetCurrent()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var user = userEntityService.GetSingle(x => x.Id == userId);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);

            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, " User Not Found");

        }

    }
}
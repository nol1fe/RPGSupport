using DataAccess.UnitOfWork;
using Entities;
using Interfaces.Services;
using Interfaces.UnitOfWork;
using RPGSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [Route("api/Users/GetUser")]
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
    }
}
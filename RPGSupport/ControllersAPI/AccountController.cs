using Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RPGSupport.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RPGSupport.ControllersAPI
{
    public class AccountController : ApiController
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authManager;


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authManager;
            }
        }
        // POST api/login
        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<HttpResponseMessage> Login([FromBody]LoginViewModel value)
        {
            if (value == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user");
            }
            var result = await SignInManager.PasswordSignInAsync(value.Email, value.Password, value.RememberMe, shouldLockout: false);

            //var result = SignInManager.PasswordSignInAsync(value.Email, value.Password, value.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    string returnUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    response.Headers.Location = new Uri(returnUrl);
                    return response;
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user");

            }
        }
        // POST: /api/register
        [HttpPost]
        [AllowAnonymous]
        [Route("api/register")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    string returnUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    response.Headers.Location = new Uri(returnUrl);
                    return response;
          
                }
       
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user");

        }
    }

}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : IUser<int>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Func<User, string, Task<ClaimsIdentity>> identityCreator)
        {
            var userIdentity = await identityCreator(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.UserName));
            //userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id.ToString()));

            // TODO: Tu można dodać nowe dane
            return userIdentity;
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool LockoutEnabled { get; set; }

    }
}

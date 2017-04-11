using DataAccess;
using Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    //    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    //    public class ApplicationUserManager : UserManager<User, int>
    //    {
    //        public ApplicationUserManager(IUserStore<User, int> store)
    //            : base(store)
    //        {
    //        }

    //        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //        {
    //            var manager = new ApplicationUserManager(new ApplicationUserStore());
    //            // Configure validation logic for usernames
    //            manager.UserValidator = new UserValidator<User, int>(manager)
    //            {
    //                AllowOnlyAlphanumericUserNames = false,
    //                RequireUniqueEmail = true
    //            };

    //            // Configure validation logic for passwords
    //            manager.PasswordValidator = new PasswordValidator
    //            {
    //                RequiredLength = 6,
    //                RequireNonLetterOrDigit = false,
    //                RequireDigit = false,
    //                RequireLowercase = true,
    //                RequireUppercase = false,
    //            };

    //            // Configure user lockout defaults
    //            manager.UserLockoutEnabledByDefault = true;
    //            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //            // You can write your own provider and plug it in here.
    //            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, int>
    //            {
    //                MessageFormat = "Your security code is {0}"
    //            });
    //            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, int>
    //            {
    //                Subject = "Security Code",
    //                BodyFormat = "Your security code is {0}"
    //            });
    //            manager.EmailService = new EmailService();
    //            manager.SmsService = new SmsService();
    //            var dataProtectionProvider = options.DataProtectionProvider;
    //            if (dataProtectionProvider != null)
    //            {
    //                manager.UserTokenProvider =
    //                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
    //            }
    //            return manager;
    //        }
    //    }
    //}
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = false;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, int>
            {
                MessageFormat = "Your security code is {0}"
            });

            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            EmailService = new EmailService();
            SmsService = new SmsService();

            UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}
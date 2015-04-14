using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Model;

namespace PmaPlus
{
    public class MyUserStore : IUserStore<User, int>, IUserPasswordStore<User, int>, IUserLockoutStore<User, int>, IUserTwoFactorStore<User, int>
    {
        private IUserRepository _userRepository;

        public MyUserStore()
        {
            _userRepository = new UserRepository(new DatabaseFactory());
        }
        public MyUserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {

        }

        public Task CreateAsync(User user)
        {
            _userRepository.Add(user);

            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(User user)
        {
            _userRepository.Update(user);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(User user)
        {
            _userRepository.Delete(user);
            return Task.FromResult<object>(null);
        }

        public Task<User> FindByIdAsync(int userId)
        {

            return Task.FromResult<User>(_userRepository.GetById(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.FromResult<User>(_userRepository.Get(u => u.UserName == userName));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult<string>(_userRepository.Get(u => u.Id == user.Id).Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult<bool>(!_userRepository.Get(u => u.Id == user.Id).Password.IsNullOrWhiteSpace());
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult<DateTimeOffset>(new DateTimeOffset(new DateTime(1, 1, 1)));
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult<object>(null);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult<int>(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult<int>(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult<object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }
    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }



    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager =
                new ApplicationUserManager(new MyUserStore());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

}
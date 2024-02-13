using Microsoft.Owin.Security;
using Newtonsoft.Json;
using repos11.BusinessLogic.UserManagement.Interfaces;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace repos11.Web.Utilities.Security
{
    public class BasicAuthentication
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly IUsersBusinessLogic _userBL;
        ILogger _logger;

        public BasicAuthentication(IUsersBusinessLogic userBL, ILogger logger)
        {
            _userBL = userBL;
            _logger = logger;
            authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
        }

        public async Task<AuthenticationResult> SignIn(string username)
        {
            try
            {
                var identity = await CreateIdentity(username);

                if (identity != null)
                {
                    authenticationManager.SignOut(ConstansAuthentication.DeafultCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                    return new AuthenticationResult();
                }

                _logger.Information("Invalid username or password");
                return new AuthenticationResult("Invalid username or password");
            }
            catch (Exception ex)
            {
                _logger.Information("Invalid username or password", ex);
                return new AuthenticationResult("Invalid username or password");
            }
        }

        private async Task<ClaimsIdentity> CreateIdentity(string username)
        {
            var user = await _userBL.GetByUserName(username);

            if (user != null)
            {
                var identity = new ClaimsIdentity(ConstansAuthentication.DeafultCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", ConstansAuthentication.DeafultProvider));

                identity.AddClaim(new Claim("UserId", user.Id.ToString(), ClaimValueTypes.Integer64));
                identity.AddClaim(new Claim(ClaimTypes.Name, username));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim("FullName", user.Name ?? ""));

                try
                {
                    var _roles = await _userBL.GetRoles(user.Id);

                    foreach (var _role in _roles)
                        identity.AddClaim(new Claim(ClaimTypes.Role, _role));
                }
                catch (Exception ex)
                {
                    _logger.Error($"error add claims roles username {user.UserName}", ex);
                }

                try
                {
                    var _actions = await _userBL.GetSystemActionByUser(user.Id);
                    var _actionsJson = JsonConvert.SerializeObject(_actions);

                    identity.AddClaim(new Claim("actions", _actionsJson));
                }
                catch (Exception ex)
                {
                    _logger.Error($"error add claims permision actions username {user.UserName}", ex);
                }

                return identity;
            }

            return null;
        }
    }

    public class AuthenticationResult
    {
        public AuthenticationResult(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
        public Boolean IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}
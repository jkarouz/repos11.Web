using Newtonsoft.Json;
using repos11.BusinessLogic.Dtos.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace repos11.Web.Models
{
    public class IdenityInfo
    {
        public static IEnumerable<Claim> claims
        {
            get
            {
                try
                {
                    return ((ClaimsPrincipal)HttpContext.Current.User).Claims;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static long UserId
        {
            get
            {
                try
                {
                    return Convert.ToInt64(claims.Where(c => c.Type == "UserId").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public static string UserName
        {
            get
            {
                try
                {
                    return Convert.ToString(claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string FullName
        {
            get
            {
                try
                {
                    return Convert.ToString(claims.Where(c => c.Type == "FullName").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string Email
        {
            get
            {
                try
                {
                    return Convert.ToString(claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static List<FnGetSystemActionDto> PermissionActions
        {
            get
            {
                try
                {
                    var _actions = Convert.ToString(claims.Where(c => c.Type == "actions").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());

                    if (!string.IsNullOrWhiteSpace(_actions))
                        return JsonConvert.DeserializeObject<List<FnGetSystemActionDto>>(_actions);

                    return new List<FnGetSystemActionDto>();
                }
                catch (Exception)
                {
                    return new List<FnGetSystemActionDto>();
                }
            }
        }
    }
}
using System;
using System.Web;
using System.Web.Mvc;

namespace repos11.Web
{
    public static class UrlHelperExtension
    {
        public static string AbsoluteAction(this UrlHelper url, string virtualPath)
        {
            Uri requestUrl = url.RequestContext.HttpContext.Request.Url;

            var result = string.Format("{0}://{1}{2}",
                                   requestUrl.Scheme,
                                   requestUrl.Authority,
                                   VirtualPathUtility.ToAbsolute(virtualPath));

            return result;
        }
    }
}
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using TennisScheduler;

namespace TennisScheduler.Models
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr
                = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using P132Pustok.DAL;
using P132Pustok.Models;

namespace P132Pustok.Attributes.FilterAttributes
{
    public class AuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string[] roles = Roles.Split(',');

            string username = context.HttpContext.Session.GetString("username");
            var db = context.HttpContext.RequestServices.GetRequiredService<PustokContext>();

            Account account = db.Accounts.FirstOrDefault(x => x.Username == username);

            if(account==null || !roles.Contains(account.Role))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "account", action = "login" }));
            }
        }
    }
}

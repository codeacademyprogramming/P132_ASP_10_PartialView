using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace P132Pustok.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WeChooz.TechAssessment.Web.Authentication;

[Route("authentication")]
public class AuthenticationController: Controller
{
    [HttpGet]
    public ActionResult Handle()
    {
        Response.Headers[HeaderNames.CacheControl] = "no-cache, must-revalidate";
        return View();
    }
}
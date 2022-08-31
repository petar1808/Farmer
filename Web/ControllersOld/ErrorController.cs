using Microsoft.AspNetCore.Mvc;

namespace Web.ControllersOld
{
    public class ErrorController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult ServerError()
        {
            return View();
        }

        //public IActionResult NotFound()
        //{
        //    return View();
        //}

        //public IActionResult BadRequest()
        //{
        //    return View();
        //}
    }
}

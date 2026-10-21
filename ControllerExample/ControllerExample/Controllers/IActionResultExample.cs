using Microsoft.AspNetCore.Mvc;

namespace ControllerExample.Controllers
{
    public class IActionResultExample : Controller
    {
        [Route("/books")]
        public IActionResult GetBook()
        {
            if (!ControllerContext.HttpContext.Request.Query.ContainsKey("bookId"))
            {
                Response.StatusCode = 400;
                return Content("Book id is not supplied");
            }
            if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.HttpContext.Request.Query["bookid"])))
            {
                Response.StatusCode = 400;
                return Content("Book id can't be null or empty");
            }
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if(bookId <= 0)
            {
                Response.StatusCode = 400;
                return Content("Book id can't be equal to or  less then zero");
            }
            if(bookId >= 1000)
            {
                Response.StatusCode = 400;
                return Content("Book id can't be greater then 1000");
            }
            if (Convert.ToBoolean(ControllerContext.HttpContext.Request.Query["isloggedin"]) == false)
            {
                Response.StatusCode = 401;
                return Content("User must be authenticated");
            }

            return File("/payment.pdf", "application/pdf");
        }
    }
}

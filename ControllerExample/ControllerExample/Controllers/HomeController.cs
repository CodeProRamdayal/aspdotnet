using Microsoft.AspNetCore.Mvc;
using ControllerExample.Models;

namespace ControllerExample.Controllers
{
   public class  HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/")]
        public string Home()
        {
            return "Hello Man i am Home";
        }

        [Route("/contact")]
        public string Contact()
        {
            return "Hello Man pls contact";
        }

        [Route("/contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string ContactUS()
        {
            return "Hello Man pls contact";
        }


        [Route("/about")]
        public string AboutPage()
        {
            return "Hello Man pls know about to me";
        }

        [Route("/htmlresp")]
        public ContentResult GetMIMETYPE()
        {
            //return new ContentResult()
            //{
            //    Content = "<h1> Hello Ramdayal Patel !<h1>",
            //    ContentType = "text/html"
            //};

            // OR
            return Content("<h2> Hello Mr. Ramdayal Patel<h2>","text/html");
        }
        [Route("/person")]

        public JsonResult GetPerson()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ramdayal",
                LastName ="Patel",
                EmailId="test.example.com",
                Age = 28
            };

            //return new JsonResult(person);
            return Json(person);
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/payment.pdf", "application/pdf");
            return File("/payment.pdf", "application/pdf");

        }

        [Route("physical-download")]
        public PhysicalFileResult PhysicalFileDownload()
        {
            //return new PhysicalFileResult(@"C:\\Users\\Ram\\Downloads\\RESIGN.png\", "image/png");
            return PhysicalFile(@"C:\\Users\\Ram\\Downloads\\RESIGN.png\", "image/png");

        }

        [Route("content-download")]
        public IActionResult FileContectDownload()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\\Users\\Ram\\Downloads\\RESIGN.png\");
            //return new FileContentResult(bytes, "image/png");

            return File(bytes, "image/png");

        }


    }
}

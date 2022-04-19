using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //controller je class za MVC controller s supportom za View (angular). Povemo fallback controlerju kater file mu bomo serviral, in apiju kaj naredit z rootom ki ga ne pozna
    public class FallbackController : Controller
    {
        public ActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),  "wwwroot", "index.html"), "text/HTML");
        }
    }
}
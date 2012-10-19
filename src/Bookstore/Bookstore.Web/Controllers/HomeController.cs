using System.Web.Mvc;
using Bookstore.Common;
using RabbitBus;

namespace Bookstore.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly IBus _bus;

        public HomeController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order(string bookName)
        {
            _bus.Publish(new BookOrder(bookName));
            return View("OrderResults");
        }
    }
}
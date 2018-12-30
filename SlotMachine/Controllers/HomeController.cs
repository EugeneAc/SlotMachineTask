using SlotMachine.Machines;
using SlotMachine.Models;
using System.Web.Mvc;

namespace SlotMachine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var machine = new VirtualSlotMachine(3);
            var model = new PlayModel();
            model.Credits = 100;
            model.Slots = machine.Slots;
            return View(model);
        }

        [HttpPost]
        public int CheckCombination(string[] combination, int credits)
        {
            return VirtualSlotMachine.CalcPayout(combination, credits);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
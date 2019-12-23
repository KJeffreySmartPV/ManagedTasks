namespace ManagedTasks.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Steeltoe.Common.Tasks;

    public class HomeController : Controller
    {
        private List<IApplicationTask> tasks;

        public HomeController(IEnumerable<IApplicationTask> tasks)
        {
            this.tasks = tasks.ToList();
        }

        public IActionResult Index()
        {
            return View(tasks.Select(t => t.Name));
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Collector.ViewComponents.Search
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

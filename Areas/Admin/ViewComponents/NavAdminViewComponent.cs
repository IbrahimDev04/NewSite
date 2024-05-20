using Microsoft.AspNetCore.Mvc;

namespace GameApp.Areas.Admin.ViewComponents
{
    public class NavAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

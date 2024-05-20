using Microsoft.AspNetCore.Mvc;

namespace GameApp.Areas.Admin.ViewComponents
{
    public class HeaderAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

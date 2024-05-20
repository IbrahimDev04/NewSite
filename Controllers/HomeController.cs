using GameApp.DataAccessLayer;
using GameApp.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApp.Controllers
{
    public class HomeController(SperingDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var data = await _context.categories
                .Select(s => new GetCategoryVM
                {
                    Name = s.Name,
                    IconName = s.IconName,

                }).ToListAsync();

            return View(data);
        }
    }
}

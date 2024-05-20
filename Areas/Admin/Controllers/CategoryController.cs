using GameApp.DataAccessLayer;
using GameApp.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class CategoryController(SperingDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            var data = await _context.categories
                .Select(s => new GetCategoryAdminVM
                {
                    Name = s.Name,
                    IconName = s.IconName,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdatedTime.ToString("dd MMM yyyy"),
                    Id = s.Id

                }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {

            await _context.categories.AddAsync(new Models.Category
            {
                Name = vm.Name,
                IconName = vm.IconName,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.categories.FirstOrDefault(c => c.Id == id);

            if(data == null) return NotFound();

            UpdateCategoryVM vM = new UpdateCategoryVM
            {
                Name = data.Name,
                IconName = data.IconName,
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateCategoryVM vm)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.categories.FirstOrDefault(c => c.Id == id);

            if (data == null) return NotFound();

            data.Name = vm.Name;
            data.IconName = vm.IconName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

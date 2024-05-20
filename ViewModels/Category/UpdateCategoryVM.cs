using System.ComponentModel.DataAnnotations;

namespace GameApp.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        [Required, MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }

        [Required]
        public string IconName { get; set; }
    }
}

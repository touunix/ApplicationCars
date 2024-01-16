using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationCars.Pages
{
    [Authorize(Policy = "IsMechanic")]
    public class MechanicsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

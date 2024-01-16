using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationCars.Pages
{
    [Authorize]
    public class LoginCarsServiceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

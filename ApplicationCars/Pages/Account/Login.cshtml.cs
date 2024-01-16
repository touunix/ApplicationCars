using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ApplicationCars.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Credential.UserName == "admin" && Credential.Password == "1")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@site.com"),
                    new Claim("Job", "Admin")
                };
                var identity = new ClaimsIdentity(claims, "Cookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookie", claimsPrincipal);
                return RedirectToPage("/Cars/Index");
            }

            if (Credential.UserName == "mechanic" && Credential.Password == "2")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "mechanic"),
                    new Claim(ClaimTypes.Email, "mechanic@site.com"),
                    new Claim("Job", "Mechanic")
                };
                var identity = new ClaimsIdentity(claims, "Cookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookie", claimsPrincipal);
                return RedirectToPage("/Mechanics");
            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
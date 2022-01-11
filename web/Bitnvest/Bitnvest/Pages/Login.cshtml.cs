using Bitnvest.Business.Handlers;
using Bitnvest.Model.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitnvest.Pages
{
    public class LoginModel : PageModel
    {
        CorrentistaHandler _handler;

        [BindProperty]
        public LoginDTO Login { get; set; }

        public LoginModel(IHttpContextAccessor http)
        {
            _handler = new CorrentistaHandler(http);
            Login = new LoginDTO();
        }

        public void OnGet()
        {

            _handler.Deslogar();
        }

        public IActionResult OnPost()
        {
            try
            {
                var response = _handler.Logar(Login);

                if (response)
                {
                    return RedirectToPage("Dashboard");
                }

                ViewData["Error"] = "<strong>Ops!</strong> Login ou senha incorreto!";
                return Page();
            }
            catch (System.Exception)
            {
                ViewData["Error"] = "<strong>Error!</strong> Ocorreu um erro no sistema, Tente novamente!";
                return Page();
            }

        }
    }
}

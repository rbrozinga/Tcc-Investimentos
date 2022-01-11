using Bitnvest.Business.Handlers;
using Bitnvest.Model.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitnvest.Pages
{
    public class CadastroModel : PageModel
    {

        CorrentistaHandler _handler;

        [BindProperty]
        public CadastroDTO CadastroDTO { get; set; }

        public CadastroModel(IHttpContextAccessor http)
        {
            _handler = new CorrentistaHandler(http);
        }


        public void OnGet()
        {

        }
        public IActionResult OnPostCadastro(CadastroDTO cadastro)
        {
            try
            {
                var cad = cadastro;
                var correntista = _handler.CriarCorrentista(cad);

                if(correntista != null)
                {
                    ViewData["Error"] = "<strong>Error!</strong> Esse email está cadastrado!";
                    return Page();
                }

                return new RedirectToPageResult("Login");

            }
            catch (System.Exception)
            {
                ViewData["Error"] = "<strong>Error!</strong> Ocorreu um erro no sistema, Tente novamente!";
                return Page();
            }
        }
    }
}

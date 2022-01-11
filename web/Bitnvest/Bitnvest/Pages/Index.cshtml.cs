using Bitnvest.Business.Handlers;
using Bitnvest.Model.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bitnvest.Pages
{
    public class IndexModel : PageModel
    {
        CorrentistaHandler _handler;

        [BindProperty]
        public CadastroDTO CadastroDTO { get; set; }

        public IndexModel(IHttpContextAccessor http)
        {
            _handler = new CorrentistaHandler(http);
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostCadastro(PreCadastroDTO cadastro)
        {
            return new RedirectToPageResult("Cadastro");
        }
    }
}

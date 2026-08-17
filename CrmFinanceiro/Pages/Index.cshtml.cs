using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrmFinanceiro.Data.Services;
using CrmFinanceiro.Data.Dto;

namespace CrmFinanceiro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FinanceiroCaixaService _financeiroCaixaService;

        public FinanceiroCaixaDTO resumoDia = null;

        public IndexModel(FinanceiroCaixaService financeiroCaixaService)
        {
            _financeiroCaixaService = financeiroCaixaService;
        }
        public void OnGet()
        {
            resumoDia = _financeiroCaixaService.carregaResumoDia();
        }
    }
}

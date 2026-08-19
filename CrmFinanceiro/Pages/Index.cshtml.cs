using Microsoft.AspNetCore.Mvc.RazorPages;
using CrmFinanceiro.Data.Services;
using CrmFinanceiro.Data.Dto;
using CrmFinanceiro.Data.DTOs;

namespace CrmFinanceiro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FinanceiroCaixaService _financeiroCaixaService;

        public ResumoCaixaDTO ResumoDia { get; private set; }
        public List<TitulosAcaoDTO> Titulos { get; private set; }

        public IndexModel(FinanceiroCaixaService financeiroCaixaService)
        {
            _financeiroCaixaService = financeiroCaixaService;
        }

        public void OnGet()
        {
            Task<ResumoCaixaDTO> resumoDia = _financeiroCaixaService.CarregaResumoDiaAsync();
            Task<List<TitulosAcaoDTO>> titulos = _financeiroCaixaService.CarregarTitulos();

            if (resumoDia != null) ResumoDia = resumoDia.Result;
            if (titulos != null) Titulos = titulos.Result;
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using CrmFinanceiro.Data.Services;
using CrmFinanceiro.Data.Dto;
using CrmFinanceiro.Data.DTOs;

namespace CrmFinanceiro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FinanceiroCaixaService _financeiroCaixaService;
        public ResumoCaixaDTO ResumoDia { get; private set; } = new ResumoCaixaDTO(0, 0);
        public List<TitulosAcaoDTO> Titulos { get; private set; } = new List<TitulosAcaoDTO>();

        public IndexModel(FinanceiroCaixaService financeiroCaixaService)
        {
            _financeiroCaixaService = financeiroCaixaService;
        }

        public async Task OnGetAsync()
        {
            // Código corrigido
            ResumoDia = await _financeiroCaixaService.CarregaResumoDiaAsync();
            Titulos = await _financeiroCaixaService.CarregarTitulos();
       
        }
    }
}

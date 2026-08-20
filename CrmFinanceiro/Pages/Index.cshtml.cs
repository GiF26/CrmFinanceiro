using Microsoft.AspNetCore.Mvc.RazorPages;
using CrmFinanceiro.Data.Services;
using CrmFinanceiro.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CrmFinanceiro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FinanceiroCaixaService _financeiroCaixaService;
        public ResumoCaixaDTO ResumoDia { get; private set; } = new ResumoCaixaDTO(0, 0, 0, 0);
        public List<TitulosAcaoDTO> Titulos { get; private set; } = new List<TitulosAcaoDTO>();

        [BindProperty(SupportsGet = true)]
        public string FiltroDoc { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string FiltroParceiro { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string FiltroTipo { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public DateTime DataInicio { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public DateTime DataFim { get; set; } = DateTime.Today;

        public IndexModel(FinanceiroCaixaService financeiroCaixaService)
        {
            _financeiroCaixaService = financeiroCaixaService;
        }

        public async Task OnGetAsync()
        {
            FiltrosConciliacaoDTO filtros = new FiltrosConciliacaoDTO(FiltroDoc, FiltroParceiro, FiltroTipo, DataInicio, DataFim);

            ResumoDia = await _financeiroCaixaService.CarregaResumoDiaAsync(filtros);
            Titulos = await _financeiroCaixaService.CarregarTitulos(filtros);
       
        }

        
    }
}

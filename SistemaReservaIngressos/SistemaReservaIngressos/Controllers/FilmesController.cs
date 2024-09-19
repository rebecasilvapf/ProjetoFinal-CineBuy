using SistemaReservaIngressos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace SistemaReservaIngressos.Controllers
{
    public class FilmesController
    {
        private readonly FilmeService filmeService;

        public FilmesController()
        {
            filmeService = new FilmeService(new System.Net.Http.HttpClient());
        }
        public async Task CarregarFilmesAssync(GridView gridView)
        {
            try
            {
                // Chama o serviço para buscar todos os filmes
                var filmes = await filmeService.BuscarTodosFilmesAsync();

                // Preenche o GridView com os filmes
                gridView.DataSource = filmes;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                // Tratar erros de carregamento de filmes
                throw new Exception("Erro ao carregar filmes", ex);
            }
        }
    }
}
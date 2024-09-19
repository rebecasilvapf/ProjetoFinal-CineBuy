using SistemaReservaIngressos.Controllers;
using SistemaReservaIngressos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaReservaIngressos
{
    public partial class Filmes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnCompraClick(object sender, ImageClickEventArgs e) 
        {
            try
            {   //Converte o objeto sender para um ImageButton para permitir o acesso a suas propriedades
                ImageButton click = (ImageButton)sender; 

                //Captura o Id do filme com base no CommandArgument passado na tag referente a cada filme
                string filmeId = click.CommandArgument;

                //EndPoint da API referente a buscar o filme
                string apiUrl = $"http://localhost:5109/api/Filme/BuscarFilme/{filmeId}";

                //Instância do HttpClient para fazer requisições HTTP
                using (HttpClient client = new HttpClient()) 
                {
                    //Resposta do método da GET da API
                    HttpResponseMessage response = await client.GetAsync(apiUrl); 

                    //Caso a resposta for sucesso 200
                    if (response.IsSuccessStatusCode) 
                    {
                        Filme filme = await response.Content.ReadAsAsync<Filme>(); //Deserializa a resposta JSON para o objeto Filme
                        string detalhesUrl = $"~/Views/ComprarFilme.aspx?id={filmeId}"; //Url da página de reserva/compra
                        Response.Redirect(detalhesUrl); //Redirecionameto para a página
                    }
                    else
                    {
                        throw new ArgumentException("Erro ao buscar o filme na API");
                    }
                }
            }
            catch (Exception ex)
            { 
                throw new Exception ("Erro: ", ex);
            }
       }
    }
}
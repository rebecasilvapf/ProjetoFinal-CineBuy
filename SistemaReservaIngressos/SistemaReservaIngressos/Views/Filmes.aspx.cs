using Newtonsoft.Json.Linq;
using SistemaReservaIngressos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaReservaIngressos
{
    public partial class Filmes : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
          
             // Chama a função para buscar os filmes e preencher os controles
              await PreencherFilmes();
  
        }

        private async Task PreencherFilmes()
        {
            string apiUrl = $"http://localhost:5109/api/Filme/BuscarTodosFilmes";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //Faz a chamada a API e armazena a reposta
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        //Converte a resposta da API em um array JSON
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        JArray filmes = JArray.Parse(jsonResponse);

                        //Itera sobre os filmes e criar os controles
                        foreach (var filme in filmes)
                        {
                            //Cria o ImageButton dinamicamente
                            ImageButton imgButton = new ImageButton
                            {
                                ImageUrl = filme["imageUrl"].ToString(),
                                CssClass = "image-button-filmes",
                                Height = 250,
                                Width = 181,
                                CommandArgument = filme["filmeId"].ToString()  // ID do filme (para identificação)
                            };
                            imgButton.Click += new ImageClickEventHandler(btnCompraClick); // Adiciona o evento de clique

                            //Limita o tamanho do titulo para não ultrapassar
                            string titulo = filme["titulo"].ToString();
                            if (titulo.Length > 20)
                            {
                                titulo = titulo.Substring(0, 21) + "...";  // Corta o título e adiciona "..."
                            }

                            // Cria o Label dinamicamente para exibir o título do filme
                            Label lblFilme = new Label
                            {
                                Text = titulo,
                                CssClass = "label-filme"
                            };

                            // Cria um container (div) para organizar a exibição do botão e título
                            var divContainer = new Panel {};
                            divContainer.Controls.Add(imgButton); // Adiciona o botão de imagem ao container
                            divContainer.Controls.Add(new Literal { Text = "<br />" }); // Quebra de linha
                            divContainer.Controls.Add(lblFilme);  // Adiciona o título ao container

                            // Adiciona o container ao controle principal na página (filmesContainer)
                            filmesContainer.Controls.Add(divContainer);
                        }
                    }
                    else
                    {
                        // Se a API falhar, exibe uma mensagem de erro
                        Label lblErro = new Label
                        {
                            Text = "Erro ao carregar os filmes.",
                            CssClass = "error-message" // Classe CSS para a mensagem de erro
                        };
                        filmesContainer.Controls.Add(lblErro); // Adiciona a mensagem ao container principal
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro genérico, exibe uma mensagem de erro
                Label lblErro = new Label
                {
                    Text = $"Erro ao carregar os filmes: {ex.Message}",
                    CssClass = "error-message" // Classe CSS para a mensagem de erro
                };
                filmesContainer.Controls.Add(lblErro); // Adiciona a mensagem ao container principal
            }
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
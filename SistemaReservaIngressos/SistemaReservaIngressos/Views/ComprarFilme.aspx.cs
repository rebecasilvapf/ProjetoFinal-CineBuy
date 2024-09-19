using SistemaReservaIngressos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaReservaIngressos.Views
{
    public partial class ComprarFilme : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string filmeId = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(filmeId))
                {
                    await CarregarInfoFilmes(filmeId);
                    await CarregarHorarios(filmeId);
                }
            }
        }

        private async Task CarregarInfoFilmes(string filmeId)
        {
            //EndPoint da API referente a buscar o filme
            string apiUrl = $"http://localhost:5109/api/Filme/BuscarFilme/{filmeId}"; 

            using (HttpClient httpClient = new HttpClient()) 
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode) 
                    {
                        Filme filme = await response.Content.ReadAsAsync<Filme>();  //Deserializa a resposta JSON para o objeto Filme
                        //Preenche o front com os dados da API
                        imageUrl.ImageUrl = filme.ImageUrl;
                        lblTitulo.Text = filme.Titulo;
                        lblClassificacao.Text = filme.ClassiEtaria;
                        lblDuracao.Text = filme.Duracao.ToString() + "m";
                        lblGenero.Text = filme.Genero;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro:", ex);
                }
            }
        }
        private async Task CarregarHorarios(string filmeId)
        {
            string apiUrl = $"http://localhost:5109/api/Horario/BuscarHorariosFilme/{filmeId}"; 

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        //Deserializa a resposta JSON para uma lista de objetos de Horario
                        List<Horario> horarios = await response.Content.ReadAsAsync<List<Horario>>();  

                        // Verifica se a lista tem algum item
                        if (horarios.Any()) 
                        {
                            //Pega o primeiro objeto da lista   
                            Horario precoFixo = horarios.FirstOrDefault();   
                            lblPreco.Text = "R$"+ precoFixo.Preco.ToString();

                            // Preenche o DropDownList com os horários do filme
                            DropDownListHorarios.DataSource = horarios;
                            DropDownListHorarios.DataTextField = "DataHora";
                            DropDownListHorarios.DataBind(); // Vincula os dados 

                            // Preenche o DropDownList com as salas disponíveis
                            DropDownListSalas.DataSource = horarios;
                            DropDownListSalas.DataTextField = "Sala"; 
                            DropDownListSalas.DataBind(); // Vincula os dados
                        }                   
                    }
                  
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro:", ex);
                }
            }
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            //Finalizar ainda
            lblDetalhesReserva.Text = "Nome: Rebeca Paulino ";
            lblDetalhesReserva.Visible = true;
            lblDetalhesTitulo.Visible = true;
        }
    }
}

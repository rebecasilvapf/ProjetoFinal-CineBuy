using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaReservaIngressos.Views
{
    public partial class DetalhesReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Pega as informações passadas na url
                string nome = Request.QueryString["nome"];
                string horario = Request.QueryString["horario"];
                string preco = Request.QueryString["preco"];
                string assentos = Request.QueryString["assentos"];
                string filme = Request.QueryString["filme"];
                string sala = Request.QueryString["sala"];


                // Exibir os detalhes na página
                lblNomeReserva.Text = "Nome: " + nome;
                lblHorarioDiaReserva.Text = "Data/Horário: " + horario;
                lblPrecoReserva.Text = "Valor Total: R$" + preco;
                lblAssentoReserva.Text = "Assento(s): " + assentos;
                lblFilmeReserva.Text = "Filme: " + filme;
                lblSalaReserva.Text = sala;
            }
        } 

        protected async void btnCancelar_Click(object sender, EventArgs e)
        {
            // Obter o ID da reserva da query string
            string idReserva = Request.QueryString["idReserva"];

            if (!string.IsNullOrEmpty(idReserva))
            {
                // Construir a URL para a API de cancelamento
                string apiUrl = $"http://localhost:5109/api/Reserva/CancelarReserva/{idReserva}";

                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await httpClient.PutAsync(apiUrl, null);

                        if (response.IsSuccessStatusCode)
                        {
                            // Se o cancelamento for bem-sucedido, redirecionar para a página inicial
                            string mensagem = "Cancelamento foi bem sucedido!";
                            string script = $"alert('{mensagem}'); window.location='Filmes.aspx';";

                            // Registrar o script na página
                            ClientScript.RegisterStartupScript(this.GetType(), "Alerta", script, true);

                        }
                        else
                        {
                            
                            lblMensagem.Text = "Erro ao cancelar a reserva. Tente novamente.";
                        }
                    }
                    catch (Exception ex)
                    {
                       
                        lblMensagem.Text = "Ocorreu um erro inesperado: " + ex.Message;
                    }
                }
            }
            else
            {
                
                lblMensagem.Text = "ID da reserva não encontrado.";
            }
        }

        protected void btnMenuInicial_Click(object sender, EventArgs e)
        {
            //Retornar para a página inicial
            Response.Redirect($"~/Views/Filmes.aspx");
        }
    }
}

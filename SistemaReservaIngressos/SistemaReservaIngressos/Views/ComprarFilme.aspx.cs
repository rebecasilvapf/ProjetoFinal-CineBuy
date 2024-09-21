using Microsoft.VisualStudio.Services.Common.CommandLine;
using Newtonsoft.Json;
using SistemaReservaIngressos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

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
                    await CarregarAssentosDisponiveis(filmeId);
                }
            }
        }

        private async Task CarregarInfoFilmes(string filmeId)
        {
            // Endpoint da API para buscar o filme
            string apiUrl = $"http://localhost:5109/api/Filme/BuscarFilme/{filmeId}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        Filme filme = await response.Content.ReadAsAsync<Filme>(); // Deserializa a resposta JSON para o objeto Filme

                        // Preenche o front com os dados da API
                        imageUrl.ImageUrl = filme.ImageUrl;
                        lblTitulo.Text = filme.Titulo;
                        lblClassificacao.Text = filme.ClassiEtaria;
                        lblDuracao.Text = filme.Duracao.ToString() + "m";
                        lblGenero.Text = filme.Genero;
                        lblSinopse.Text = filme.Sinopse;
                    }
                }
                catch (Exception ex)
                {
                    // Lançar exceção com mensagem personalizada
                    throw new Exception("Erro ao carregar informações do filme:", ex);
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
                        // Deserializa a resposta JSON para uma lista de objetos Horario
                        List<Horario> horarios = await response.Content.ReadAsAsync<List<Horario>>();

                        // Verifica se a lista tem algum item
                        if (horarios.Any())
                        {
                            // Pega o primeiro objeto da lista
                            Horario precoFixo = horarios.FirstOrDefault();
                            lblPreco.Text = precoFixo != null ? $"R${precoFixo.Preco}" : "Preço não encontrado";

                            // Preenche o DropDownList com os horários do filme
                            DropDownListHorarios.DataSource = horarios;
                            DropDownListHorarios.DataTextField = "DataHora";
                            DropDownListHorarios.DataValueField = "HorarioId";
                            DropDownListHorarios.DataBind(); // Vincula os dados
                        }
                        else
                        {
                            lblPreco.Text = "Nenhum horário disponível";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lançar exceção com mensagem personalizada
                    throw new Exception("Erro ao carregar horários do filme:", ex);
                }
            }
        }

        protected void ddlHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Captura o horarioId selecionado no DropDownList
            string horarioId = DropDownListHorarios.SelectedValue;

            // Chama o método para carregar os assentos de acordo com o horário selecionado
            Task.Run(() => CarregarAssentosDisponiveis(horarioId)).Wait();
        }


        private async Task<List<int>> ObterIdAssentosSelecionados(int horarioId)
        {
            List<int> idsSelecionados = new List<int>();

            try
            {
                //Verificar em cada controle no Panel -> panelAssentos
                foreach (Control control in panelAssentos.Controls)
                {
                    //Verifica se o controle for um CheckBox e estiver selecionado
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        string textoAssento = checkBox.Text; //Pega o Text que está no checkbox selecionado

                        char fileira = textoAssento[0]; //Fileiras "A", "B" ou "C"
                        int numeroAssento = int.Parse(textoAssento.Substring(1)); //Número da fileira
                        try
                        {
                            int idAssento = await ConstruirIdAssento(fileira, numeroAssento, horarioId);
                            idsSelecionados.Add(idAssento);                                                      
                        }
                        catch (Exception ex)
                        {
                            lblDetalhesReserva.Text += "Erro ao construir o ID do assento: " + ex.Message + " ";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lblDetalhesReserva.Text += $"Erro ao obter assentos: {ex.Message}. ";
            }

            return idsSelecionados;
        }
        private List<string> ObterAssentosSelecionados()
        {
            List<string> assentosSelecionados = new List<string>();

            try
            {
                //Verificar em cada controle no Panel -> panelAssentos
                foreach (Control control in panelAssentos.Controls)
                {
                    if(control is CheckBox check)
                    //Verifica se o controle for um CheckBox e estiver selecionado
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        string textoAssento = checkBox.Text; //Pega o Text que está no checkbox selecionado

                        char fileira = textoAssento[0]; //Fileiras "A", "B", ou "C"
                        int numeroAssento = int.Parse(textoAssento.Substring(1)); //Número da fileira

                        try
                        {
                            string assento = $"{fileira}{numeroAssento}";
                            assentosSelecionados.Add(assento);
                        }
                        catch (Exception ex)
                        {
                            lblDetalhesReserva.Text += "Erro ao construir a numeração dos assentos: " + ex.Message + " ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblDetalhesReserva.Text += $"Erro ao obter assentos: {ex.Message}. ";
            }

            return assentosSelecionados;
        }

        private async Task<int> ConstruirIdAssento( char fileira, int numeroAssento, int horarioId)
        {
            string apiUrl = $"http://localhost:5109/api/Assento/BuscarAssentosPorHorario/{horarioId}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {                  
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserializa a resposta JSON para uma lista de Assentos
                        List<Assentos> assentos = await response.Content.ReadAsAsync<List<Assentos>>();

                        // Encontra o assento correspondente com base na fileira e no número
                        var assento = assentos.FirstOrDefault(a => a.Fileira == fileira && a.Numero == numeroAssento && a.HorarioId == horarioId);

                        if (assento != null)
                        {
                            return assento.AssentoId; // Retorna o ID do assento encontrado
                        }
                        else
                        {
                            throw new Exception("Assento não encontrado.");
                        }
                    }
                    else
                    {
                        throw new Exception("Erro ao obter os assentos.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao construir o ID do assento:", ex);
                }
            }
        }

        private async Task MudarDisponibilidadeAssento(int assentoId, bool disponivel)
        {
            try
            {
                string apiUrl = $"http://localhost:5109/api/Assento/AtualizarDisponibilidade/{assentoId}?disponivel={disponivel.ToString().ToLower()}";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PutAsync(apiUrl, null);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Erro ao atualizar a disponibilidade do assento.");
                    }
                }
            }
            catch(Exception ex)
            {
                lblDetalhesReserva.Text = "Ocorreu um erro ao atualizar a disponibilidade do assento: " + ex.Message;
                lblDetalhesReserva.Visible = true;
            }
        }

        private async Task<bool> VerificarDisponibilidadeAssentos(int assentoId)
        {
            string apiUrl = $"http://localhost:5109/api/Assento/BuscarAssento/{assentoId}";

            using(HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if(response.IsSuccessStatusCode)
                    {
                        // Deserializa a resposta JSON para um objeto Assento
                        var assento = await response.Content.ReadAsAsync<Assentos>();

                        return assento.Disponivel;
                    }
                    else
                    {
                        throw new Exception("Erro ao verificar a disponibilidade do assento.");
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception("Erro na verificação do assento:", ex);
                }
            }
        }
        private async Task CarregarAssentosDisponiveis(string horarioId)
        {
            string apiUrl = $"http://localhost:5109/api/Assento/BuscarAssentosPorHorario/{horarioId}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserializa a resposta JSON para uma lista de Assentos
                        List<Assentos> assentos = await response.Content.ReadAsAsync<List<Assentos>>();

                        // Itera sobre os controles de assento no painel
                        foreach (Control control in panelAssentos.Controls)
                        {
                            if (control is CheckBox checkBox)
                            {
                                string textoAssento = checkBox.Text;
                                char fileira = textoAssento[0];
                                int numeroAssento = int.Parse(textoAssento.Substring(1));

                                var assento = assentos.FirstOrDefault(a => a.Fileira == fileira && a.Numero == numeroAssento);

                                // Atualiza o CheckBox de acordo com a disponibilidade do assento
                                if (assento != null)
                                {
                                    checkBox.Enabled = assento.Disponivel;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblDetalhesReserva.Text = "Erro ao carregar a disponibilidade dos assentos: " + ex.Message;
                    lblDetalhesReserva.Visible = true;
                }
            }
        }
        private async Task RealizarCompra(int horarioId)
        {
            try
            {
                List<int> idsAssentosSelecionados = await ObterIdAssentosSelecionados(horarioId);
                List<string> assentosSelecionados = ObterAssentosSelecionados();

                if (idsAssentosSelecionados.Count == 0)
                {
                    lblDetalhesReserva.Text = "Nenhum assento selecionado.";
                    lblDetalhesReserva.Visible = true;
                    return;
                }

                //Verifica a disponibilidade de cada assento selecionado
                List<int> assentosIndisponiveis = new List<int>();
                foreach(int assentoId in idsAssentosSelecionados)
                {
                    bool disponivel = await VerificarDisponibilidadeAssentos(assentoId);
                    if(!disponivel)
                    {
                        assentosIndisponiveis.Add(assentoId);
                    }
                }

                //Verifica se tem assentos indisponiveis e retorna uma mensagem de quais assentos
                if (assentosIndisponiveis.Count > 0)
                {
                    lblDetalhesReserva.Text = $"Os seguintes assentos estão indisponiveis: {string.Join(",", assentosIndisponiveis)}";
                }

                ReservaRequest reservaRequest = new ReservaRequest
                {
                    HorarioId = horarioId,
                    Cliente = txtNome.Text,
                    DataReserva = DateTime.Now,
                    Confirmado = true
                };

                string apiUrl = $"http://localhost:5109/api/Reserva/AdicionarReserva";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(apiUrl, reservaRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        //Extrair o número da string da resposta da API
                        var pegarReservaId = Regex.Match(responseBody, @"\d+");

                        if (pegarReservaId.Success && int.TryParse(pegarReservaId.Value, out int reservaId))
                        {
                            foreach (int assentoId in idsAssentosSelecionados)
                            {
                                var detalheReserva = new
                                {
                                    ReservaId = reservaId,
                                    AssentoId = assentoId
                                };

                                string detalhesUrl = $"http://localhost:5109/api/DetalhesReserva/AdicionarDetalhesReserva";
                                HttpResponseMessage detalheResponse = await httpClient.PostAsJsonAsync(detalhesUrl, detalheReserva);

                                if (!detalheResponse.IsSuccessStatusCode)
                                {
                                    throw new Exception("Erro ao adicionar detalhes da reserva.");
                                }

                                // Atualiza a disponibilidade do assento para false
                                await MudarDisponibilidadeAssento(assentoId, false);
                                
                            }

                            decimal precoPorAssento = decimal.Parse(lblPreco.Text.Replace("R$", "").Trim());
                            decimal valorTotal = precoPorAssento * idsAssentosSelecionados.Count;
                            var horarioSelecionado = DropDownListHorarios.SelectedItem.Text;

                            lblDetalhesReserva.Text = $"Reserva confirmada!\n" +
                                             $"ID da Reserva: {reservaId}\n" +
                                             $"Assento Id: {string.Join(", ", idsAssentosSelecionados)}\n" +
                                             $"Nome: {txtNome.Text}\n" +
                                             $"Data e Horário: {horarioSelecionado}\n" +
                                             $"Valor Total: R${valorTotal}\n" +
                                             $"Assentos selecionados: {string.Join(", ", assentosSelecionados)}\n" +
                                             $"Filme: {lblTitulo.Text}"; 

                            lblDetalhesReserva.Visible = true;
                            lblDetalhesTitulo.Visible = true;
                        }
                        else
                        {
                            throw new Exception("Erro ao realizar a compra dos ingressos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblDetalhesReserva.Text = "Ocorreu um erro: " + ex.Message;
                lblDetalhesReserva.Visible = true;
            }
        }

        protected async void BtnFinalizarCompra_Click(object sender, EventArgs e)
        {
            // Verifica se o nome do cliente foi preenchido
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                lblDetalhesReserva.Text = "Por favor, digite seu nome.";
                lblDetalhesReserva.Visible = true;
                return;
            }

            // Pega o ID do horário selecionado
            int horarioId;
            if (!int.TryParse(DropDownListHorarios.SelectedValue, out horarioId))
            {
                lblDetalhesReserva.Text = "Horário inválido selecionado.";
                lblDetalhesReserva.Visible = true;
                return;
            }
            try
            {
                // Executa a seleção dos assentos
                await RealizarCompra(horarioId);
            }
            catch (Exception ex)
            {
                lblDetalhesReserva.Text = $"Erro: {ex.Message}";
                lblDetalhesReserva.Visible = true;
            }
        }
    }
}

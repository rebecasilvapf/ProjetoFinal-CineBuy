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
                }
            }
        }

        private async Task CarregarInfoFilmes(string filmeId)
        {
            // Endpoint da API para buscar o filme com base no id do filme
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
                    throw new Exception("Erro ao carregar informações do filme:", ex);
                }
            }
        }
        private async Task CarregarHorarios(string filmeId)
        {
            //Endpoint da API para buscar os horarios referente ao filme
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
                            // Pega o primeiro objeto da lista para preço
                            Horario precoFixo = horarios.FirstOrDefault();
                            lblPreco.Text = precoFixo != null ? $"R${precoFixo.Preco}" : "Preço não encontrado";

                            // Preenche o DropDownList com os horários do filme
                            DropDownListHorarios.DataSource = horarios.Select(h => new
                            {
                                HorarioId = h.HorarioId,
                                DataHora = h.DataHora.ToString("dd/MM/yyyy HH:mm") // Formata o horário
                            }).ToList();
                            DropDownListHorarios.DataTextField = "DataHora";
                            DropDownListHorarios.DataValueField = "HorarioId";
                            DropDownListHorarios.DataBind(); // Vincula os dados

                            DropDownListHorarios.Items.Insert(0, new ListItem("Selecione", ""));
                        }
                        else
                        {
                            lblPreco.Text = "Nenhum horário disponível";
                        }
                    }
                }
                catch (Exception ex)
                {                    
                    throw new Exception("Erro ao carregar horários do filme:", ex);
                }
            }
        }

        protected async void ddlHorarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Captura o horarioId selecionado no DropDownList
            string horarioId = DropDownListHorarios.SelectedValue;

            // Chama o método para carregar os assentos e sala de acordo com o horário
            panelAssentos.Visible = true;
            await CarregarAssentos(horarioId);
            await AtualizarSala(horarioId);
        }  

        private async Task<string> AtualizarSala(string horarioId)
        {
            //Endpoint da API para buscar os horarios referente ao horarioId
            string apiUrl = $"http://localhost:5109/api/Horario/BuscarHorario/{horarioId}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        Horario horario = await response.Content.ReadAsAsync<Horario>();
                        return lblSala.Text = horario.Sala; //Retornar a sala referente ao horario Id para o label na tela
                    }
                    else
                    {
                        return lblSala.Text = "Erro ao obter informações da sala.";
                    }
                }
                catch (Exception ex)
                {
                    return lblSala.Text = "Erro ao buscar a sala: " + ex.Message;
                }
            }
        }

        private async Task<int> ConstruirIdAssento(char fileira, int numeroAssento, int horarioId)
        {
            //Endpoint da API para buscar os assentos referente ao horarioId
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
                        int numeroAssento = int.Parse(textoAssento.Substring(1)); //Número da assento
                        try
                        {
                            int idAssento = await ConstruirIdAssento(fileira, numeroAssento, horarioId);
                            idsSelecionados.Add(idAssento);
                        }
                        catch (Exception ex)
                        {
                            lblDetalhesReserva.Text += "Erro ao obter o ID do assento: " + ex.Message + " ";
                            mostrarErros.Visible = true;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lblDetalhesReserva.Text += $"Erro ao obter assentos: {ex.Message}. ";
                mostrarErros.Visible = true;
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
                        //Verifica se o controle for um CheckBox e estiver selecionado
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            string textoAssento = checkBox.Text; //Pega o Text que está no checkbox selecionado

                            char fileira = textoAssento[0]; //Fileiras "A", "B", ou "C"
                            int numeroAssento = int.Parse(textoAssento.Substring(1)); //Número do assento

                            try
                            {
                                string assento = $"{fileira}{numeroAssento}";
                                assentosSelecionados.Add(assento);
                            }
                            catch (Exception ex)
                            {
                                lblDetalhesReserva.Text += "Erro ao construir a numeração dos assentos: " + ex.Message + " ";
                                mostrarErros.Visible = true;
                            }
                      }
                }
            }
            catch (Exception ex)
            {
                lblDetalhesReserva.Text += $"Erro ao obter assentos: {ex.Message}. ";
                mostrarErros.Visible = true;
            }

            return assentosSelecionados;
        }  

        private async Task MudarDisponibilidadeAssento(int assentoId, bool disponivel)
        {
            try
            {
                //Endpoint da API para atualizar a disponibilidade do assento 
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
            catch (Exception ex)
            {
                lblDetalhesReserva.Text = "Ocorreu um erro ao atualizar a disponibilidade do assento: " + ex.Message;
                mostrarErros.Visible = true;
            }
        }

        private async Task CarregarAssentos(string horarioId)
        {
            //Endpoint da API para buscar os assentos por horarioId
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

                        if (int.TryParse(horarioId, out int horarioIdInt))
                        {
                            // Itera sobre os controles de assento no painel
                            foreach (Control control in panelAssentos.Controls)
                            {
                                if (control is CheckBox checkBox)
                                {
                                    int id = horarioIdInt;
                                    string textoAssento = checkBox.Text;
                                    char fileira = textoAssento[0];
                                    int numeroAssento = int.Parse(textoAssento.Substring(1));

                                    //Busca um assento correspondente na lista de assentos obtida da API
                                    var assento = assentos.FirstOrDefault(a => a.Fileira == fileira && a.Numero == numeroAssento && a.HorarioId == horarioIdInt);

                                    // Atualiza o CheckBox de acordo com a disponibilidade do assento
                                    if (assento != null)
                                    {
                                        checkBox.Enabled = assento?.Disponivel ?? false;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblDetalhesReserva.Text = "Erro ao carregar a disponibilidade dos assentos: " + ex.Message;
                    mostrarErros.Visible = true;
                }
            }
        }

        private async Task RealizarCompra(int horarioId)
        {          
            try
            {
                List<int> idsAssentosSelecionados = await ObterIdAssentosSelecionados(horarioId);
                List<string> assentosSelecionados = ObterAssentosSelecionados();

                //Verifica se algum assento foi selecionado
                if (idsAssentosSelecionados.Count == 0)
                {
                    lblDetalhesReserva.Text = "Nenhum assento selecionado.";
                    mostrarErros.Visible = true;
                    return;
                }

                //Cria um objeto ReservaRequest com os dados da reserva
                ReservaRequest reservaRequest = new ReservaRequest
                {
                    HorarioId = horarioId,
                    Cliente = txtNome.Text,
                    DataReserva = DateTime.Now,
                    Confirmado = true
                };

                //Endpoint da API para adicionar a nova reserva
                string apiUrl = $"http://localhost:5109/api/Reserva/AdicionarReserva";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(apiUrl, reservaRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();

                        //Extrair o número do id da reserva da resposta da API
                        var pegarReservaId = Regex.Match(responseBody, @"\d+");

                        if (pegarReservaId.Success && int.TryParse(pegarReservaId.Value, out int reservaId))
                        {
                            // Adiciona detalhes da reserva para cada assento selecionado
                            foreach (int assentoId in idsAssentosSelecionados)
                            {
                                var detalheReserva = new
                                {
                                    ReservaId = reservaId,
                                    AssentoId = assentoId
                                };

                                //Endpoint da API para adicionar os detalhes da reserva
                                string detalhesUrl = $"http://localhost:5109/api/DetalhesReserva/AdicionarDetalhesReserva";
                                HttpResponseMessage detalheResponse = await httpClient.PostAsJsonAsync(detalhesUrl, detalheReserva);

                                if (!detalheResponse.IsSuccessStatusCode)
                                {
                                    throw new Exception("Erro ao adicionar detalhes da reserva.");
                                }

                                // Atualiza a disponibilidade do assento para false
                                await MudarDisponibilidadeAssento(assentoId, false);
                            }

                            //Preencher os campos a ser passado na url para a página de confirmação da reserva
                            decimal precoPorAssento = decimal.Parse(lblPreco.Text.Replace("R$", "").Trim());
                            decimal valorTotal = precoPorAssento * idsAssentosSelecionados.Count;

                            var horarioSelecionado = DropDownListHorarios.SelectedItem.Text;
                            string horarioData = horarioSelecionado.ToString();
                            string assentos = string.Join(", ", assentosSelecionados);
                            string nome = txtNome.Text;
                            string filme = lblTitulo.Text;
                            string sala = lblSala.Text;
                            string idReserva = reservaId.ToString();

                            //Redireciona para a outra página
                            string url = $"ConfirmacaoReserva.aspx?idReserva={idReserva}&nome={nome}&horario={horarioData}&preco={valorTotal}&assentos={assentos}&filme={filme}&sala={sala}";
                            Response.Redirect(url);                        
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
                mostrarErros.Visible = true;
            }
        }

        protected async void BtnFinalizarCompra_Click(object sender, EventArgs e)
        {
            // Verifica se o nome do cliente foi preenchido
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                lblDetalhesReserva.Text = "Por favor, digite seu nome.";
                mostrarErros.Visible = true;
                return;
            }

            // Pega o ID do horário selecionado
            int horarioId;
            if (!int.TryParse(DropDownListHorarios.SelectedValue, out horarioId))
            {
                lblDetalhesReserva.Text = "Horário inválido selecionado.";
                mostrarErros.Visible = true;
                return;
            }
            try
            {
                // Executa o método de realizar compra
                await RealizarCompra(horarioId);
            }
            catch (Exception ex)
            {
                lblDetalhesReserva.Text = $"Erro: {ex.Message}";
                mostrarErros.Visible = true;
            }
        }
    }
}
using Newtonsoft.Json;
using SistemaReservaIngressos.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace SistemaReservaIngressos.Services
{
    public class FilmeService
    {
        private readonly HttpClient _httpClient;

        public FilmeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri("http://localhost:5109/api/Filme/");
        }

        public async Task<List<Filme>> BuscarTodosFilmesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("BuscarTodosFilmes"); //realiza a requisição para a api e retorna o status/conteúdo
                response.EnsureSuccessStatusCode(); //verifica o status HTTP da resposta
               
                var filmes = await response.Content.ReadAsAsync<List<Filme>>(); //Converte os dados do JSON em um objeto
                return filmes;               
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao buscar filmes",ex);
            }
            catch(JsonException ex)
            {
                throw new Exception("Erro ao deserializar a resposta da API", ex);
            }
        }

    }
}
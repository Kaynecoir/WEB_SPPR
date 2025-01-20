using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.Services.GameService
{
    public class ApiGameService : IGameService
    {
        private HttpClient _httpClient;
        private string _pageSize;
        private ILogger<ApiGameService> _logger;
        private JsonSerializerOptions _serializerOptions;

        public ApiGameService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ApiGameService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
 
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<BoardGame>> CreateGameAsync(BoardGame product, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "BoardGame");

            var response = await _httpClient.PostAsJsonAsync(
            uri,
            product,
            _serializerOptions);
            if (response.IsSuccessStatusCode)
            {
                var data = await response
                .Content
                .ReadFromJsonAsync<ResponseData<BoardGame>>
                (_serializerOptions);

                return data; // boardgame;
            }
            _logger
            .LogError($"-----> object not created. Error:{ response.StatusCode.ToString()}");
            return ResponseData<BoardGame>.Error($"Объект не добавлен. Error:{ response.StatusCode.ToString()}");

        }

        public Task DeleteGameAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<BoardGame>> GetGameByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<GameListModel<BoardGame>>> GetGameListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // подготовка URL запроса
            var urlString
            = new
            StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}games/");
            // добавить категорию в маршрут
            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            };
            // добавить номер страницы в маршрут
            if (pageNo > 1)
            {
                urlString.Append($"page{pageNo}");
            };
            // добавить размер страницы в строку запроса
            if (!_pageSize.Equals("3"))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize));
            }

            // отправить запрос к API
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                    .Content
                    .ReadFromJsonAsync<ResponseData<GameListModel<BoardGame>>>
                    (_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<GameListModel<BoardGame>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error: { response.StatusCode.ToString()}");
            return ResponseData<GameListModel<BoardGame>>.Error($"Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
        }

        public Task UpdateGameAsync(int id, BoardGame product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}

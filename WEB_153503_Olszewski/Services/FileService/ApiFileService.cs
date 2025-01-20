
using Microsoft.AspNetCore.Http;
using WEB_153503_Olszewski.Domain.Entities;
using WEB_153503_Olszewski.Domain.Models;

namespace WEB_153503_Olszewski.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        
        public ApiFileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpContext = HttpContextAccessor.HttpContext;
        }

        public async Task DeleteFileAsync(string fileUri)
        {
            // путь к сохраняемому файлу
            var fileInfo = new FileInfo(fileUri);
            // если такой файл существует, удалить его
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // Создать объект запроса
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            // Сформировать случайное имя файла, сохранив расширение
            var extension = Path.GetExtension(file.FileName);
            var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
            // Создать контент, содержащий поток загруженного файла
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file.OpenReadStream());
            content.Add(streamContent, "file", newName);
            // Поместить контент в запрос
            request.Content = content;
            // Отправить запрос к API
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Вернуть полученный Url сохраненного файла
                return await response.Content.ReadAsStringAsync();
            }
            return String.Empty;
        }
    }
}

using System.Net.Http.Json;
using Client.Models;

namespace Client.Services;

public class UserProjectService
{
    private readonly HttpClient _httpClient;

    public UserProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserProject>> GetProjectsAsync()
    {
        try
        {
            var projects = await _httpClient.GetFromJsonAsync<List<UserProject>>("api/userproject");
            return projects ?? new List<UserProject>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching user projects: {ex.Message}");
            return new List<UserProject>();
        }
    }

    public async Task<UserProject?> GetProjectAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UserProject>($"api/userproject/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> AddProjectAsync(UserProject project)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/userproject", project);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateProjectAsync(UserProject project)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/userproject/{project.Id}", project);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/userproject/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Services;

public static class ChatService
{
    public static async Task<Guid> CreateChatAsync(
        HttpClient httpClient, ContactModel contactModel, CancellationToken cancellationToken)
    {
        var content = new StringContent(JsonConvert.SerializeObject(contactModel),
            Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/chat/create", content, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsAsync<Guid>(cancellationToken);
    }
}
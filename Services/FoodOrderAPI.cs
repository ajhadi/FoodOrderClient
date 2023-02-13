using System.Text;
using FoodOrderClient.Models;
using Newtonsoft.Json;

namespace FoodOrderClient.Services.FoodOrderAPI;

public class FoodOrderAPI : IFoodOrderAPI
{
    private readonly HttpClient client;
    private readonly ILogger<FoodOrderAPI> logger;

    public FoodOrderAPI(HttpClient client, ILogger<FoodOrderAPI> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public async Task<ServiceStatus<AuthToken>> GetTokenAsync(string username, string password)
    {
        try
        {
            var url = new Uri($"/api/auth/login", UriKind.Relative);

            var jsonContent = JsonConvert.SerializeObject(
                new
                {
                    username = username,
                    password = password
                }
            );
            var request = new HttpRequestMessage()
            {
                RequestUri = url,
                Method = HttpMethod.Post,
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };
            var send = await client.SendAsync(request);
            var content = await send
                .Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthToken>(content);

            if (send.IsSuccessStatusCode)
                return ServiceStatus.SuccessObjectResult<AuthToken>(result);
            return ServiceStatus.ErrorResult<AuthToken>(
                Error.Create(result.HttpStatusCode, result.Code, result.Message)
            );
        }
        catch (System.Exception e)
        {
            logger.LogError($"error : {e.Message}, {e.StackTrace}");
            return ServiceStatus.ErrorResult<AuthToken>();
        }

    }
    public async Task<ServiceStatus<List<TableDTO>>> GetTablesAsync(string accessToken)
    {
        try
        {
            var url = new Uri($"/api/table", UriKind.Relative);

            var request = new HttpRequestMessage()
            {
                RequestUri = url,
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            var send = await client.SendAsync(request);
            var content = await send
                .Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TableDTO>>(content);

            if (send.IsSuccessStatusCode)
                return ServiceStatus.SuccessObjectResult<List<TableDTO>>(result);
            return ServiceStatus.ErrorResult<List<TableDTO>>(
                Error.Create((int)send.StatusCode, 0, "Something went wrong")
            );
        }
        catch (System.Exception e)
        {
            logger.LogError($"error : {e.Message}, {e.StackTrace}");
            return ServiceStatus.ErrorResult<List<TableDTO>>();
        }
    }
    public async Task<ServiceStatus<List<ItemDTO>>> GetItemsAsync(string accessToken)
    {
        try
        {
            var url = new Uri($"/api/item", UriKind.Relative);

            var request = new HttpRequestMessage()
            {
                RequestUri = url,
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            var send = await client.SendAsync(request);
            var content = await send
                .Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ItemDTO>>(content);

            if (send.IsSuccessStatusCode)
                return ServiceStatus.SuccessObjectResult<List<ItemDTO>>(result);
            return ServiceStatus.ErrorResult<List<ItemDTO>>(
                Error.Create((int)send.StatusCode, 0, "Something went wrong")
            );
        }
        catch (System.Exception e)
        {
            logger.LogError($"error : {e.Message}, {e.StackTrace}");
            return ServiceStatus.ErrorResult<List<ItemDTO>>();
        }
    }
}
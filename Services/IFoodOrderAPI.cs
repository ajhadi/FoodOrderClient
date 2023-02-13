using FoodOrderClient.Models;

namespace FoodOrderClient.Services.FoodOrderAPI;

public interface IFoodOrderAPI
{
    Task<ServiceStatus<AuthToken>> GetTokenAsync(string username, string password);
    Task<ServiceStatus<List<TableDTO>>> GetTablesAsync(string accessToken);
    Task<ServiceStatus<List<ItemDTO>>> GetItemsAsync(string accessToken);
}
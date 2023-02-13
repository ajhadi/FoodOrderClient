namespace FoodOrderClient.Models;

public class APIBaseModel
{
    public int HttpStatusCode { get; set; }
    public int Code { get; set; }
    public string Message { get; set; }
}
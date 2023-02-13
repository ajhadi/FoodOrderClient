namespace FoodOrderClient.Models;

public class ItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public bool IsReady { get; set; }
    public ItemType Type { get; set; }
}
public enum ItemType
{
    Food,
    Beverage
}
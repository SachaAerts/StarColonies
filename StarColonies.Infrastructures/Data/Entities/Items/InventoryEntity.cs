namespace StarColonies.Infrastructures.Data.Entities.Items;

public class InventoryEntity
{
    public string ColonistId { get; set; } 
    public ColonistEntity Colonist { get; set; } 

    public int ItemId { get; set; }
    public ItemEntity Item { get; set; }
}
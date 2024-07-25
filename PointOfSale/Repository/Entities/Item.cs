namespace PointOfSaleApp.Entities;

public class Item
{
    public int ItemID {get; set;}
    public string ItemName {get; set;}
    public double ItemPrice {get; set;}

    public ICollection<Purchase> Purchases {get;set;}

}
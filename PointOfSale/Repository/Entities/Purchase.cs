using PointOfSaleApp.Entities;

public class Purchase 
{
    public int PurchaseID{get; set;}
    public int ReceiptID{get; set;}

    public int ItemID{get; set;}

    public int ItemQuantity{get; set;}

    // public ICollection<Item> Items {get; set;}
    // public ICollection<Receipt> Receipts {get; set;}

}
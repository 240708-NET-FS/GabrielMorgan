
using System.Security.Cryptography.X509Certificates;
using Azure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using PointOfSaleApp.Entities;
using PointOfSaleApp.Service;

public class ItemQuantity
{
    public Item Item { get; set; }
    public int Quantity { get; set; }
}
public class Entry
{



    ItemService itemService;
    PurchaseService purchaseService;
    ReceiptService receiptService;

    Receipt currentReceipt;
    List<Item> availableItems;
    public List<ItemQuantity> CheckOut;


    double total;

    public Entry(ItemService iService,
                    PurchaseService pService,
                    ReceiptService rService)
    {
        currentReceipt = new Receipt();
        CheckOut = new List<ItemQuantity>();
        itemService = iService;
        purchaseService = pService;
        receiptService = rService;
        availableItems = (List<Item>?)itemService.GetAll();
        InputItems();
    }

    public void PrintAvailable()
    {
        foreach (Item i in availableItems)
        {
            Console.WriteLine(i.ItemName);
        }
    }

   public List<Item> getAvailable(){
        return availableItems;
    }

    public void CalcTotal()
    {
        this.total = 0;
        foreach (ItemQuantity itemQuantity in CheckOut)
        {
            total += Math.Round(itemQuantity.Item.ItemPrice * itemQuantity.Quantity, 2);
        }
    }

    public double GetTotal()
    {
        return Math.Round(this.total, 2);
    }

    void printCheckoutList()
    {
        Console.Clear();

        Console.WriteLine("Item_Name" + '\t' + "Price" + '\t' + "Quantity");
        foreach (ItemQuantity itemQ in CheckOut)
        {
            Console.WriteLine(itemQ.Item.ItemName + '\t' + itemQ.Item.ItemPrice + '\t' + itemQ.Quantity);
        }

        Console.WriteLine("total: " + GetTotal());
    }

    void completeCheckout(){
        createReceipt();

        foreach(ItemQuantity i in CheckOut){
            Purchase p = new Purchase
            {
                ReceiptID = currentReceipt.ReceiptID,
                ItemID = i.Item.ItemID,
                ItemQuantity = i.Quantity
            };
            purchaseService.Create(p);
        }
    }

    void createReceipt(){
        this.currentReceipt.Total = GetTotal();
        this.currentReceipt.PurchaseDate = DateTime.Now;
        this.currentReceipt = receiptService.CreateAndGetReceipt(this.currentReceipt);
    }

    Item? SearchForItem(string term)
    {
        foreach (Item i in availableItems)
        {
            if (i.ItemName.ToLower().Trim(' ').Contains(term.ToLower().Trim(' ')))
            {
                return i;
            }
        }

        return null;
    }

    public void InputItems()
    {

        bool done = false;

        while (!done)
        {

            Console.Write("Add Item to Checkout: ");
            
            string search = Console.ReadLine();

            if (search.ToLower().Contains("!q"))
            {
                done = true;
            } else 

            if (search.ToLower().Contains("!c"))
            {
                completeCheckout();
                done = true;
            } else 
            if (search.ToLower().Contains("!i")){
                PrintAvailable();
            }
            else
            {
                
                Item found = SearchForItem(search);
                if (found != null)
                {
                    Console.Write("Select Quantity: ");
                    string? inputAmount = Console.ReadLine();
                    bool parseSuccess = int.TryParse(inputAmount, out int selectedNumber);

                    if (parseSuccess)
                    {
                        CheckOut.Add(new ItemQuantity { Item = found, Quantity = selectedNumber });
                        CalcTotal();
                        printCheckoutList();
                    } else {
                        Console.WriteLine("Improper Quantity. Try Again.");
                    }
                }

                else{
                    Console.WriteLine("Item not found. Try again.");
                }
            }
        }

    }







}
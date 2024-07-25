
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
    DateTime currentTime;
    List<Item> availableItems;
    List<ItemQuantity> checkOut;



 




    double total;

    public Entry(ItemService iService,
                    PurchaseService pService,
                    ReceiptService rService)
    {
        currentTime = new DateTime();
        currentReceipt = new Receipt();
        checkOut = new List<ItemQuantity>();
        itemService = iService;
        purchaseService = pService;
        receiptService = rService;
        availableItems = (List<Item>?)itemService.GetAll();
        inputItems();
    }

    void printAvailable()
    {
        foreach (Item i in availableItems)
        {
            Console.WriteLine(i.ItemName);
        }
    }

    void CalcTotal()
    {
        this.total = 0;
        foreach (ItemQuantity itemQuantity in checkOut)
        {
            total += Math.Round(itemQuantity.Item.ItemPrice * itemQuantity.Quantity, 2);
        }
    }

    double getTotal()
    {
        return Math.Round(this.total, 2);
    }

    void printCheckoutList()
    {
        Console.Clear();

        Console.WriteLine("Item_Name" + '\t' + "Price" + '\t' + "Quantity");
        foreach (ItemQuantity itemQ in checkOut)
        {
            Console.WriteLine(itemQ.Item.ItemName + '\t' + itemQ.Item.ItemPrice + '\t' + itemQ.Quantity);
        }

        Console.WriteLine("total: " + getTotal());
    }

    void completeCheckout(){
        createReceipt();

        foreach(ItemQuantity i in checkOut){
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
        this.currentReceipt.Total = getTotal();
        this.currentReceipt.PurchaseDate = DateTime.Now;
        this.currentReceipt = receiptService.CreateAndGetReceipt(this.currentReceipt);
    }

    Item? SearchForItem(string term)
    {
        foreach (Item i in availableItems)
        {
            if (i.ItemName.ToLower().Contains(term.ToLower()))
            {
                return i;
            }
        }

        return null;
    }

    void inputItems()
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
                printAvailable();
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
                        checkOut.Add(new ItemQuantity { Item = found, Quantity = selectedNumber });
                        CalcTotal();
                        printCheckoutList();
                    }
                }

                else{
                    Console.WriteLine("Item not found. Try again.");
                }
            }
        }

    }







}
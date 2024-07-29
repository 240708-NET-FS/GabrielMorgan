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
        Console.Clear();
        printInstructions();
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
        int bottomLine = Console.WindowHeight - 1;

        Console.WriteLine("Item Name".PadRight(20) + '\t' + "Price" + '\t' + "Quantity");
        foreach (ItemQuantity itemQ in CheckOut)
        {
            Console.WriteLine(itemQ.Item.ItemName.PadRight(20) + '\t' + itemQ.Item.ItemPrice + '\t' + itemQ.Quantity);
        }
        Console.SetCursorPosition(29, bottomLine -1);
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

            Console.Clear();
            Console.WriteLine("Purchase Complete. Press any key (Not Enter) to continue");
            Console.ReadKey();
            Console.Clear();
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

    void printInstructions(){
        Console.WriteLine("Entry Mode Instructions");
        Console.WriteLine("!i for inventory");
        Console.WriteLine("!c to complete checkout");
        Console.WriteLine("!q to quit to home.");

        Console.WriteLine("Otherwise, type an item name to enter it into the cart.");
    }

    public void InputItems()
    {

        int bottomLine = Console.WindowHeight - 1;

        bool done = false;

        while (!done)
        {

            Console.SetCursorPosition(0, bottomLine);
            Console.Write("(Entry) ");
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
                    Console.SetCursorPosition(0, bottomLine);
                    Console.Write("(Entry) ");
                    Console.Write("Select Quantity: ");
                    string? inputAmount = Console.ReadLine();
                    bool parseSuccess = int.TryParse(inputAmount, out int selectedNumber);

                    if (parseSuccess)
                    {
                        CheckOut.Add(new ItemQuantity { Item = found, Quantity = selectedNumber });
                        CalcTotal();
                        printCheckoutList();
                    } else {
                        Console.Write("(Entry) ");
                        Console.WriteLine("Improper Quantity. Try Again.");
                    }
                }

                else{
                    Console.Write("(Entry) ");
                    Console.WriteLine("Item not found. Try again.");
                }
            }
        }

    }







}
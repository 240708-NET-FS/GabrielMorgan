using PointOfSaleApp.Service;

namespace PointOfSaleApp;
public class Home
{
    ItemService itemService;
    PurchaseService purchaseService;

    ReceiptService receiptService;

    public Home(ItemService itemService, PurchaseService purchaseService, ReceiptService receiptService)
    {
        this.itemService = itemService;
        this.purchaseService = purchaseService;
        this.receiptService = receiptService;

    }

    public void Show()
    {
        navigate();
    }

    void printLogo()
    {
        foreach (string line in File.ReadAllLines("assets/logo.txt"))
        {
            Console.WriteLine(line);
        }

    }

    void printOptions()
    {
        string[] options = { "[1] Entry Mode", "[2] Quit"};
        foreach (string option in options)
        {
            Console.WriteLine(option);
        }


    }

    void navigate()
    {

        printLogo();
        printOptions();

        Console.Write("(Home) ");
        string? response = Console.ReadLine();
        bool parseSuccess = int.TryParse(response, out int selectedNumber);
        if (parseSuccess && selectedNumber >= 1 && selectedNumber <= 2)
        {

            if (selectedNumber == 1)
            {
                Entry entry = new Entry(itemService, purchaseService, receiptService);
            }

            if (selectedNumber == 2)
            {
                return;
            }
        }

       
        navigate();
    }

}


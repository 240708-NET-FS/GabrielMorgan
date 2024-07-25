using System.Transactions;
using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;
using PointOfSaleApp.Service;

namespace PointOfSaleApp;


public class App{
    public static void Main(string[] args){

        using (var context = new ApplicationDbContext()){

            ItemDAO itemDao = new ItemDAO(context);
            ItemService itemService = new ItemService(itemDao);

            ReceiptDAO receiptDAO = new ReceiptDAO(context);
            ReceiptService receiptService = new ReceiptService(receiptDAO);

            PurchaseDAO purchaseDAO = new PurchaseDAO(context);
            PurchaseService purchaseService = new PurchaseService(purchaseDAO);

            Home homeController = new Home(itemService, purchaseService, receiptService);
            State.isActive = true;

            // List<Item> items = (List<Item>)itemService.GetAll();

            // foreach(Item i in items){
            //     Console.WriteLine(i.ItemName);
            // }

            homeController.Show();
        }
       
    }
}

using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;
using PointOfSaleApp.Service;
using System.Threading.Tasks;



namespace PointOfSale.Tests;
public class PurchaseServiceTests
{
    private DbContextOptions<ApplicationDbContext> GetInMemoryDbContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestPurchaseDatabase")
            .Options;
    }



       public void Test_AddPurchaseToDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted(); 
            var purchaseDAO = new PurchaseDAO(context);
            var pService = new PurchaseService(purchaseDAO);
            var purchase = new Purchase { ReceiptID = 1, ItemID = 101, ItemQuantity = 2 };

            pService.Create(purchase);
        }

        using (var context = new ApplicationDbContext(options))
        {
            Assert.Equal(1, context.Purchases.Count());
            var retrievedPurchase = context.Purchases.First();
            Assert.Equal(1, retrievedPurchase.ReceiptID);
            Assert.Equal(101, retrievedPurchase.ItemID);
            Assert.Equal(2, retrievedPurchase.ItemQuantity);
        }
    }


    [Fact]
    public void Test_RemovePurchaseFromDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); 
            var purchaseDAO = new PurchaseDAO(context);
            var pService = new PurchaseService(purchaseDAO);
            var purchase = new Purchase { ReceiptID = 1, ItemID = 101, ItemQuantity = 2 };
            pService.Create(purchase);

            pService.Delete(purchase);
        }

        using (var context = new ApplicationDbContext(options))
        {
            Assert.Empty(context.Purchases);
        }
    }

    [Fact]
    public  void Test_ReturnAllPurchases()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var purchaseDAO = new PurchaseDAO(context);
            var pService = new PurchaseService(purchaseDAO);
            var purchases = new[]
            {
                new Purchase { ReceiptID = 1, ItemID = 101, ItemQuantity = 2 },
                new Purchase { ReceiptID = 2, ItemID = 102, ItemQuantity = 1 }
            };
            foreach (var purchase in purchases)
            {
                pService.Create(purchase);
            }
            var allPurchases = pService.GetAll();

            Assert.Equal(2, allPurchases.Count);
        }
    }

    [Fact]
    public void Test_ReturnOnePurchase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var purchaseDAO = new PurchaseDAO(context);
            var pService = new PurchaseService(purchaseDAO);
            var purchase = new Purchase { ReceiptID = 1, ItemID = 101, ItemQuantity = 2 };
            pService.Create(purchase);

            var retrievedPurchase = pService.getById(purchase.PurchaseID);

            Assert.Equal(1, retrievedPurchase.ReceiptID);
            Assert.Equal(101, retrievedPurchase.ItemID);
            Assert.Equal(2, retrievedPurchase.ItemQuantity);
        }
    }

    [Fact]
    public void Test_ModifyPurchaseInDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var purchaseDAO = new PurchaseDAO(context);
            var pService = new PurchaseService(purchaseDAO);
            var purchase = new Purchase { ReceiptID = 1, ItemID = 101, ItemQuantity = 2 };
            pService.Create(purchase);

            purchase.ItemQuantity = 5;
            pService.Update(purchase);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var updatedPurchase = context.Purchases.First();
            Assert.Equal(5, updatedPurchase.ItemQuantity);
        }
    }
}
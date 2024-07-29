using Microsoft.EntityFrameworkCore;
using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;
using PointOfSaleApp.Service;



namespace PointOfSale.Tests;
public class ReceiptServiceTests
{
    private DbContextOptions<ApplicationDbContext> GetInMemoryDbContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestReceiptDatabase")
            .Options;
    }

    [Fact]
    public void Test_AddReceiptToDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipt = new Receipt { PurchaseDate = DateTime.Now, Total = 50.75 };

            rService.Create(receipt);
        }

        using (var context = new ApplicationDbContext(options))
        {
            Assert.Equal(1, context.Receipts.Count());
            var retrievedReceipt = context.Receipts.First();
            Assert.Equal(50.75, retrievedReceipt.Total);
        }
    }

    [Fact]
    public async void Test_AddReceiptAndReturnReceipt()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipt = new Receipt { PurchaseDate = DateTime.Now, Total = 20.00 };

            var createdReceipt = rService.CreateAndGetReceipt(receipt);
            
            await Task.Delay(1000);
            Assert.NotNull(createdReceipt);
            Assert.Equal(receipt.Total, createdReceipt.Total);
        }
    }

    [Fact]
    public async void Test_RemoveReceiptFromDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipt = new Receipt { PurchaseDate = DateTime.Now, Total = 15.99 };
            rService.Create(receipt);

            rService.Delete(receipt);
        }

        using (var context = new ApplicationDbContext(options))
        {
            await Task.Delay(1000);
            Assert.Empty(context.Receipts);
        }
    }

    [Fact]
    public async void Test_ReturnAllReceipts()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipts = new[]
            {
                new Receipt { PurchaseDate = DateTime.Now, Total = 10.00 },
                new Receipt { PurchaseDate = DateTime.Now, Total = 25.50 }
            };
            foreach (var receipt in receipts)
            {
                rService.Create(receipt);
            }
            var allReceipts = rService.GetAll();

            await Task.Delay(1000);
            Assert.Equal(2, allReceipts.Count);
        }
    }

    [Fact]
    public async void Test_ReturnOneReceipt()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipt = new Receipt { PurchaseDate = DateTime.Now, Total = 30.99 };
            rService.Create(receipt);

            var retrievedReceipt = rService.getById(receipt.ReceiptID);

            await Task.Delay(1000);
            Assert.Equal(receipt.Total, retrievedReceipt.Total);
        }
    }

    [Fact]
    public async void Test_ModifyReceiptInDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var receiptDAO = new ReceiptDAO(context);
            var rService = new ReceiptService(receiptDAO);
            var receipt = new Receipt { PurchaseDate = DateTime.Now, Total = 40.00 };
            rService.Create(receipt);

            receipt.Total = 45.00;
            rService.Update(receipt);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var updatedReceipt = context.Receipts.First();
            await Task.Delay(1000);
            Assert.Equal(45.00, updatedReceipt.Total);
        }
    }
}
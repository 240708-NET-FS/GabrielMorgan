using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;
using PointOfSaleApp.Service;
using Microsoft.EntityFrameworkCore;

namespace PointOfSale.Tests;
public class ItemServiceTests
{


    private DbContextOptions<ApplicationDbContext> GetInMemoryDbContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestItemDatabase")
            .Options;
    }

    [Fact]
    public void Test_AddItemToDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); 
            var itemDAO = new ItemDAO(context);
            var iService = new ItemService(itemDAO);
            var item = new Item { ItemName = "Mango", ItemPrice = 12.99 };

            iService.Create(item);
        }

        using (var context = new ApplicationDbContext(options))
        {

            Assert.Equal(1, context.Items.Count());
            var retrievedItem = context.Items.First();
            Assert.Equal("Mango", retrievedItem.ItemName);
            Assert.Equal(12.99, retrievedItem.ItemPrice);
        }
    }

    [Fact]
    public void Test_RemoveItemFromDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated(); 
            var itemDAO = new ItemDAO(context);
            var iService = new ItemService(itemDAO);
            var item = new Item { ItemName = "Carrot", ItemPrice = 2.49 };
            iService.Create(item);

            iService.Delete(item);
        }

        using (var context = new ApplicationDbContext(options))
        {
          
            Assert.Empty(context.Items);
        }
    }

    [Fact]
    public  void Test_ReturnAllItems()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var itemDAO = new ItemDAO(context);
            var iService = new ItemService(itemDAO);
            var items = new[]
            {
                new Item { ItemName = "Grapes", ItemPrice = 5.99 },
                new Item { ItemName = "Noodles", ItemPrice = 7.89 }
            };
            foreach (var item in items)
            {
                iService.Create(item);
            }
            var allItems = iService.GetAll();
            Assert.Equal(2, allItems.Count);
        }
    }

    [Fact]
    public void Test_ReturnOneItem()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var itemDAO = new ItemDAO(context);
            var iService = new ItemService(itemDAO);
            var item = new Item { ItemName = "Cereal", ItemPrice = 3.99 };
            iService.Create(item);

            var retrievedItem = iService.getById(item.ItemID);

            Assert.Equal("Cereal", retrievedItem.ItemName);
            Assert.Equal(3.99, retrievedItem.ItemPrice);
        }
    }

    [Fact]
    public void Test_ModifyItemInDatabase()
    {
        var options = GetInMemoryDbContextOptions();

        using (var context = new ApplicationDbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var itemDAO = new ItemDAO(context);
            var iService = new ItemService(itemDAO);
            var item = new Item { ItemName = "Soda", ItemPrice = 1.99 };
            iService.Create(item);

            item.ItemName = "Soda";
            item.ItemPrice = 2.49;
            iService.Update(item);
        }

        using (var context = new ApplicationDbContext(options))
        {
            var updatedItem = context.Items.First();
            Assert.Equal("Soda", updatedItem.ItemName);
            Assert.Equal(2.49, updatedItem.ItemPrice);
        }
    }
}
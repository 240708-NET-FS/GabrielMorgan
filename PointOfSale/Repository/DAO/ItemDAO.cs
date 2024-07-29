using PointOfSaleApp.Entities;

namespace PointOfSaleApp.DAO;

public class ItemDAO : IDAO<Item>
{
    private ApplicationDbContext context;

    public ItemDAO(ApplicationDbContext context)
    {
        this.context = context;
    }

    public ItemDAO(){}

    public void Create(Item entry)
    {
        context.Items.Add(entry);
        context.SaveChanges();
    }

    public void Delete(Item entry)
    {
        context.Items.Remove(entry);
        context.SaveChanges();
    }

    public ICollection<Item> GetAll()
    {
        List<Item> items = context.Items.ToList();
        return items;
    }

    public Item GetById(int ID)
    {
        Item item = context.Items.FirstOrDefault(i => i.ItemID == ID);
        return item;
    }

    public void Update(Item newEntry)
    {
        Item originalItem = context.Items.FirstOrDefault(u => u.ItemID == newEntry.ItemID);

        if (originalItem != null)
        {
            originalItem.ItemName = newEntry.ItemName;
            originalItem.ItemPrice = newEntry.ItemPrice;

            context.Items.Update(originalItem);
            context.SaveChanges();
        }

    }
}
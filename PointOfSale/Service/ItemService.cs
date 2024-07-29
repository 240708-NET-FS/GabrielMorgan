using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;

namespace PointOfSaleApp.Service;

public class ItemService : IService<Item>
{
    private readonly ItemDAO itemDAO;

    public ItemService(ItemDAO itemDAO)
    {
        this.itemDAO = itemDAO;
    }

      public ItemService(){}

    public void Create(Item item)
    {
       itemDAO.Create(item);
    }

    public void Delete(Item item)
    {
        itemDAO.Delete(item);
    }

    public virtual ICollection<Item> GetAll()
    {
       return itemDAO.GetAll();
    }

    public Item getById(int Id)
    {
        return itemDAO.GetById(Id);
    }

    public void Update(Item item)
    {
        itemDAO.Update(item);
    }
}
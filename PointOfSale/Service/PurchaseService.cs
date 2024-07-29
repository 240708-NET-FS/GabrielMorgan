using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;

namespace PointOfSaleApp.Service;

public class PurchaseService : IService<Purchase>
{
    private readonly PurchaseDAO purchaseDAO;

    public PurchaseService(PurchaseDAO purchaseDAO)
    {
        this.purchaseDAO = purchaseDAO;
    }

    public PurchaseService(){}

    public void Create(Purchase purchase)
    {
       purchaseDAO.Create(purchase);
    }

    public void Delete(Purchase purchase)
    {
        purchaseDAO.Delete(purchase);
    }

    public ICollection<Purchase> GetAll()
    {
       return purchaseDAO.GetAll();
    }

    public Purchase getById(int Id)
    {
        return purchaseDAO.GetById(Id);
    }

    public void Update(Purchase purchase)
    {
        purchaseDAO.Update(purchase);
    }
}
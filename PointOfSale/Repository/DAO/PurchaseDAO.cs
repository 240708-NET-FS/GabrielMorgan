
using PointOfSaleApp.Entities;

namespace PointOfSaleApp.DAO;

public class PurchaseDAO : IDAO<Purchase>
{

    private ApplicationDbContext context;

    public PurchaseDAO(ApplicationDbContext context)
    {
        this.context = context;
    }

    public PurchaseDAO(){}
    public void Create(Purchase entry)
    {
        context.Purchases.Add(entry);
        context.SaveChanges();
    }

    public void Delete(Purchase entry)
    {
        context.Purchases.Remove(entry);
        context.SaveChanges();
    }

    public ICollection<Purchase> GetAll()
    {
        List<Purchase> purchases = context.Purchases.ToList();
       return purchases;
    }

    public Purchase GetById(int ID)
    {
        Purchase purchase = context.Purchases.FirstOrDefault(r => r.PurchaseID == ID);
        return purchase;
    }

    public void Update(Purchase newpurchase)
    {
        Purchase originalpurchase = context.Purchases.FirstOrDefault(u => u.ReceiptID == newpurchase.PurchaseID);

        if (originalpurchase != null)
        {
            originalpurchase.ItemID = newpurchase.ItemID;
            originalpurchase.ReceiptID = newpurchase.ReceiptID;
            context.Purchases.Update(originalpurchase);
            context.SaveChanges();
        }
    }
}
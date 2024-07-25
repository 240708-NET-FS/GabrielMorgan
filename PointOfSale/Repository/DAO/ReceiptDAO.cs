
using PointOfSaleApp.Entities;

namespace PointOfSaleApp.DAO;

public class ReceiptDAO : IDAO<Receipt>
{

    private ApplicationDbContext context;

    public ReceiptDAO(ApplicationDbContext context)
    {
        this.context = context;
    }
    public void Create(Receipt receipt)
    {
        context.Receipts.Add(receipt);
        context.SaveChanges();
    }

    public Receipt CreateAndGetReceipt(Receipt receipt){
        context.Receipts.Add(receipt);
        context.SaveChanges();
        return receipt;
    }

    public void Delete(Receipt receipt)
    {
       context.Receipts.Remove(receipt);
       context.SaveChanges();
    }

    public ICollection<Receipt> GetAll()
    {
       List<Receipt> receipts = context.Receipts.ToList();
       return receipts;
    }

    public Receipt GetById(int ID)
    {
        Receipt receipt = context.Receipts.FirstOrDefault(r => r.ReceiptID== ID);
        return receipt;
    }

    public void Update(Receipt newReceipt)
    {
         Receipt originalReceipt = context.Receipts.FirstOrDefault(u => u.ReceiptID == newReceipt.ReceiptID);

        if (originalReceipt != null)
        {
            originalReceipt.PurchaseDate = newReceipt.PurchaseDate;
            originalReceipt.Total = newReceipt.Total;
            context.Receipts.Update(originalReceipt);
            context.SaveChanges();
        }
    }
}
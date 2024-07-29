using PointOfSaleApp.DAO;
using PointOfSaleApp.Entities;

namespace PointOfSaleApp.Service;


public class ReceiptService : IService<Receipt>
{
    private readonly ReceiptDAO ReceiptDAO;

    public ReceiptService(ReceiptDAO ReceiptDAO)
    {
        this.ReceiptDAO = ReceiptDAO;
    }

    public ReceiptService(){}

    public void Create(Receipt Receipt)
    {
       ReceiptDAO.Create(Receipt);
    }

    public Receipt CreateAndGetReceipt(Receipt Receipt){
        ReceiptDAO.CreateAndGetReceipt(Receipt);

        return Receipt;
    }

    public void Delete(Receipt Receipt)
    {
        ReceiptDAO.Delete(Receipt);
    }

    public ICollection<Receipt> GetAll()
    {
       return ReceiptDAO.GetAll();
    }

    public Receipt getById(int Id)
    {
        return ReceiptDAO.GetById(Id);
    }

    public void Update(Receipt Receipt)
    {
        ReceiptDAO.Update(Receipt);
    }
}
namespace PointOfSaleApp.Service;
public interface IService <T>
{
    public T getById(int Id);
    public ICollection<T> GetAll();
    public void Create(T item);

    public void Delete(T item);

    public void Update(T item);
}
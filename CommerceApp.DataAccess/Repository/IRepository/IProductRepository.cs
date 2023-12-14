using CommerceApp.Models;
namespace CommerceApp.DataAccess;

public interface IProductRepository:IRepository<Product>
{
       void Update(Product Product);

}


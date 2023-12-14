using CommerceApp.Models;
using CommerceApp.DataAccess.Repository.IRepository;
namespace CommerceApp.DataAccess;

public class ProductRepository:Repository<Product>,IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db):base(db)
    {
        _db=db;
    }
    public void Update(Product Product)
    {
        _db.Products.Update(Product);
    }
}

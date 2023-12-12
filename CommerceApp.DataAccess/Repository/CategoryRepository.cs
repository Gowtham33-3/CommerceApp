using System.Linq.Expressions;
using CommerceApp.DataAccess.Repository.IRepository;
using CommerceApp.Models;

namespace CommerceApp.DataAccess;

public class CategoryRepository : Repository<Category>,ICategoryRepository
{
  private readonly ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db): base(db)
    {
        _db=db;
    }
    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }
}

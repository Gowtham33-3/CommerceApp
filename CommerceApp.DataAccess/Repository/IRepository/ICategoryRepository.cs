using CommerceApp.Models;

namespace CommerceApp.DataAccess;

public interface ICategoryRepository:IRepository<Category>
{
   void Update(Category category);
}

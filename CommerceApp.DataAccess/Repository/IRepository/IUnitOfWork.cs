namespace CommerceApp.DataAccess;

public interface IUnitOfWork
{
 ICategoryRepository categoryRepository{get;}
 void Save();
}

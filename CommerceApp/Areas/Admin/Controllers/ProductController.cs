using CommerceApp.DataAccess;
using CommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceApp.Areas.Admin.Namespace
{
 [Area("Admin")]
public class ProductController: Controller
{
    private readonly IUnitOfWork _UnitOfWork;
    public ProductController(IUnitOfWork UnitOfWork)
    {
        _UnitOfWork=UnitOfWork;
    }
     public IActionResult Index()
        {
            List<Product>ObjProductList = _UnitOfWork.Product.GetAll().ToList();
            return View(ObjProductList);
        }
         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
         public IActionResult Create(Product obj)
        {
            if(ModelState.IsValid){
               _UnitOfWork.Product.Add(obj);
               _UnitOfWork.Save();
               TempData["Success"]="Product Created successfully";
               return RedirectToAction("Index","Product");
            }
           
            return View();
        }
          public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

          Product? FetchedCategory = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          return View(FetchedCategory);
        }
        [HttpPost]
         public IActionResult Edit(Product obj)
        {
            if(ModelState.IsValid){
                _UnitOfWork.Product.Update(obj);
               _UnitOfWork.Save();
               TempData["Success"]="Product Updated successfully";
                return RedirectToAction("Index","Product");
            }
            return View();
        }
          public IActionResult Delete(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

          Product? FetchedCategory = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          return View(FetchedCategory);
        }
        [HttpPost,ActionName("Delete")]
         public IActionResult DeletePost(int? id)
        {
             if(id==null || id==0){
                return NotFound();
            }

          Product? FetchedCategory = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          _UnitOfWork.Product.Remove(FetchedCategory);
          _UnitOfWork.Save();
          TempData["Success"]="Product Deleted successfully";
          return RedirectToAction("Index","Product") ;
        }


}

}
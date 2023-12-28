using CommerceApp.DataAccess;
using CommerceApp.Models;
using CommerceApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommerceApp.Areas.Admin.Namespace
{
 [Area("Admin")]
public class ProductController: Controller
{
    private readonly IUnitOfWork _UnitOfWork ;
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
           IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem{
                  Text=u.Name,
                  Value=u.Id.ToString()
            });
             ProductVM ProductVM =new () {
               Categorylist = CategoryList,
                Product = new Product()
             };
            return View(ProductVM);
        }
        [HttpPost]
         public IActionResult Create(ProductVM ProductVM)
        {
            if(ModelState.IsValid){
               _UnitOfWork.Product.Add(ProductVM.Product);
               _UnitOfWork.Save();
               TempData["Success"]="Product Created successfully";
               return RedirectToAction("Index","Product");
            }else{
                ProductVM.Categorylist = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem{
                  Text=u.Name,
                  Value=u.Id.ToString()
            });
             return View(ProductVM);
            }
          
        }
          public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

          Product? FetchedProduct = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedProduct==null){
            return NotFound();
          }
          return View(FetchedProduct);
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

          Product? FetchedProduct = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedProduct==null){
            return NotFound();
          }
          return View(FetchedProduct);
        }
        [HttpPost,ActionName("Delete")]
         public IActionResult DeletePost(int? id)
        {
             if(id==null || id==0){
                return NotFound();
            }

          Product? FetchedProduct = _UnitOfWork.Product.Get(u=>u.Id==id);
          if(FetchedProduct==null){
            return NotFound();
          }
          _UnitOfWork.Product.Remove(FetchedProduct);
          _UnitOfWork.Save();
          TempData["Success"]="Product Deleted successfully";
          return RedirectToAction("Index","Product") ;
        }


}

}
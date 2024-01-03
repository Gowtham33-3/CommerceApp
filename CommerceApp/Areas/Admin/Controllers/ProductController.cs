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
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork UnitOfWork,IWebHostEnvironment webHostEnvironment)
    {
        _UnitOfWork=UnitOfWork;
        _webHostEnvironment=webHostEnvironment;
    }
     public IActionResult Index()
        {
            List<Product>ObjProductList = _UnitOfWork.Product.GetAll().ToList();
            return View(ObjProductList);
        }
         public IActionResult Upsert(int? Id)
        {
           IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem{
                  Text=u.Name,
                  Value=u.Id.ToString()
            });
             ProductVM ProductVM =new () {
               Categorylist = CategoryList,
                Product = new Product()
             };
             if(Id==null || Id==0){
               //create
                  return View(ProductVM);
             }else{
              ///update
               ProductVM.Product= _UnitOfWork.Product.Get(u=>u.Id==Id);
               return View(ProductVM);
             }
           
        }
        [HttpPost]
         public IActionResult Upsert(ProductVM ProductVM,IFormFile? file)
        {
            if(ModelState.IsValid){
              
               string wwwRootPath = _webHostEnvironment.WebRootPath;
              if(file != null){
              string fileName= Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
              string productPath = Path.Combine(wwwRootPath,@"images\product");
              if(!string.IsNullOrEmpty(ProductVM.Product.ImageURL))
              {
                var oldImagePath= Path.Combine(wwwRootPath,ProductVM.Product.ImageURL.TrimStart('\\'));

                if(System.IO.File.Exists(oldImagePath)){
                  System.IO.File.Delete(oldImagePath);
                }

              }
              using(var fileStream = new  FileStream(Path.Combine(productPath,fileName),FileMode.Create))
              {
                  file.CopyTo(fileStream);
              }
               ProductVM.Product.ImageURL=@"\images\product\"+fileName;
              }
            
               if(ProductVM.Product.Id==0){
                  _UnitOfWork.Product.Add(ProductVM.Product);
               }else{
                   _UnitOfWork.Product.Update(ProductVM.Product);
               }
              
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
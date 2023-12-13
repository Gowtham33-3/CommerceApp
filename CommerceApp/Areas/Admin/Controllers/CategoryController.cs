using CommerceApp.DataAccess;
using CommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceApp.Areas.Admin.Namespace
{
     [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork  _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork=UnitOfWork;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            List<Category>ObjCategoryList = _UnitOfWork.Category.GetAll().ToList();
            return View(ObjCategoryList);
        }
         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
         public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid){
               _UnitOfWork.Category.Add(obj);
               _UnitOfWork.Save();
               TempData["Success"]="Category Created successfully";
               return RedirectToAction("Index","Category");
            }
           
            return View();
        }
          public IActionResult Edit(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

          Category? FetchedCategory = _UnitOfWork.Category.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          return View(FetchedCategory);
        }
        [HttpPost]
         public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid){
                _UnitOfWork.Category.Update(obj);
               _UnitOfWork.Save();
               TempData["Success"]="Category Updated successfully";
                return RedirectToAction("Index","Category");
            }
            return View();
        }
          public IActionResult Delete(int? id)
        {
            if(id==null || id==0){
                return NotFound();
            }

          Category? FetchedCategory = _UnitOfWork.Category.Get(u=>u.Id==id);
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

          Category? FetchedCategory = _UnitOfWork.Category.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          _UnitOfWork.Category.Remove(FetchedCategory);
          _UnitOfWork.Save();
          TempData["Success"]="Category Deleted successfully";
          return RedirectToAction("Index","Category") ;
        }

    }
}

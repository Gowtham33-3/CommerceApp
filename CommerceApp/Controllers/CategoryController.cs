using CommerceApp.DataAccess;
using CommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository  _CategoryRepository;
        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository=CategoryRepository;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            List<Category>ObjCategoryList = _CategoryRepository.GetAll().ToList();
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
               _CategoryRepository.Add(obj);
               _CategoryRepository.Save();
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

          Category? FetchedCategory = _CategoryRepository.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          return View(FetchedCategory);
        }
        [HttpPost]
         public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid){
                _CategoryRepository.Update(obj);
               _CategoryRepository.Save();
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

          Category? FetchedCategory = _CategoryRepository.Get(u=>u.Id==id);
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

          Category? FetchedCategory = _CategoryRepository.Get(u=>u.Id==id);
          if(FetchedCategory==null){
            return NotFound();
          }
          _CategoryRepository.Remove(FetchedCategory);
          _CategoryRepository.Save();
          TempData["Success"]="Category Deleted successfully";
          return RedirectToAction("Index","Category") ;
        }

    }
}

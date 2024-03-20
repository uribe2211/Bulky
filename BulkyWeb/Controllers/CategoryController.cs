using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;
    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public IActionResult Index()
    {
        List<Category> data = _categoryRepo.GetAll().ToList();
        return View(data);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"]="Category created succesfully";

                return RedirectToAction("Index", "Category");
            }

            return View();

        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = _categoryRepo.Get(u => u.Id==id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.Update(category);
            _categoryRepo.Save();
            TempData["success"] = "Category edited succesfully";

            return RedirectToAction("Index");
        }

        return View();
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var category = _categoryRepo.Get(u=>u.Id==id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost,ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var category = _categoryRepo.Get(u=>u.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        _categoryRepo.Remove(category);
        _categoryRepo.Save();

        TempData["success"] = "Category deleted succesfully";

        return RedirectToAction("Index");
    }
}

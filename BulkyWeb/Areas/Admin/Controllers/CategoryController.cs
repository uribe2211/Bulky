using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unit;
    public CategoryController(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public IActionResult Index()
    {
        List<Category> data = _unit.Category.GetAll().ToList();
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
                _unit.Category.Add(obj);
                _unit.Save();
                TempData["success"] = "Category created succesfully";

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

        var category = _unit.Category.Get(u => u.Id == id);

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
            _unit.Category.Update(category);
            _unit.Save();
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

        var category = _unit.Category.Get(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var category = _unit.Category.Get(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        _unit.Category.Remove(category);
        _unit.Save();

        TempData["success"] = "Category deleted succesfully";

        return RedirectToAction("Index");
    }
}

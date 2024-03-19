using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BulkyWeb.Controllers;

public class CategoryController(DbContextApp db) : Controller
{
    private readonly DbContextApp _db = db;

    public IActionResult Index()
    {
        List<Category> data = _db.Categories.ToList();
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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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

        var category = _db.Categories.FirstOrDefault(c => c.Id == id);

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
            _db.Categories.Update(category);
            _db.SaveChanges();
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

        var category = _db.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost,ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var category = _db.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(category);
        _db.SaveChanges();

        TempData["success"] = "Category deleted succesfully";

        return RedirectToAction("Index");
    }
}

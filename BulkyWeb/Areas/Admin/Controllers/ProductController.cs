using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> data = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(data);
    }
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };

        if (id == null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
            return View(productVM);
        }
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        try
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRoot, @"images\products");

                    if (!string.IsNullOrEmpty(productVM.Product.imageURL))
                    {
                        var oldImage = Path.Combine(wwwRoot, productVM.Product.imageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    };

                    productVM.Product.imageURL = @$"\images\products\{fileName}";
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index", "Product");
            }

            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }

            return View(productVM);

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

        var obj = _unitOfWork.Product.Get(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product edited succesfully";

            return RedirectToAction("Index");
        }

        return View();
    }
    //[HttpGet]
    //public IActionResult Delete(int? id)
    //{
    //    if (id == null || id == 0)
    //    {
    //        return NotFound();
    //    }

    //    var obj = _unitOfWork.Product.Get(u => u.Id == id);

    //    if (obj == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(obj);
    //}

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.Product.Get(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();

        TempData["success"] = "Product deleted succesfully";

        return RedirectToAction("Index");
    }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> dataList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = dataList });
    }

    public IActionResult Delete(int? id)
    {
        var del = _unitOfWork.Product.Get(u => u.Id == id);

        if (del == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, del.imageURL.TrimStart('\\'));

        if (System.IO.File.Exists(oldImage))
        {
            System.IO.File.Delete(oldImage);
        }
        _unitOfWork.Product.Remove(del);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Succesfull" });
    }
    #endregion
}

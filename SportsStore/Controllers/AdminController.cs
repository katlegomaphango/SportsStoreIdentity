using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private IRepositoryWrapper _repository;

    public AdminController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        return View(_repository.Product.FindAll());
    }

    public IActionResult Edit(int productId)
    {
        var product = _repository.Product.GetById(productId);

        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Title = "Edit Product";
        PopulateCategoryDDL(product.CategoryID);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.Product.Update(product);
            _repository.Product.Save();
            TempData["Message"] = $"{product.Name} has been saved";
            return RedirectToAction("Index");
        }
        else
        {
            return View(product);
        }
    }

    private void PopulateCategoryDDL(object selectedCategory = null)
    {
        ViewBag.CategoryID = new SelectList(_repository.Category.FindAll(),
            "CategoryID", "CategoryName", selectedCategory);
    }

    public IActionResult Create()
    {
        PopulateCategoryDDL();
        ViewBag.Title = "Add Product";
        return View("Edit", new Product());
    }

    [HttpPost]
    public IActionResult Delete(int productId)
    {
        var product = _repository.Product.GetById(productId);

        if (product != null)
        {
            try
            {
                _repository.Product.Delete(product);
                _repository.Product.Save();
                TempData["Message"] = $"{product.Name} was deleted";
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = $"Unable to delete {product.Name}";
            }
        }
        return RedirectToAction("Index");
    }

}

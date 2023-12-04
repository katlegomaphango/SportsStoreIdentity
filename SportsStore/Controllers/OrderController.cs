using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IRepositoryWrapper _repository;
    private readonly Cart _cart;

    public OrderController(IRepositoryWrapper repository, Cart cartService)
    {
        _repository = repository;
        _cart = cartService;

    }

    [Authorize(Roles = "Customer")]
    public IActionResult Checkout()
    {
        return View();
    }

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (_cart.Lines.Count() == 0)
        {
            ModelState.AddModelError("", "Sorry, your cart is empty!");
        }
        if (ModelState.IsValid)
        {
            order.Lines = _cart.Lines.ToArray();
            _repository.Order.SaveOrder(order);
            var orderId = order.OrderID;
            _cart.Clear();
            return RedirectToPage("/Completed", new { orderId });
        }
        else
        {
            return View();
        }
    }

    [Authorize(Roles = "Administrator")]
    public IActionResult List()
    {
        return View(new OrderListViewModel
        {
            UnshippedOrders = _repository.Order.GetOrders().Where(o => !o.Shipped),
            ShippedOrders = _repository.Order.GetOrders().Where(o => o.Shipped)
        });
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public IActionResult MarkShipped(int orderID)
    {
        Order order = _repository.Order.GetById(orderID);
        if (order != null)
        {
            order.Shipped = true;
            _repository.Order.Save();
            TempData["Message"] = $"Order {orderID} has been shipped";
        }
        else
        {
            TempData["Message"] = $"Order {orderID} could not been shipped";
        }
        return RedirectToAction(nameof(List));
    }
}

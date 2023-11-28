namespace SportsStore.Models.ViewModels;

public class OrderListViewModel
{
    public IEnumerable<Order> UnshippedOrders { get; set; }

    public IEnumerable<Order> ShippedOrders { get; set; }
}

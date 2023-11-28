using SportsStore.Models;

namespace SportsStore.Data;

public interface IOrderRepository : IRepositoryBase<Order>
{
    IQueryable<Order> GetOrders();
    void SaveOrder(Order order);
}

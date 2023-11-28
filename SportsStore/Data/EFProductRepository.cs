using SportsStore.Models;
//using System.Linq.Expressions;

namespace SportsStore.Data;

public class EFProductRepository : RepositoryBase<Product>, IProductRepository
{
    public EFProductRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }

}

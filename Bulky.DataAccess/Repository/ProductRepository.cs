using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.Repository;

namespace Bulky.DataAccess.Repository;

public class ProductRepository :Repository<Product>,IProductRepository
{
    private readonly DbContextApp _db;

    public ProductRepository(DbContextApp db):base(db)
    {
        _db = db;
    }

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}

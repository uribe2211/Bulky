using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.Repository;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {
        private readonly DbContextApp _db;

        public CategoryRepository(DbContextApp db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

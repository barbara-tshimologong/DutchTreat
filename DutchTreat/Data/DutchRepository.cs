using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _context.Orders
                    .ToList();
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Products
                .OrderBy(p => p.Title)
                .ToList();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
            
        }
        public Order GetOrderById(int id)
        {
            try
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                .Where(p => p.Category == category)
                .ToList();
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}

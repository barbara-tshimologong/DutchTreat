using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _env;

        public DutchSeeder(DutchContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }  
        
        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (_context.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filePath);

                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _context.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "1000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

            _context.Add(order);

                _context.SaveChanges();
            }
        }
    }
}

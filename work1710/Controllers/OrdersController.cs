using Microsoft.AspNetCore.Mvc;
using work1710.Models;

namespace work1710.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "Олександр Петренко", TotalAmount = 1500.50m, OrderDate = DateTime.Now.AddDays(-5), Status = "Completed" },
            new Order { Id = 2, CustomerName = "Марія Коваленко", TotalAmount = 450.00m, OrderDate = DateTime.Now.AddDays(-3), Status = "Pending" },
            new Order { Id = 3, CustomerName = "Андрій Шевченко", TotalAmount = 3200.00m, OrderDate = DateTime.Now.AddDays(-2), Status = "Completed" },
            new Order { Id = 4, CustomerName = "Іван Дмитрук", TotalAmount = 900.25m, OrderDate = DateTime.Now.AddDays(-1), Status = "Cancelled" },
            new Order { Id = 5, CustomerName = "Ольга Бойко", TotalAmount = 1200.00m, OrderDate = DateTime.Now, Status = "Pending" },
            new Order { Id = 6, CustomerName = "Максим Мельник", TotalAmount = 2100.00m, OrderDate = DateTime.Now, Status = "Completed" }
           };

        [HttpGet]
        public IActionResult GetOrders(
            string? searchCustomer,
            string? sortBy,
            int pageNumber = 1,
            int pageSize = 2)
          {
            var query = _orders.AsQueryable();


            if (!string.IsNullOrEmpty(searchCustomer))
            {
                query = query.Where(o => o.CustomerName.Contains(searchCustomer, StringComparison.OrdinalIgnoreCase));
              }
 
              if (!string.IsNullOrEmpty(sortBy))
             {
                  if (sortBy.ToLower() == "amount")
                {
                    query = query.OrderBy(o => o.TotalAmount);
                  }
                 else if (sortBy.ToLower() == "date")
                  {
                    query = query.OrderBy(o => o.OrderDate);
                 }
              }

            var totalItems = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

               return Ok(new
             {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = items
              });
           }
     }}
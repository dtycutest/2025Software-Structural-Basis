using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrder.自定义;

namespace WebApplicationOrder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly OrdermanagerContext ctx;

        public OrdersController(ILogger<OrdersController> _logger, OrdermanagerContext _db)
        {
            this.logger = _logger;
            this.ctx = _db;
            if (ctx.Products.Count() == 0)
            {
                ctx.Products.Add(new Product("apple", 100.0));
                ctx.Products.Add(new Product("egg", 200.0));
                ctx.SaveChanges();
            }
            if (ctx.Customers.Count() == 0)
            {
                ctx.Customers.Add(new Customer("li"));
                ctx.Customers.Add(new Customer("zhang"));
                ctx.SaveChanges();
            }
        }

        private static void FixOrder(Order newOrder)
        {
            newOrder.CustomerId = newOrder.Customer?.Id;
            newOrder.Customer = null;
            newOrder.Orderdetails.ForEach(d => {
                d.ProductId = d.Product?.Id;
                d.Product = null;
            });
        }

        [HttpGet]
        public object GetProducts()
        {
            return ctx.Products.ToList();
        }
        [HttpGet]
        public List<Order> GetAllOrders()
        {
            if (ctx.Orders.Count() == 0) return null;
            return ctx.Orders
                      .Include(o => o.Orderdetails.Select(d => d.Product))
                      .Include(o => o.Customer)
                      .ToList<Order>();
        }
        [HttpPost]
        public void AddOrder(Order order)
        {
            Console.WriteLine(order);
            FixOrder(order);
            ctx.Entry(order).State = EntityState.Added;
            ctx.SaveChanges();
        }
        [HttpGet]
        public Order GetOrder(string id)
        {
            return ctx.Orders
                .Include(o => o.Orderdetails.Select(d => d.Product))
                .Include(o => o.Customer)
                .SingleOrDefault(o => o.OrderId == id);
        }
        [HttpGet]
        public void RemoveOrder(string orderId)
        {
            var order = ctx.Orders.Include("Details")
                .SingleOrDefault(o => o.OrderId == orderId);
            if (order == null) return;
            ctx.Orderdetails.RemoveRange(order.Orderdetails);
            ctx.Orders.Remove(order);
            ctx.SaveChanges();
        }
        [HttpGet]
        public List<Order> QueryOrdersByProductName(string productsName)
        {
            return ctx.Orders
                .Include(o => o.Orderdetails.Select(d => d.Product))
                .Include(o => o.Customer)
                .Where(order => order.Orderdetails.Any(item => item.Product.Name == productsName))
                .ToList();
        }
        [HttpGet]
        public List<Order> QueryOrdersByCustomerName(string customerName)
        {
            return ctx.Orders
                .Include(o => o.Orderdetails.Select(d => d.Product))
                .Include(o => o.Customer)
                .Where(order => order.Customer.Name == customerName)
                .ToList();
        }
        [HttpGet]
        public List<Order> QueryByTotalPrice(float price)
        {
            return ctx.Orders
                .Include(o => o.Orderdetails.Select(d => d.Product)) //EF core中使用ThenInclude
                .Include("Customer")
            .Where(order => order.Orderdetails.Sum(d => d.Quantity * d.Product.Price) > price)
            .ToList();
        }
        [HttpPost]
        public void UpdateOrder(Order newOrder)
        {
            RemoveOrder(newOrder.OrderId);
            AddOrder(newOrder);
        }
    }
}

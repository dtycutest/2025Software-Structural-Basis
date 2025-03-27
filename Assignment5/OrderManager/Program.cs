using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class Order
    {
        private string id;
        private OrderDetails details;
        public Order(string id,OrderDetails details)
        {
            this.id = id;
            this.details=details;
        }
        public override string ToString()
        {
            return $"{{id:{id},details:{details}}}";
        }
        public bool CheckID(string id)
        {
            return this.id == id;
        }
        public bool CheckGoods(string name)
        {
            return details.CheckGoods(name);
        }
        public bool CheckCustomer(string name)
        {
            return details.CheckCustomer(name);
        }
        public bool CheckPrice(int min,int max)
        {
            return details.CheckPrice(min,max);
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            if(id==order.id&&details==order.details) return true;
            return false;
        }
    }
    public class OrderDetails
    {
        private List<Goods> goods;
        private int price;
        private Customer customer;
        public OrderDetails(Customer customer)
        {
            this.price = 0;
            this.customer = customer;
            this.goods=new List<Goods>();
        }
        public void AddGoods(string name,int num,int unitPrice)
        {
            Goods good = new Goods(name, num, unitPrice);
            this.goods.Add(good);
            //Console.WriteLine(good);
            price += num * unitPrice;
        }
        public override string ToString()
        {
            string goodsStr = "";
            for(int i=0;i<goods.Count;i++)
            {
                Goods good = goods[i];
                goodsStr += good.ToString();
                if(i<goods.Count-1) goodsStr += ",";
            }
            return $"{{goods:[{goodsStr}],price:{price},customer:{customer}}}";
        }
        public bool CheckGoods(string name)
        {
            if(goods.Exists(g=>g.CheckName(name))) return true;
            return false;
        }
        public bool CheckCustomer(string name)
        {
            return customer.CheckName(name);
        }
        public bool CheckPrice(int min,int max)
        {
            return price >= min && price <= max;
        }
        public override bool Equals(object obj)
        {
            OrderDetails details= obj as OrderDetails;
            if(this.ToString()==details.ToString()) return true;
            return false;
        }
    }
    public class OrderService
    {
        private static List<Order> orders=new List<Order>();
        public static Customer RegisterCustomer(string name)
        {
            Customer customer = new Customer(name);
            return customer;
        }
        public static void AddOrder(Customer customer,string id,int n,List<string> names,List<int> nums,List<int> unitPrices)
        {
            OrderDetails details = new OrderDetails(customer);
            for (int i=0;i<n;i++)
            {
                string name = names[i];
                int num = nums[i];
                int unitPrice = unitPrices[i];
                details.AddGoods(name, num, unitPrice);
            }
            Order order = new Order(id,details);
            if (orders.Exists(o => o == order))
            {
                Console.WriteLine("订单已存在");
                return;
            }
            orders.Add(order);
            Console.WriteLine(order);
        }
        public static string ToStringAll()
        {
            string res = "";
            for(int i = 0; i < orders.Count; i++)
            {
                res+= orders[i].ToString();
                if(i<orders.Count-1) res += ",";
            }
            return res;
        }
        public static void PrintAll()
        {
            foreach(Order order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
        public static void DelOrder(string id)
        {
            if (CheckOrder(id))
            {
                orders.Remove(orders.Find(o=>o.CheckID(id)));
            }
            PrintAll();
        }
        public static bool CheckOrder(string id)
        {
            if(orders.Exists(o => o.CheckID(id))) { return true; }
            return false;
        }
        public static void ModifyOrder(Customer customer,string id, int n, List<string> names, List<int> nums, List<int> unitPrices)
        {
            Order order = orders.Find(o => o.CheckID(id));
            orders.Remove(order);
            OrderDetails details = new OrderDetails(customer);
            for (int i = 0; i < n; i++)
            {
                string name = names[i];
                int num = nums[i];
                int unitPrice = unitPrices[i];
                details.AddGoods(name, num, unitPrice);
            }
            orders.Add(new Order(id,details));
        }
        public static IEnumerable<Order> SearchByID(string id)
        {
            if (CheckOrder(id))
            {
                var query = orders.Where(o => o.CheckID(id));
                return query;
            }
            return null;
        }
        public static IEnumerable<Order> SearchByGoodsName(string name)
        {
            if (orders.Exists(o => o.CheckGoods(name)))
            {
                var query=orders.Where(o=>o.CheckGoods(name));
                return query;
            }
            return null;
        }
        public static IEnumerable<Order> SearchByCustomer(string name)
        {
            if (orders.Exists(o => o.CheckCustomer(name)))
            {
                var query=orders.Where(o=>o.CheckCustomer(name));
                return query;
            }
            return null;
        }
        public static IEnumerable<Order> SearchByPrice(int min,int max)
        {
            if (orders.Exists(o => o.CheckPrice(min, max)))
            {
                var query=orders.Where(o=> o.CheckPrice(min, max));
                return query;
            }
            return null;
        }
        public static void Print(IEnumerable<Order> query)
        {
            foreach(var o in query)
            {
                Console.WriteLine(o);
            }
        }
    }
    public class Goods
    {
        private string name;
        private int num;
        private int unitPrice;
        public Goods(string name, int num, int unitPrice)
        {
            this.name = name;
            this.num = num;
            this.unitPrice = unitPrice;
        }
        public override string ToString()
        {
            return $"{{name:{name},num:{num},unitPrice:{unitPrice}}}";
        }
        public bool CheckName(string name)
        {
            return this.name == name;
        }
    }
    public class Customer
    {
        static private int customerIndex=0;
        private int id;
        private string name;
        public Customer(string name)
        {
            this.id = customerIndex++;
            this.name = name;
        }
        public override string ToString()
        {
            return $"{{id:{id},name:{name}}}";
        }
        public bool CheckName(string name)
        {
            return this.name == name;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer a=OrderService.RegisterCustomer("dty");
            Console.WriteLine("控制台订单管理程序：");
            Console.WriteLine("默认用户："+a);
            while (true)
            {
                Console.WriteLine("操作代号：添加订单0、删除订单1、修改订单2、查询订单3。");
                Console.WriteLine("请输入操作号：");
                int opt;
                try
                {
                    opt = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                if (opt == 0)
                {
                    Console.WriteLine("输入订单号：");
                    string id = Console.ReadLine();
                    Console.WriteLine("输入货物种类数：");
                    int n = int.Parse(Console.ReadLine());
                    List<string> names = new List<string>();
                    List<int> nums= new List<int>();
                    List<int> unitPrices= new List<int>();
                    for (int i = 1; i <=n; i++)
                    {
                        Console.WriteLine($"输入货物{i}的名称：");
                        string name = Console.ReadLine();
                        Console.WriteLine($"输入货物{i}的数量：");
                        int num = int.Parse(Console.ReadLine());
                        Console.WriteLine($"输入货物{i}的单价：");
                        int unitPrice = int.Parse(Console.ReadLine());
                        names.Add(name);
                        nums.Add(num);
                        unitPrices.Add(unitPrice);
                    }
                    OrderService.AddOrder(a,id,n,names,nums,unitPrices);
                }
                if (opt == 1)
                {
                    Console.WriteLine("输入要删除的订单号：");
                    string id = Console.ReadLine();
                    OrderService.DelOrder(id);
                }
                if (opt == 2)
                {
                    Console.WriteLine("输入订单号：");
                    string id = Console.ReadLine();
                    if (!OrderService.CheckOrder(id))
                    {
                        Console.WriteLine("order id不存在");
                        continue;
                    }
                    Console.WriteLine("输入货物种类数：");
                    int n = int.Parse(Console.ReadLine());
                    List<string> names = new List<string>();
                    List<int> nums = new List<int>();
                    List<int> unitPrices = new List<int>();
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine($"输入货物{i}的名称：");
                        string name = Console.ReadLine();
                        Console.WriteLine($"输入货物{i}的数量：");
                        int num = int.Parse(Console.ReadLine());
                        Console.WriteLine($"输入货物{i}的单价：");
                        int unitPrice = int.Parse(Console.ReadLine());
                        names.Add(name);
                        nums.Add(num);
                        unitPrices.Add(unitPrice);
                    }
                    OrderService.ModifyOrder(a, id, n, names, nums, unitPrices);
                }
                if(opt == 3)
                {
                    Console.WriteLine("查询订单方式：订单号0、商品名称1、客户2、订单金额3。");
                    Console.WriteLine("请输入查询方式：");
                    int t = int.Parse(Console.ReadLine());
                    if (t == 0)
                    {
                        Console.WriteLine("输入订单号：");
                        string id = Console.ReadLine();
                        var qeury=OrderService.SearchByID(id);
                        if(qeury == null)
                        {
                            Console.WriteLine("订单号不存在");
                            continue;
                        }
                        OrderService.Print(qeury);
                    }
                    if (t == 1)
                    {
                        Console.WriteLine("输入货物名称：");
                        string name = Console.ReadLine();
                        var qeury = OrderService.SearchByGoodsName(name);
                        if (qeury == null)
                        {
                            Console.WriteLine("含有该货物的订单不存在");
                            continue;
                        }
                        OrderService.Print(qeury);
                    }
                    if (t == 2)
                    {
                        Console.WriteLine("输入客户名称：");
                        string name = Console.ReadLine();
                        var qeury = OrderService.SearchByCustomer(name);
                        if (qeury == null)
                        {
                            Console.WriteLine("不存在该客户的订单");
                            continue;
                        }
                        OrderService.Print(qeury);
                    }
                    if (t == 3)
                    {
                        Console.WriteLine("输入金额下限：");
                        int min = int.Parse(Console.ReadLine());
                        Console.WriteLine("输入金额上限：");
                        int max = int.Parse(Console.ReadLine());
                        var qeury = OrderService.SearchByPrice(min,max);
                        if (qeury == null)
                        {
                            Console.WriteLine("订单号不存在");
                            continue;
                        }
                        OrderService.Print(qeury);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderApp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OrderManager
{
    public partial class Form1 : Form
    {
        private OrderService orderService = new OrderService();
        List<Product> products = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            Customer customer1 = new Customer(1, "Customer1");
            Customer customer2 = new Customer(2, "Customer2");

            Product milk = new Product(1, "Milk", 69.9f);
            Product eggs = new Product(2, "eggs", 4.99f);
            Product apple = new Product(3, "apple", 5.59f);
            products.Add(milk);
            products.Add(eggs);
            products.Add(apple);

            Order order1 = new Order(1, customer1, new DateTime(2021, 3, 21));
            order1.AddDetails(new OrderDetail(apple, 8));
            order1.AddDetails(new OrderDetail(eggs, 10));
            //order1.AddDetails(new OrderDetail(eggs, 8));
            //order1.AddDetails(new OrderDetail(milk, 10));

            Order order2 = new Order()
            {
                Id = 2,
                Customer = customer2,
                CreateTime = new DateTime(2021, 3, 21),
                Details = new OrderDetails(new List<OrderDetail> { new OrderDetail(eggs, 10), new OrderDetail(milk, 10) })
            };

            Order order3 = new Order(3, customer2, new DateTime(2021, 3, 21));
            order3.AddDetails(new OrderDetail(milk, 100));

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);

            dataGridView1.DataSource = orderService.orders;

            List<string> methods = new List<string> { "查询订单号", "查询用户名称", "查询商品名称", "查询总价（达到一定）金额" };
            comboBoxQueryMethod.DataSource = methods;

            dataGridView2.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                Customer customer = new Customer(form2.customerId,form2.customerName);
                Order order = new Order(form2.Id, customer, DateTime.Now,form2.Details);
                orderService.AddOrder(order);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = orderService.orders;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(textBoxDelete.Text);
            orderService.RemoveOrder(Id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = orderService.orders;
        }

        private void comboBoxQueryMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> keys = new List<string> { "订单号:", "用户名称:", "商品名称:", "总价（达到一定）金额:" };
            int opt=comboBoxQueryMethod.SelectedIndex;
            labelKey.Text = keys[opt];
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            int opt = comboBoxQueryMethod.SelectedIndex;
            IEnumerable<Order> orders = orderService.orders;
            if (opt == 0) orders = orderService.Query(o => o.Id == int.Parse(textBoxKey.Text));
            if (opt == 1) orders = orderService.QueryByCustomerName(textBoxKey.Text);
            if (opt == 2) orders = orderService.QueryByProductName(textBoxKey.Text);
            if (opt == 3) orders = orderService.QueryByTotalPrice(int.Parse(textBoxKey.Text));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = orders.ToList<Order>();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(textBoxModify.Text);
            int idx = orderService.orders.FindIndex(o => o.Id == Id);
            Form2 form2 = new Form2();
            Order order = orderService.orders[idx];
            form2.textBoxId.Text = order.Id.ToString();
            form2.textBoxCustomerId.Text = order.Customer.Id.ToString();
            form2.textBoxCustomerName.Text = order.Customer.Name.ToString();
            order.Details.Details.ForEach(d => form2.Details.Details.Add(d));
            form2.dataGridView1.DataSource = form2.Details.Details;
            if (idx >= 0)
            {
                orderService.orders.RemoveAt(idx);
            }
            try
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    Customer customer = new Customer(form2.customerId, form2.customerName);
                    Order newOrder = new Order(form2.Id, customer, DateTime.Now, form2.Details);
                    orderService.AddOrder(newOrder);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = orderService.orders;
                }
            }
            catch(DataException ev)
            {
                MessageBox.Show(ev.Message);
            }
        }
    }
}

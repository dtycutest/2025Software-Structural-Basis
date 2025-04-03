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

namespace OrderManager
{
    public partial class Form3 : Form
    {
        public OrderDetail detail=new OrderDetail();
        public List<Product> products = new List<Product>();
        public Product current;
        public Form3()
        {
            InitializeComponent();
            Product milk = new Product(1, "Milk", 69.9f);
            Product eggs = new Product(2, "eggs", 4.99f);
            Product apple = new Product(3, "apple", 5.59f);
            products.Add(milk);
            products.Add(eggs);
            products.Add(apple);
            List<string> productNames= new List<string>();
            for (int i = 0; i < products.Count; i++)
            {
                productNames.Add(products[i].Name);
            }
            comboBox1.DataSource = productNames;
            comboBox1.SelectedIndex = 0;
        }
        /*private void buttonConfirm_Click(object sender, EventArgs e)
        {
            detail=
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            current = products[comboBox1.SelectedIndex];
            label1.Text = current.Id.ToString();
            label2.Text = current.Name;
            label3.Text = current.Price.ToString();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            detail = new OrderDetail(current, int.Parse(textBoxQuantity.Text));
            this.DialogResult=DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}

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
    public partial class Form2 : Form
    {
        public int Id;
        public int customerId;
        public string customerName;
        public OrderDetails Details=new OrderDetails();
        public Form2()
        {
            InitializeComponent();
            dataGridView1.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3= new Form3();
            if (form3.ShowDialog() == DialogResult.OK)
            {
                Details.Details.Add(form3.detail);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Details.Details;
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this.Id=int.Parse(textBoxId.Text);
            this.customerId=int.Parse(textBoxCustomerId.Text);
            this.customerName=textBoxCustomerName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string cellValue = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            int idx=Details.Details.FindIndex(d=>d.Product.ToString()==cellValue);
            if (idx >= 0) Details.Details.RemoveAt(idx);
            dataGridView1.DataSource=null;
            dataGridView1.DataSource = Details.Details;
        }
    }
}

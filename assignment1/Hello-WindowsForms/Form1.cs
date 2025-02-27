using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hello_WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b= int.Parse(textBox2.Text);
            string opt=comboBox1.SelectedItem.ToString();
            int res=0;
            if (opt == "+") res = a + b;
            if (opt == "-") res = a - b;
            if (opt == "*") res = a * b;
            if (opt == "/")
            {
                if(b!=0)
                    res = a / b;
                else
                {
                    label4.Text = "不可以除以0 qwq";
                    return;
                }
            }
            if (opt == "%") res = a % b;
            label4.Text = res.ToString();
        }
    }
}

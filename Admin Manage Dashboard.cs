using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event_Management_system_2._0
{
    public partial class Admin_Manage_Dashboard : Form
    {
        public Admin_Manage_Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Log_In li = new Log_In();
            li.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            txtEvent cs = new txtEvent();
            cs.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vanue v = new Vanue();
            v.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Generate_Reports gn = new Generate_Reports();
            gn.Show();
        }
    }
}

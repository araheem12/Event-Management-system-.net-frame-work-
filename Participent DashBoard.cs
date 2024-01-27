using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//# include"Customercs.cs"

namespace Event_Management_system_2._0
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Vanue vn = new Vanue();
            vn.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customercs cs=new Customercs();
            cs.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FeedBack fb = new FeedBack();
            fb.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Log_In li = new Log_In();
            li.Show();
        }
    }
}

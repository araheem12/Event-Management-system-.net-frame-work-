using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
//#include "DashBoard.cs"
namespace Event_Management_system_2._0
{
    public partial class Log_In : Form
    {
        //string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
        OracleConnection con;


        public Log_In()
        {
            InitializeComponent();
        }
        private void Log_In_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Ensure the application exits when the form is closed
        }
        private void button1_Click(object sender, EventArgs e)
        {

            con.Open();
            this.Hide();

            if (textBox1.Text == "participent" && textBox2.Text == "participent")
            {
                DashBoard db = new DashBoard();
                db.Show();
                con.Close();
            }
            else if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Admin_Manage_Dashboard ad = new Admin_Manage_Dashboard();
                ad.Show();
                con.Close();
            }
            else
            {
                MessageBox.Show("Enter correct credentials!!");
                con.Close();
                // Clear the textboxes for re-entry
                textBox1.Text = "";
                textBox2.Text = "";
                // Show the login form again
                this.Show();
                return;
            }
        }

        private void Log_In_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
             con = new OracleConnection(conStr);
            this.FormClosing += Log_In_FormClosing;
        }
    }
}

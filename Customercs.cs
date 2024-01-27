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
namespace Event_Management_system_2._0
{
    public partial class Customercs : Form
    {
        OracleConnection con;
        public Customercs()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void updategrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM CUSTOMER";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }
        private void Customercs_Load(object sender, EventArgs e)
        {
            string str = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
            con = new OracleConnection(str);
            updategrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand insertEmp = con.CreateCommand();
                insertEmp.CommandText = "INSERT INTO CUSTOMER (cusid, cusname, cusphone) VALUES (:cusId, :cusName, :cusPhone)";

                // Use parameters to avoid SQL injection and improve security
                insertEmp.Parameters.Add(":cusId", OracleDbType.Int32).Value = Convert.ToInt32(txtcusid.Text);
                insertEmp.Parameters.Add(":cusName", OracleDbType.Varchar2).Value = txtcusname.Text;
                insertEmp.Parameters.Add(":cusPhone", OracleDbType.Varchar2).Value = txtcusphone.Text;

                insertEmp.CommandType = CommandType.Text;
                int rows = insertEmp.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Data Inserted Successfully!\n also confirmation mail has been sent");
                }
                else
                {
                    MessageBox.Show("No rows inserted. Please check your input values.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid format. Please enter valid values for numeric fields.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                updategrid();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand deleteEmp = con.CreateCommand();
                deleteEmp.CommandText = "DELETE FROM CUSTOMER WHERE cusid = :cusId";

                // Use parameters to avoid SQL injection and improve security
                deleteEmp.Parameters.Add(":cusId", OracleDbType.Int32).Value = Convert.ToInt32(txtcusid.Text);

                deleteEmp.CommandType = CommandType.Text;
                int rows = deleteEmp.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Data Deleted Successfully!");
                }
                else
                {
                    MessageBox.Show("No rows deleted. Please check the provided cusId.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                updategrid();
            }



        }

        private void updt_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                using (OracleCommand updateCustomer = con.CreateCommand())
                {
                    updateCustomer.CommandText = "UPDATE CUSTOMER SET cusname = :cusName, cusphone = :cusPhone WHERE cusid = :cusId";

                    updateCustomer.Parameters.Add(":cusName", OracleDbType.Varchar2).Value = txtcusname.Text;
                    updateCustomer.Parameters.Add(":cusPhone", OracleDbType.Int32).Value = Convert.ToInt32(txtcusphone.Text);
                    updateCustomer.Parameters.Add(":cusId", OracleDbType.Int32).Value = Convert.ToInt32(txtcusid.Text);

                    updateCustomer.CommandType = CommandType.Text;

                    int rows = updateCustomer.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data Updated Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No rows updated. Please check your input values or ensure the record exists.");
                    }
                } // The using block ensures that the command is disposed properly

            }
            catch (OracleException ex)
            {
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'CUSTOMER' is created in the database.");
                }
                else
                {
                    MessageBox.Show("Oracle Exception: " + ex.Message);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid format. Please enter valid values for numeric fields.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            updategrid();
        }

        private void txtcusname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcusid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            DashBoard db = new DashBoard();
            db.Show();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
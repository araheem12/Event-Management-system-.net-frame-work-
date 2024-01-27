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
    public partial class Vanue : Form
    {
        string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";

        public Vanue()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Vanue_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
            new OracleConnection(conStr);
            updategrid();
        }
        private void updategrid()
        {
            OracleConnection conn = new OracleConnection(conStr);

            conn.Open();
            OracleCommand getEmps = conn.CreateCommand();
            getEmps.CommandText = "SELECT * FROM Vanue";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);
            try
            {
                {
                    con.Open();

                    using (OracleCommand insertVanue = con.CreateCommand())
                    {
                        insertVanue.CommandText = "INSERT INTO Vanue (V_id, Name, Capacity, address, Manager, phone_no) " +
                                                  "VALUES (:V_id, :Name, :Capacity, :address, :Manager, :phone_no)";

                        insertVanue.Parameters.Add(":V_id", OracleDbType.Int32).Value = Convert.ToInt32(txtv_id.Text); // Assuming txtv_id is the textbox for V_id
                        insertVanue.Parameters.Add(":Name", OracleDbType.Varchar2, 50).Value = txtName.Text;
                        insertVanue.Parameters.Add(":Capacity", OracleDbType.Decimal).Value = Convert.ToDecimal(txtCapacity.Text);
                        insertVanue.Parameters.Add(":address", OracleDbType.Varchar2, 50).Value = txtaddress.Text;
                        insertVanue.Parameters.Add(":Manager", OracleDbType.Varchar2, 50).Value = txtManager.Text;
                        insertVanue.Parameters.Add(":phone_no", OracleDbType.Varchar2, 50).Value = txtPhone_no.Text;

                        int rows = insertVanue.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Data Inserted Successfully!");
                            updategrid();
                        }
                        else
                        {
                            MessageBox.Show("No rows inserted. Please check your input values.");
                        }
                    } // The using block ensures that the command is disposed properly
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'Vanue' is created in the database.");
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

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);

            try
            {
                con.Open();

                using (OracleCommand deleteVanue = con.CreateCommand())
                {
                    deleteVanue.CommandText = "DELETE FROM Vanue WHERE V_id = :V_id";
                    deleteVanue.Parameters.Add(":V_id", OracleDbType.Int32).Value = Convert.ToInt32(txtv_id.Text);

                    deleteVanue.CommandType = CommandType.Text;

                    int rows = deleteVanue.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No rows deleted. Please check the provided V_id.");
                    }
                } // The using block ensures that the command is disposed properly

                updategrid(); // Assuming this is a method that refreshes your data grid
            }
            catch (OracleException ex)
            {
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'Vanue' is created in the database.");
                }
                else
                {
                    MessageBox.Show("Oracle Exception: " + ex.Message);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid format. Please enter a valid V_id for deletion.");
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
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);

            try
            {
                con.Open();

                using (OracleCommand updateVanue = con.CreateCommand())
                {
                    updateVanue.CommandText = "UPDATE Vanue SET Name = :Name, Capacity = :Capacity, address = :address, Manager = :Manager, phone_no = :phone_no " +
                                              "WHERE V_id = :V_id";

                    updateVanue.Parameters.Add(":Name", OracleDbType.Varchar2, 50).Value = txtName.Text;
                    updateVanue.Parameters.Add(":Capacity", OracleDbType.Decimal).Value = Convert.ToDecimal(txtCapacity.Text);
                    updateVanue.Parameters.Add(":address", OracleDbType.Varchar2, 50).Value = txtaddress.Text;
                    updateVanue.Parameters.Add(":Manager", OracleDbType.Varchar2, 50).Value = txtManager.Text;
                    updateVanue.Parameters.Add(":phone_no", OracleDbType.Varchar2, 50).Value = txtPhone_no.Text;
                    updateVanue.Parameters.Add(":V_id", OracleDbType.Int32).Value = Convert.ToInt32(txtv_id.Text);

                    int rows = updateVanue.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data Updated Successfully!");
                        updategrid();
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
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'Vanue' is created in the database.");
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Manage_Dashboard ab = new Admin_Manage_Dashboard();
            ab.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
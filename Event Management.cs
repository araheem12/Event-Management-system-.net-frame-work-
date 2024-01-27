using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace Event_Management_system_2._0
{
    public partial class txtEvent : Form
    {
        string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
        public txtEvent()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Event_Management_Load(object sender, EventArgs e)
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
            getEmps.CommandText = "SELECT * FROM Event";
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
                con.Open();

                using (OracleCommand insertEvent = con.CreateCommand())
                {
                    // Define the SQL INSERT statement with parameters
                    insertEvent.CommandText = "INSERT INTO Event (Event_Id, Event_Name, Event_Date, Time, fees, duration, status) VALUES " +
                                              "(:Event_Id, :Event_Name, :Event_Date, :Time, :fees, :duration, :status)";

                    // Add parameters with their corresponding values
                    insertEvent.Parameters.Add(":Event_Id", OracleDbType.Int32).Value = Convert.ToInt32(txtE_id.Text);
                    insertEvent.Parameters.Add(":Event_Name", OracleDbType.Varchar2).Value = txtE_name.Text;
                    insertEvent.Parameters.Add(":Event_Date", OracleDbType.Varchar2).Value = txtE_date.Text;  // Assuming your date is stored as a string
                    insertEvent.Parameters.Add(":Time", OracleDbType.Varchar2).Value = txttime.Text;  // Assuming your date is stored as a string
                    insertEvent.Parameters.Add(":fees", OracleDbType.Varchar2).Value = txtfees.Text;  // Assuming your date is stored as a string
                    insertEvent.Parameters.Add(":duration", OracleDbType.Varchar2).Value = txtDuration.Text;  // Assuming your date is stored as a string
                    insertEvent.Parameters.Add(":status", OracleDbType.Varchar2).Value = txtStatus.Text;  // Assuming your date is stored as a string






                    int rows = insertEvent.ExecuteNonQuery();

                    // Check the result of the execution
                    if (rows > 0)
                    {
                        MessageBox.Show("Data Inserted Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No rows inserted. Please check your input values.");
                    }
                }
                updategrid();
            }
            catch (OracleException ex)
            {
                // Handle Oracle-specific exceptions
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'Event' is created in the database.");
                }
                else
                {
                    MessageBox.Show("Oracle Exception: " + ex.Message);
                }
            }
            catch (FormatException ex)
            {
                // Handle format-related exceptions, e.g., when converting string to numeric
                MessageBox.Show("Invalid format. Please enter valid values for numeric fields.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Ensure that the database connection is closed, whether an exception occurs or not
                con.Close();
            }




        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Manage_Dashboard ab = new Admin_Manage_Dashboard();
            ab.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);
            try
            {
                con.Open();

                using (OracleCommand updateEvent = con.CreateCommand())
                {
                    updateEvent.CommandText = "UPDATE Event SET Event_name = :Event_name, Event_date = :Event_date, " +
                                              "time = :time, fees = :fees, duration = :duration, status = :status " +
                                              "WHERE Event_id = :Event_id";

                    updateEvent.Parameters.Add(":Event_id", OracleDbType.Int32).Value = Convert.ToInt32(txtE_id.Text);
                    updateEvent.Parameters.Add(":Event_name", OracleDbType.Varchar2, 50).Value = txtE_name.Text;
                    updateEvent.Parameters.Add(":Event_date", OracleDbType.Varchar2, 50).Value = txtEvent_date.Text;
                    updateEvent.Parameters.Add(":time", OracleDbType.Varchar2, 50).Value = txttime.Text;
                    updateEvent.Parameters.Add(":fees", OracleDbType.Varchar2, 50).Value = txtfees.Text;
                    updateEvent.Parameters.Add(":duration", OracleDbType.Varchar2, 50).Value = txtDuration.Text;
                    updateEvent.Parameters.Add(":status", OracleDbType.Varchar2, 50).Value = txtStatus.Text;

                    int rows = updateEvent.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data Updated Successfully!");
                        updategrid();
                    }
                    else
                    {
                        MessageBox.Show("No rows updated. Please check your input values or ensure the record exists.");
                    }
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'Event' is created in the database.");
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
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
                updategrid();
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);

            try
            {
                con.Open();
                OracleCommand deleteEmp = con.CreateCommand();
                deleteEmp.CommandText = "DELETE FROM Event WHERE Event_id= :cusId";

                // Use parameters to avoid SQL injection and improve security
                deleteEmp.Parameters.Add(":cusId", OracleDbType.Int32).Value = Convert.ToInt32(txtE_id.Text);

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

        private void time_Click(object sender, EventArgs e)
        {

        }
    }
}

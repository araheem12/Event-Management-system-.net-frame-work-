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
    public partial class FeedBack : Form
    {
        string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";

        public FeedBack()
        {
            InitializeComponent();
        }

        private void FeedBack_Load(object sender, EventArgs e)
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
            getEmps.CommandText = "SELECT * FROM feedback";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            conn.Close();
        }

        private void txtVanueName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);

            try
            {
                con.Open();

                using (OracleCommand insertFeedback = con.CreateCommand())
                {
                    insertFeedback.CommandText = "INSERT INTO feedback (fbnum, ev_id, ev_name, Venue, Punctuality, Hospitality, overall) VALUES " +
                                                 "(:fbnum, :ev_id, :ev_name, :Venue, :Punctuality, :Hospitality, :overall)";

                    // Assuming fbnum is an auto-incremented primary key and you don't need to provide a value for it
                    // If fbnum is not auto-incremented, provide a valid value for it in your application logic
                    insertFeedback.Parameters.Add(":fbnum", OracleDbType.Int32).Value = Convert.ToInt32(txtfb_Id.Text);
                    insertFeedback.Parameters.Add(":ev_id", OracleDbType.Int32).Value = Convert.ToInt32(txtEvent_Id.Text);
                    insertFeedback.Parameters.Add(":ev_name", OracleDbType.Varchar2).Value = txtEventName.Text;
                    insertFeedback.Parameters.Add(":Venue", OracleDbType.Varchar2).Value = txtVenue.Text;
                    insertFeedback.Parameters.Add(":Punctuality", OracleDbType.Varchar2).Value = txtPunctuality.Text;
                    insertFeedback.Parameters.Add(":Hospitality", OracleDbType.Varchar2).Value = txtHospitality.Text;
                    insertFeedback.Parameters.Add(":overall", OracleDbType.Varchar2).Value = txtOverall.Text;

                    insertFeedback.CommandType = CommandType.Text;

                    int rows = insertFeedback.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data Inserted Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No rows inserted. Please check your input values.");
                    }
                } // The using block ensures that the command is disposed properly

                // Optionally, updategrid(); if needed for your application logic
            }
            catch (OracleException ex)
            {
                if (ex.Number == 942) // Check for ORA-00942 error code
                {
                    MessageBox.Show("Table or view does not exist. Please ensure the table 'feedback' is created in the database.");
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
                updategrid();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection(conStr);

try
{
    con.Open();

    using (OracleCommand deleteFeedback = con.CreateCommand())
    {
        deleteFeedback.CommandText = "DELETE FROM feedback WHERE fbnum = :fbnum";
        deleteFeedback.Parameters.Add(":fbnum", OracleDbType.Int32).Value = Convert.ToInt32(txtfb_Id.Text);

        deleteFeedback.CommandType = CommandType.Text;

        int rows = deleteFeedback.ExecuteNonQuery();

        if (rows > 0)
        {
            MessageBox.Show("Data Deleted Successfully!");
        }
        else
        {
            MessageBox.Show("No rows deleted. Please check the provided fbnum.");
        }
    } // The using block ensures that the command is disposed properly

    // Optionally, updategrid(); if needed for your application logic
}
catch (OracleException ex)
{
    if (ex.Number == 942) // Check for ORA-00942 error code
    {
        MessageBox.Show("Table or view does not exist. Please ensure the table 'feedback' is created in the database.");
    }
    else
    {
        MessageBox.Show("Oracle Exception: " + ex.Message);
    }
}
catch (FormatException ex)
{
    MessageBox.Show("Invalid format. Please enter a valid fbnum for deletion.");
}
catch (Exception ex)
{
    // Handle other exceptions
    MessageBox.Show("An error occurred: " + ex.Message);
}
finally
{
    con.Close();
                updategrid();
}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashBoard db = new DashBoard();
            db.Show();

        }
    }
}

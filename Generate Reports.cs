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
    public partial class Generate_Reports : Form
    {
        string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";

        public Generate_Reports()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Generate_Reports_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID =F219051; PASSWORD= abdulraheem50@";
            new OracleConnection(conStr);
            updategrid();
        }
        private void updategrid()
        {
            OracleConnection con = new OracleConnection(conStr);

            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM report";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Manage_Dashboard ab = new Admin_Manage_Dashboard();
            ab.Show();
        }

        private DataTable reportData;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Set up font and brush for drawing
            Font font = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Set initial position
            float yPos = 10;

            // Print column headers
            for (int i = 0; i < reportData.Columns.Count; i++)
            {
                e.Graphics.DrawString(reportData.Columns[i].ColumnName, font, brush, new Point(10 + i * 100, (int)yPos));
            }

            // Move to the next line
            yPos += font.GetHeight();

            // Print data
            for (int i = 0; i < reportData.Rows.Count; i++)
            {
                for (int j = 0; j < reportData.Columns.Count; j++)
                {
                    e.Graphics.DrawString(reportData.Rows[i][j].ToString(), font, brush, new Point(10 + j * 100, (int)yPos));
                }

                // Move to the next line
                yPos += font.GetHeight();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Fetch data from Oracle database
            reportData = GetReportDataFromOracle();

            // Open print dialog
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Print the document
                printDocument1.Print();
            }
        }

        // Function to fetch data from the Oracle database (replace this with your actual data retrieval logic)
        private DataTable GetReportDataFromOracle()
        {
            // Assuming you have an Oracle SQL query to retrieve data from your view
            string query = "SELECT * FROM report";

            // Use an OracleDataAdapter to fill a DataTable
            OracleConnection con = new OracleConnection(conStr);
            {
                con.Open();
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, con))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SQLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetFontAndColorOfDatagridview();
        }
        public void SetFontAndColorOfDatagridview()
        {
            dataGridView.BackgroundColor = Color.FromArgb(64, 64, 64);
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dataGridView.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView.DefaultCellStyle.Font = new Font("Comic Sans MS", 12);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Comic Sans MS", 12);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.DimGray;

            dataGridView.AllowUserToResizeRows = false;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string P_Name = TxtProdName.Text;
            string P_Price = TxtProdPrice.Text;
            string p_Quantity = TxtProdQuantity.Text;

            if (P_Name == "" || TxtProdPrice.Text == "" || TxtProdQuantity.Text == "")
            {
                //MessageBox.Show("Data Missing!!!");
                LblMsg.ForeColor = Color.Aqua;
                LblMsg.Text = "WARNING: Input data to add them to Database";
            }
            else
            {
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = StoreMart.sqlite3");
                ConnectDb.Open();

                string query = "INSERT INTO Products(Name,Price,Quantity) VALUES('" + P_Name + "','" + P_Price + "','" + p_Quantity + "')";
                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                TxtProdName.Text = "";
                TxtProdPrice.Text = "";
                TxtProdQuantity.Text = "";

                LblMsg.ForeColor = Color.Chartreuse;
                LblMsg.Text = "LAST ACTIVITY: Data Successfully added to database";
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = StoreMart.sqlite3");
            ConnectDb.Open();

            string query = "SELECT * FROM Products";
            SQLiteDataAdapter DataAdptr = new SQLiteDataAdapter(query, ConnectDb);

            DataTable Dt = new DataTable();
            DataAdptr.Fill(Dt);
            dataGridView.DataSource = Dt;

            ConnectDb.Close();

            dataGridView.Columns[0].Width = Convert.ToInt32(dataGridView.Width * 0.15);
            dataGridView.Columns[1].Width = Convert.ToInt32(dataGridView.Width * 0.4);
            dataGridView.Columns[2].Width = Convert.ToInt32(dataGridView.Width * 0.2);
            dataGridView.Columns[3].Width = Convert.ToInt32(dataGridView.Width * 0.2);

            LblMsg.ForeColor = Color.Chartreuse;
            LblMsg.Text = "LAST ACTIVITY: Viewing Database table";

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string P_ID = TxtID.Text;

            if (TxtID.Text == "")
            {
                LblMsg.ForeColor = Color.Aqua;
                LblMsg.Text = "WARNING: Input data to delete it from Database";
            }
            else
            {
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = StoreMart.sqlite3");
                ConnectDb.Open();

                string query = "DELETE FROM Products WHERE ID ='" + P_ID + "' ";
                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                TxtID.Text = "";

                LblMsg.ForeColor = Color.Chartreuse;
                LblMsg.Text = "LAST ACTIVITY: Data Successfully deleted from database";
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string U_ID = TxtUpdateID.Text;
            string U_Name = TxtUpdateName.Text;
            string U_Price = TxtUpdatePrice.Text;
            string U_Quantity = TxtUpdateQuantity.Text;

            if (TxtUpdateID.Text == "" || TxtUpdateName.Text == "" || TxtUpdatePrice.Text == "" ||
                TxtUpdateQuantity.Text == "")
            {
                LblMsg.ForeColor = Color.Aqua;
                LblMsg.Text = "WARNING: Input data to modify it in the Database";
            }
            else
            {
                SQLiteConnection ConnectDb = new SQLiteConnection("Data Source = StoreMart.sqlite3");
                ConnectDb.Open();

                string query = "REPLACE INTO Products(ID,Name,Price,Quantity) VALUES('" + U_ID + "','" + U_Name + "','" + U_Price + "','" + U_Quantity + "')";
                SQLiteCommand Cmd = new SQLiteCommand(query, ConnectDb);
                Cmd.ExecuteNonQuery();

                ConnectDb.Close();

                TxtUpdateID.Text = "";
                TxtUpdateName.Text = "";
                TxtUpdatePrice.Text = "";
                TxtUpdateQuantity.Text = "";

                LblMsg.ForeColor = Color.Chartreuse;
                LblMsg.Text = "LAST ACTIVITY: Existing Data Successfully updated in the database";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Book_Management_System
{
    public partial class AddCat : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public AddCat()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (TXT_CAT.Text != "")
            {
                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO TBCAT (CAT) VALUES (@CAT)";
                cmd.Parameters.AddWithValue("@CAT", TXT_CAT.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت الاضافة بنجاح");
                this.Close();
            }
            else
            {
                MessageBox.Show("اكتب اسم الصنف اولا");
            }
        }
    }
}

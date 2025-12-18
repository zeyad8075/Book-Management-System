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
using System.IO;

namespace Book_Management_System
{
    
    public partial class AddBook : Form
    {
        //var connection
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        List<string> list = new List<string>();
        public int state;
        public AddBook()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddCat AddCat_FRM = new AddCat();
            AddCat_FRM.Show();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            if (txt_name.Text == "" || txt_auther.Text == "" || txt_price.Text == "" || txt_cat.Text == "")
            {
                MessageBox.Show("اكمل بيانات الكتاب");
            }
            else
            {
                if (state == 0)
                {
                    //convert image to binary
                    MemoryStream ma = new MemoryStream();
                    this._cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();
                    /* --------------------------------------------------------------------------------*/
                    //sql command
                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO BOOKS (TITLE,AUTHER,PRICE,CAT,DATE,COVER,NUMBER) VALUES (@TITLE,@AUTHER,@PRICE,@CAT,@DATE,@COVER,@NUMBER)";
                    cmd.Parameters.AddWithValue("@TITLE", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.Parameters.AddWithValue("@NUMBER", txt_number.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت الاضافة بنجاح");
                    this.Close();
                }
                else
                {
                    /*UPDATE8*/
                    //convert image to binary
                    MemoryStream ma = new MemoryStream();
                    this._cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();
                    /* --------------------------------------------------------------------------------*/
                    //sql command
                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE BOOKS SET TITLE=@TITLE,AUTHER=@AUTHER,PRICE=@PRICE,CAT=@CAT,DATE=@DATE,COVER=@COVER,NUMBER=@NUMBER WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@TITLE", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.Parameters.AddWithValue("@ID", state);
                    cmd.Parameters.AddWithValue("@NUMBER", txt_number.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم التعديل بنجاح");
                    this.Close();
                }

            }
            cmd.Parameters.Clear();

        }

        private void AddBook_Load(object sender, EventArgs e)
        {

           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dia = new OpenFileDialog();
            var result = dia.ShowDialog();
            if (result == DialogResult.OK)
            {
                _cover.Image = Image.FromFile(dia.FileName);
            }
        }

        private void txt_cat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddBook_Activated(object sender, EventArgs e)
        {
            try
            {
                txt_cat.Items.Clear();
                List<string> list = new List<string>();
                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT CAT FROM TBCAT";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(Convert.ToString(rd[0]));
                }
                int i = 0;
                while (i < list.LongCount())
                {
                    txt_cat.Items.Add(list[i]);
                    i++;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

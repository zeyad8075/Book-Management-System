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
    public partial class books_management : Form
    {
        //FOR SQL CONNECT 
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        List<string> list = new List<string>();
        public int state;
        public books_management()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddBook ADD_BOOK_FRM = new AddBook();
            ADD_BOOK_FRM.btnadd.Text = "اضافة";
            ADD_BOOK_FRM.Show();
            ADD_BOOK_FRM.state = 0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BookDetailes BookDetailes_FRM = new BookDetailes();
            BookDetailes_FRM.Show();
            List<string> list_det = new List<string>();
            try
            {

                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT TITLE,AUTHER,PRICE,CAT,DATE,NUMBER FROM BOOKS WHERE ID=@ID";
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list_det.Add(Convert.ToString(rd[0]));
                    list_det.Add(Convert.ToString(rd[1]));
                    list_det.Add(Convert.ToString(rd[2]));
                    list_det.Add(Convert.ToString(rd[3]));
                    list_det.Add(Convert.ToString(rd[4]));
                    list_det.Add(Convert.ToString(rd[5]));

                }

                BookDetailes_FRM.name.Text = list_det[0];
                BookDetailes_FRM.auther.Text = list_det[1];
                BookDetailes_FRM.price.Text = list_det[2] + " $";
                BookDetailes_FRM.cat.Text = list_det[3];
                BookDetailes_FRM.date.Text = list_det[4];
                BookDetailes_FRM.number.Text = list_det[5];
                con.Close();
                //read image from DB
                con.Open();
                cmd.CommandText = "SELECT COVER FROM BOOKS WHERE ID=@IDIMAGE";
                cmd.Parameters.AddWithValue("@IDIMAGE", dataGridView1.CurrentRow.Cells[0].Value);
                byte[] img = (byte[])cmd.ExecuteScalar();
                MemoryStream ma = new MemoryStream();
                ma.Write(img, 0, img.Length);
                BookDetailes_FRM._Cover.Image = Image.FromStream(ma);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
            cmd.Parameters.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       

        private void books_management_Activated_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
            var sql = "SELECT ID ,TITLE,AUTHER,PRICE,CAT,NUMBER FROM BOOKS";
            da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "رقم الكتاب";
            dataGridView1.Columns[1].HeaderText = "العنوان";
            dataGridView1.Columns[2].HeaderText = "المؤلف";
            dataGridView1.Columns[3].HeaderText = "السعر";
            dataGridView1.Columns[4].HeaderText = "التصنيف";
            dataGridView1.Columns[5].HeaderText = "العدد";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            AddBook ADD_BOOK_FRM = new AddBook();
            ADD_BOOK_FRM.btnadd.Text = "تعديل";
            ADD_BOOK_FRM.Show();
            ADD_BOOK_FRM.state = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            try
            {

                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT TITLE,AUTHER,PRICE,CAT,DATE,NUMBER FROM BOOKS WHERE ID=@ID";
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(Convert.ToString(rd[0]));
                    list.Add(Convert.ToString(rd[1]));
                    list.Add(Convert.ToString(rd[2]));
                    list.Add(Convert.ToString(rd[3]));
                    list.Add(Convert.ToString(rd[4]));
                    list.Add(Convert.ToString(rd[5]));
                }

                ADD_BOOK_FRM.txt_name.Text = list[0];
                ADD_BOOK_FRM.txt_auther.Text = list[1];
                ADD_BOOK_FRM.txt_price.Text = list[2];
                ADD_BOOK_FRM.txt_cat.Text = list[3];
                ADD_BOOK_FRM.txt_date.Value = Convert.ToDateTime(list[4]);
                ADD_BOOK_FRM.txt_number.Text = list[5];
                con.Close();
                //read image from DB
                con.Open();
                cmd.CommandText = "SELECT COVER FROM BOOKS WHERE ID=@IDIMAGE";
                cmd.Parameters.AddWithValue("@IDIMAGE", dataGridView1.CurrentRow.Cells[0].Value);
                byte[] img = (byte[])cmd.ExecuteScalar();
                MemoryStream ma = new MemoryStream();
                ma.Write(img, 0, img.Length);
                ADD_BOOK_FRM._cover.Image = Image.FromStream(ma);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }
            cmd.Parameters.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "DELETE FROM BOOKS WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Parameters.Clear();
            MessageBox.Show("تمت عملية الحذف بنجاح");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (txt_search.Text == "") { MessageBox.Show("enter id of book. please.."); }
            else
            {
                BookDetailes BookDetailes_FRM = new BookDetailes();
                BookDetailes_FRM.Show();
                List<string> list_det = new List<string>();
                try
                {

                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\My_projectes\C#_progectes\Book Management System\Book Management System\BOOK_DB.mdf;Integrated Security=True");
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "SELECT TITLE,AUTHER,PRICE,CAT,DATE,NUMBER FROM BOOKS WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txt_search.Text));
                    var rd = cmd.ExecuteReader();
                    
                    while (rd.Read())
                    {
                        list_det.Add(Convert.ToString(rd[0]));
                        list_det.Add(Convert.ToString(rd[1]));
                        list_det.Add(Convert.ToString(rd[2]));
                        list_det.Add(Convert.ToString(rd[3]));
                        list_det.Add(Convert.ToString(rd[4]));
                        list_det.Add(Convert.ToString(rd[5]));

                    }

                    BookDetailes_FRM.name.Text = list_det[0];
                    BookDetailes_FRM.auther.Text = list_det[1];
                    BookDetailes_FRM.price.Text = list_det[2] + " $";
                    BookDetailes_FRM.cat.Text = list_det[3];
                    BookDetailes_FRM.date.Text = list_det[4];
                    BookDetailes_FRM.number.Text = list_det[5];
                    con.Close();
                    //read image from DB
                    con.Open();
                    cmd.CommandText = "SELECT COVER FROM BOOKS WHERE ID=@IDIMAGE";
                    cmd.Parameters.AddWithValue("@IDIMAGE", Convert.ToInt32(txt_search.Text));
                    byte[] img = (byte[])cmd.ExecuteScalar();
                    MemoryStream ma = new MemoryStream();
                    ma.Write(img, 0, img.Length);
                    BookDetailes_FRM._Cover.Image = Image.FromStream(ma);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();

                }
                cmd.Parameters.Clear();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

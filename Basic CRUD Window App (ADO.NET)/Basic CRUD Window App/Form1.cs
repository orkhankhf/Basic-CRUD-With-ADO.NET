using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_CRUD_Window_App
{
    public partial class Form1 : Form
    {
        Connection obj = new Connection();
        public int id;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ad = textBox1.Text;
            var soyad = textBox2.Text;
            var yas = Convert.ToInt32(textBox3.Text);
            var command = $"INSERT INTO [User] (Name, Surname, Age) VALUES ('{ad}', '{soyad}', {yas})";
            SqlCommand com = new SqlCommand(command, obj.Con);
            com.ExecuteNonQuery();
            MessageBox.Show("Elave Olundu !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var command = "SELECT * FROM [User]";
            SqlCommand com = new SqlCommand(command, obj.Con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(ds);
            textBox4.Text = "";
            for (int a = 0; a<ds.Tables[0].Rows.Count; a++)
            {
                textBox4.Text += ds.Tables[0].Rows[a]["Id"].ToString() + " " + ds.Tables[0].Rows[a]["Name"].ToString() + " " + ds.Tables[0].Rows[a]["Surname"].ToString() + " " + ds.Tables[0].Rows[a]["Age"].ToString()+"\r\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(id != null)
            {
                var command = $"DELETE FROM [User] WHERE Id = {id}";
                SqlCommand com = new SqlCommand(command, obj.Con);
                com.ExecuteNonQuery();
                textBox5.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var command = "SELECT Id FROM [User]";
            SqlCommand com = new SqlCommand(command, obj.Con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(ds);
            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[a]["Id"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = Convert.ToInt16(comboBox1.SelectedItem);
            var command = $"SELECT * FROM [User] WHERE id = {id}";
            SqlCommand com = new SqlCommand(command, obj.Con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(ds);
            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
            {
                if (Convert.ToInt16(ds.Tables[0].Rows[a]["Id"]) == id)
                {
                    textBox5.Text = ds.Tables[0].Rows[a]["Id"].ToString() + "\r\n" + ds.Tables[0].Rows[a]["Name"].ToString() + "\r\n" + ds.Tables[0].Rows[a]["Surname"].ToString() + "\r\n" + ds.Tables[0].Rows[a]["Age"].ToString();
                } 
            }
        }
    }
    class Connection
    {
        public string Str;
        public SqlConnection Con;

        public Connection()
        {
            this.Str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\P104\Documents\DataBank.mdf;Integrated Security=True;Connect Timeout=30";
            this.Con = new SqlConnection(Str);
            Con.Open();
        }
        ~Connection()
        {
            this.Con.Close();
        }
    }
    
}

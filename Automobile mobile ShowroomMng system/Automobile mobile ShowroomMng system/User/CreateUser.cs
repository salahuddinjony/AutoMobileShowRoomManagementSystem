using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Configuration;

namespace Automobile_mobile_ShowroomMng_system.User
{
    public partial class CreateUser : Form
    {
        private SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AM"].ConnectionString);
        public CreateUser()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateUser_Load(object sender, EventArgs e)
        {
            autogenerate();
            search_load();
            grid_Load();
        }
        public void new1()
    {
        textBoxUserId.Text = "";
        textBoxUserName.Text = "";
        textBoxPasswd.Text = "";
        textBoxUserName.Focus();
    }
        private void autogenerate()
        {
            string num = "UID0000";
            cn.Open();
            string qry = "select id from tbl_user";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                num = reader["id"].ToString();
            }
            num = string.Format("UID{0}", (Convert.ToInt32(num.Substring(4)) + 1).ToString("D4"));
            textBoxUserId.Text = num;
            cn.Close();
        }
        public void search_load()
        {
            comboBoxSearch.Items.Clear();

            string load = string.Format("SELECT userName from tbl_user ");
            SqlCommand comd = new SqlCommand(load, cn);
            cn.Close();
            cn.Open();
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                comboBoxSearch.Items.Add(reader[0]);
            }
            cn.Close();
        }
        public void grid_Load()
        {
            dataGridViewCreateUser.Rows.Clear();
            string load = string.Format("SELECT id,userName,passwd from tbl_user");
            SqlCommand comd = new SqlCommand(load, cn);
            cn.Close();
            cn.Open();
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                dataGridViewCreateUser.Rows.Add(reader[0], reader[1], reader[2]);
            }
            cn.Close();


        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            new1();
            comboBoxSearch.Text = "";
            autogenerate();
            search_load();
            grid_Load();
            buttonsave.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            
            autogenerate();
            SqlCommand insert = new SqlCommand("INSERT into tbl_user(id,userName,passwd) values(@id,@userName,@passwd)", cn);

            insert.Parameters.AddWithValue("@id", textBoxUserId.Text);
            insert.Parameters.AddWithValue("@userName", textBoxUserName.Text);
            insert.Parameters.AddWithValue("@passwd", textBoxPasswd.Text);
            cn.Close();
            cn.Open();
            new1();
            
          try
          {
                insert.ExecuteNonQuery();
                MessageBox.Show("User Add successfully...");
           }
            catch
            {
                MessageBox.Show("Try Again!!");
            }
            cn.Close();
            search_load();
            grid_Load();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string load = string.Format("SELECT * FROM tbl_user WHERE userName=@userName", cn);
            SqlCommand command = new SqlCommand(load, cn);
            command.Parameters.AddWithValue("@userName", comboBoxSearch.Text);
            cn.Close();
            cn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBoxUserId.Text = reader[0].ToString();
                textBoxUserName.Text = reader[1].ToString();
                textBoxPasswd.Text = reader[2].ToString();

            }
            cn.Close();
        }

        private void comboBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonsave.Enabled = false;
            buttonUpdate.Enabled=true;
            buttonDelete.Enabled = true;

            string load = string.Format("SELECT * FROM tbl_user WHERE userName=@userName", cn);
            SqlCommand command = new SqlCommand(load, cn);
            command.Parameters.AddWithValue("@userName", comboBoxSearch.Text);
            cn.Close();
            cn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBoxUserId.Text = reader[0].ToString();
                textBoxUserName.Text = reader[1].ToString();
                textBoxPasswd.Text = reader[2].ToString();

            }
            cn.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("UPDATE tbl_user SET id=@id,userName=@userName,passwd=@passwd WHERE id=@id", cn);
            update.Parameters.AddWithValue("@id", textBoxUserId.Text);
            update.Parameters.AddWithValue("@userName", textBoxUserName.Text);
            update.Parameters.AddWithValue("@passwd", textBoxPasswd.Text);
            cn.Close();
            cn.Open();
            try
            {
                update.ExecuteNonQuery();
                MessageBox.Show("Data Update Success");
            }
            catch
            {
                MessageBox.Show("Error!!");
            }
            cn.Close();
            grid_Load();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlCommand del = new SqlCommand("DELETE from tbl_user where id=@id", cn);
            del.Parameters.AddWithValue("@id", textBoxUserId.Text);
            cn.Close();
            cn.Open();
            try
            {
                del.ExecuteNonQuery();
                MessageBox.Show("Delete");

            }
            catch
            {
                cn.Close();
                MessageBox.Show("Not Delete");

            }
            cn.Close();
            search_load();
            grid_Load();
        }

        private void dataGridViewCreateUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonsave.Enabled = false;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewCreateUser.Rows[e.RowIndex];
                textBoxUserId.Text = row.Cells["column1"].Value.ToString();
                textBoxUserName.Text = row.Cells["column2"].Value.ToString();
                textBoxPasswd.Text = row.Cells["column3"].Value.ToString();
            }
        }
    }
}

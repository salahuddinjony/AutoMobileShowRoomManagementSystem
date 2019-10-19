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

namespace Automobile_mobile_ShowroomMng_system.Panel
{
    public partial class Qaualification : Form
    {
        private SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AM"].ConnectionString);

        public Qaualification()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void Qaualification_Load(object sender, EventArgs e)
        {
            autogenerate();
            search_load();
        }

        public void search_load()
        {
            comboBoxSearch.Items.Clear();

            string load = string.Format("SELECT name from tbl_qual");
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
        public void new1()
        {
            textBoxId.Text = "";
            textBoxName.Text = "";
            textBoxName.Focus();
        }
        private void autogenerate()
        {
            string num = "ID0000";
            cn.Open();
            string qry = "select id from tbl_qual";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                num = reader["id"].ToString();
            }
            num = string.Format("ID{0}", (Convert.ToInt32(num.Substring(4)) + 1).ToString("D4"));
            textBoxId.Text = num;
            cn.Close();
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            SqlCommand insert = new SqlCommand("INSERT into tbl_qual(id,name) values(@id,@name)", cn);

            insert.Parameters.AddWithValue("@id", textBoxId.Text);
            insert.Parameters.AddWithValue("@name", textBoxName.Text);
            cn.Close();
            cn.Open();

            try
            {
                insert.ExecuteNonQuery();
                MessageBox.Show("Add successfully...");
            }
            catch
            {
                MessageBox.Show("Try Again!!");
            }
            cn.Close();
            search_load();
         
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            new1();
            comboBoxSearch.Text = "";
            autogenerate();
            search_load();
            buttonsave.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;

        }

        private void comboBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonsave.Enabled = false;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;

            string load = string.Format("SELECT * FROM tbl_qual WHERE name=@name", cn);
            SqlCommand command = new SqlCommand(load, cn);
            command.Parameters.AddWithValue("@name", comboBoxSearch.Text);
            cn.Close();
            cn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBoxId.Text = reader[0].ToString();
                textBoxName.Text = reader[1].ToString();

            }
            cn.Close();
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlCommand del = new SqlCommand("DELETE from tbl_qual where id=@id", cn);
            del.Parameters.AddWithValue("@id", textBoxId.Text);
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
            new1();
            comboBoxSearch.Text= "";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("UPDATE tbl_qual SET id=@id,name=@name WHERE id=@id", cn);
            update.Parameters.AddWithValue("@id", textBoxId.Text);
            update.Parameters.AddWithValue("name", textBoxName.Text);
          
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
            search_load();
        }
    }
}

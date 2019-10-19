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

namespace Automobile_mobile_ShowroomMng_system.Employee
{
    public partial class EmployeeInfo : Form
    {
        private SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AM"].ConnectionString);
        public EmployeeInfo()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {
            name_load();
            post_load();

        }
         public void name_load()
        {
            comboBoxQualification.Items.Clear();

            string load = string.Format("SELECT name from tbl_qual ");
            SqlCommand comd = new SqlCommand(load, cn);
            cn.Close();
            cn.Open();
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                comboBoxQualification.Items.Add(reader[0]);
            }
            cn.Close();
        }
         public void post_load()
        {
            comboBoxPost.Items.Clear();

            string load = string.Format("SELECT name from tbl_post ");
            SqlCommand comd = new SqlCommand(load, cn);
            cn.Close();
            cn.Open();
            SqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                comboBoxQualification.Items.Add(reader[0]);
            }
            cn.Close();
        }
         public string imgloc;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dateTimePickerDOB_ValueChanged(object sender, EventArgs e)
        {
            textBoxDate.Text = Convert.ToString(dateTimePickerDOB.Text);
        }

        private void textBoxDate_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxPost_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBoxQualification_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dig = new OpenFileDialog();
                dig.Filter = "Image files(*.jpg;*.jpeg;*.gif;)|*.jpg;*.jpeg;*.gif";
                if (dig.ShowDialog() == DialogResult.OK)
                {
                    imgloc = dig.FileName.ToString();
                    pictureBox1.ImageLocation = imgloc;
                    //pictureBox1.Visible = true;
                    //pictureBox2.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

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
            search_load();
            autogenerate();

        }
        private void autogenerate()
        {
            string num = "EMPID0000";
            cn.Open();
            string qry = "select id from tbl_empinfo";
            SqlCommand cmd = new SqlCommand(qry, cn);
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                num = reader["id"].ToString();
            }
            num = string.Format("EMPID{0}", (Convert.ToInt32(num.Substring(5)) + 1).ToString("D5"));
            textBoxEmpId.Text = num;
            cn.Close();
        }
        public void search_load()
        {
            comboBoxSearch.Items.Clear();

            string load = string.Format("SELECT id from tbl_empinfo ");
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
                comboBoxPost.Items.Add(reader[0]);
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
                    pictureBoximg.ImageLocation = imgloc;
                    //pictureBox1.Visible = true;
                    //pictureBox2.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;

                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                string sql = "INSERT INTO tbl_empinfo(id,name,fname,mname,religion,bgroup,gender,nid,dob,qual,post,phn,adds,img)VALUES('" +
                textBoxEmpId.Text + "','" + textBoxEmpName.Text + "','" + textBoxFName.Text + "','" + textBoxMName.Text + "','" + comboBoxReligion.Text + "','" +
                comboBoxBloodGroup.Text + "','" + comboBoxGender.Text + "','" + textBoxNID.Text + "','" + textBoxDate.Text + "','" + comboBoxQualification.Text + "','" + comboBoxPost.Text + "','" + textBoxPhone.Text + "','" + textBoxAddress.Text + "',@img)";
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.Add(new SqlParameter("@img", img));

                    int x = cmd.ExecuteNonQuery();
                    cn.Close();


                    MessageBox.Show("Employee Added successfully... ");
                    autogenerate();
                   

                }
            }
            catch
            {
                SqlCommand insert = new SqlCommand("INSERT into tbl_empinfo(id,name,fname,mname,religion,bgroup,gender,nid,dob,qual,post,phn,adds) values(@id,@name,@fname,@mname,@religion,@bgroup,@gender,@nid,@dob,@qual,@post,@phn,@adds)", cn);

                insert.Parameters.AddWithValue("@id", textBoxEmpId.Text);
                insert.Parameters.AddWithValue("@name", textBoxEmpName.Text);
                insert.Parameters.AddWithValue("@fname", textBoxFName.Text);
                insert.Parameters.AddWithValue("@mname", textBoxMName.Text);
                insert.Parameters.AddWithValue("@religion", comboBoxReligion.Text);
                insert.Parameters.AddWithValue("@bgroup", comboBoxBloodGroup.Text);
                insert.Parameters.AddWithValue("@gender", comboBoxGender.Text);
                insert.Parameters.AddWithValue("@nid", textBoxNID.Text);
                insert.Parameters.AddWithValue("@dob", textBoxDate.Text);
                insert.Parameters.AddWithValue("@qual", comboBoxQualification.Text);
                insert.Parameters.AddWithValue("@post", comboBoxPost.Text);
                insert.Parameters.AddWithValue("@phn", textBoxPhone.Text);
                insert.Parameters.AddWithValue("@adds", textBoxAddress.Text);
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
                autogenerate();
            }


        }
        public void new1()
        {
            textBoxEmpName.Text = "";
            textBoxFName.Text = "";
            textBoxMName.Text = "";
            comboBoxReligion.Text = "";
            comboBoxBloodGroup.Text = "";
            textBoxNID.Text = "";
            textBoxDate.Text = "";
            comboBoxGender.Text = "";
            comboBoxQualification.Text = "";
            comboBoxPost.Text = "";
            textBoxPhone.Text = "";
            textBoxAddress.Text = "";
            pictureBoximg.Image = null;





        }
        private void buttonNew_Click(object sender, EventArgs e)
        {
            comboBoxSearch.Text = "";
            new1();
            autogenerate();
            search_load();
            buttonsave.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void pictureBoximg_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonsave.Enabled = false;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
            try
            {


                string load = string.Format("SELECT * FROM tbl_empinfo WHERE id=@id", cn);
                SqlCommand command = new SqlCommand(load, cn);
                command.Parameters.AddWithValue("@id", comboBoxSearch.Text);
                cn.Close();
                cn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBoxEmpId.Text = reader[0].ToString();
                    textBoxEmpName.Text = reader[1].ToString();
                    textBoxFName.Text = reader[2].ToString();
                    textBoxMName.Text = reader[3].ToString();
                    comboBoxReligion.Text = reader[4].ToString();
                    comboBoxBloodGroup.Text = reader[5].ToString();
                    comboBoxGender.Text = reader[6].ToString();
                    textBoxNID.Text = reader[7].ToString();
                    textBoxDate.Text = reader[8].ToString();
                    comboBoxQualification.Text = reader[9].ToString();
                    comboBoxPost.Text = reader[10].ToString();
                    textBoxPhone.Text = reader[11].ToString();
                    textBoxAddress.Text = reader[12].ToString();
                    byte[] img = (byte[])reader[13];
                    MemoryStream ms = new MemoryStream(img); ms.Seek(5, SeekOrigin.Begin);
                    pictureBoximg.Image = Image.FromStream(ms);


                }
                cn.Close();
            }
            catch
            {
                pictureBoximg.Image = null;
                string load = string.Format("SELECT * FROM tbl_empinfo WHERE id=@id", cn);
                SqlCommand command = new SqlCommand(load, cn);
                command.Parameters.AddWithValue("@id", comboBoxSearch.Text);
                cn.Close();
                cn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBoxEmpId.Text = reader[0].ToString();
                    textBoxEmpName.Text = reader[1].ToString();
                    textBoxFName.Text = reader[2].ToString();
                    textBoxMName.Text = reader[3].ToString();
                    comboBoxReligion.Text = reader[4].ToString();
                    comboBoxBloodGroup.Text = reader[5].ToString();
                    comboBoxGender.Text = reader[6].ToString();
                    textBoxNID.Text = reader[7].ToString();
                    textBoxDate.Text = reader[8].ToString();
                    comboBoxQualification.Text = reader[9].ToString();
                    comboBoxPost.Text = reader[10].ToString();
                    textBoxPhone.Text = reader[11].ToString();
                    textBoxAddress.Text = reader[12].ToString();



                }
                cn.Close();
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {

            

            byte[] img = null;
            FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            SqlCommand update = new SqlCommand("UPDATE tbl_empinfo SET id=@id,name=@name,fname=@fname,mname=@mname,religion=@religion,bgroup=@bgroup,gender=@gender,nid=@nid,dob=@dob,qual=@qual,post=@post,phn=@phn,adds=@adds,img=@img WHERE id=@id", cn);
            update.Parameters.AddWithValue("@id", textBoxEmpId.Text);
            update.Parameters.AddWithValue("@name", textBoxEmpName.Text);
            update.Parameters.AddWithValue("@fname", textBoxFName.Text);
            update.Parameters.AddWithValue("@mname", textBoxMName.Text);
            update.Parameters.AddWithValue("@religion", comboBoxReligion.Text);
            update.Parameters.AddWithValue("@bgroup", comboBoxBloodGroup.Text);
            update.Parameters.AddWithValue("@gender", comboBoxGender.Text);
            update.Parameters.AddWithValue("@nid", textBoxNID.Text);
            update.Parameters.AddWithValue("@dob", textBoxDate.Text);
            update.Parameters.AddWithValue("@qual", comboBoxQualification.Text);
            update.Parameters.AddWithValue("@post", comboBoxPost.Text);
            update.Parameters.AddWithValue("@phn", textBoxPhone.Text);
            update.Parameters.AddWithValue("@adds", textBoxAddress.Text);
            update.Parameters.AddWithValue("@img", img);
            cn.Close();
            cn.Open();
            try
            {
                update.ExecuteNonQuery();
                MessageBox.Show("Data Update Success");
                comboBoxSearch.Items.Clear();
                //search_load();
            }
            catch
            {
                MessageBox.Show("Error!!");
            }
            cn.Close();
            search_load();

            }
            catch
            {
                SqlCommand update = new SqlCommand("UPDATE tbl_empinfo SET id=@id,name=@name,fname=@fname,mname=@mname,religion=@religion,bgroup=@bgroup,gender=@gender,nid=@nid,dob=@dob,qual=@qual,post=@post,phn=@phn,adds=@adds WHERE id=@id", cn);
                update.Parameters.AddWithValue("@id", textBoxEmpId.Text);
                update.Parameters.AddWithValue("@name", textBoxEmpName.Text);
                update.Parameters.AddWithValue("@fname", textBoxFName.Text);
                update.Parameters.AddWithValue("@mname", textBoxMName.Text);
                update.Parameters.AddWithValue("@religion", comboBoxReligion.Text);
                update.Parameters.AddWithValue("@bgroup", comboBoxBloodGroup.Text);
                update.Parameters.AddWithValue("@gender", comboBoxGender.Text);
                update.Parameters.AddWithValue("@nid", textBoxNID.Text);
                update.Parameters.AddWithValue("@dob", textBoxDate.Text);
                update.Parameters.AddWithValue("@qual", comboBoxQualification.Text);
                update.Parameters.AddWithValue("@post", comboBoxPost.Text);
                update.Parameters.AddWithValue("@phn", textBoxPhone.Text);
                update.Parameters.AddWithValue("@adds", textBoxAddress.Text);
       
                cn.Close();
                cn.Open();
                try
                {
                    update.ExecuteNonQuery();
                    MessageBox.Show("Data Update Success");
                    comboBoxSearch.Items.Clear();
                    //search_load();
                }
                catch
                {
                    MessageBox.Show("Error!!");
                }
                cn.Close();
            }
           }
         


        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBoximg.Image = null;
        }
    }
}

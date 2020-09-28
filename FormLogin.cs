using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teacher_s_Student_And_Class_Management_System
{
    struct ID
    {
        public string AccountType
        {
            get;set;
        }
        public string Year
        {
            get;set;
        }
        public string UniqueNumber
        {
            get;set;
        }
        public ID (string num, string y)
        {
            AccountType = "";
            UniqueNumber = num;
            Year = y;
        }

        internal string GetId ()
        {
            return AccountType+"_"+UniqueNumber+"_"+Year;
        }

    }
    public partial class Login : Form
    {
        private string query = "";
        //DataAccess da = new DataAccess();   //Connecting DB
        //DataSet ds;

        private ID id;

        internal ID Id
        {
            get;set;
        }
        public Login()
         {
            InitializeComponent();
            panelRegistration.Hide();
            panelLogin.Show();
            IdSetter();
        }

        internal void IdSetter()
        {
            query = "SELECT * FROM login";
            DataAccess.Ds = DataAccess.ExecuteQuery(query);
            string uniqueNum = DataAccess.Ds.Tables[0].Rows.Count.ToString();  
            //MessageBox.Show(uniqueNum);
            string num = uniqueNum;
            uniqueNum = "";
            string year = DateTime.Today.Year.ToString();
            id = new ID(num, year);
        }


        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            panelLogin.Show();
            panelRegistration.Hide();
        }

        private void btnNewRegistration_Click(object sender, EventArgs e)
        {
            panelLogin.Hide();
            panelRegistration.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            query = @"select * from login where id = '" + this.txtId.Text + "' and password = '" + this.txtLoginPassword.Text + "';";
            DataAccess.Ds = DataAccess.ExecuteQuery(query);
            //MessageBox.Show(DataAccess.Ds.Tables[0].Rows.Count.ToString());
            if (DataAccess.Ds.Tables[0].Rows.Count == 1)                       //login approve checker
            {
                string name = DataAccess.Ds.Tables[0].Rows[0][2].ToString();
                accountType = DataAccess.Ds.Tables[0].Rows[0][3].ToString();
                MessageBox.Show("Login approved for " +name);    //if approves then show msgbox then go to Home
                Home home = new Home(txtId.Text, name, accountType);
                home.FormClosed += new FormClosedEventHandler(home_FormClosed);     //// event for Home closes, login form also closes
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login Invalid");
            }
        }
        private void home_FormClosed(object sender, FormClosedEventArgs e)      // When Home closes, login form also closes
        {
            this.Close();
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        internal string accountType;
        private void btnRegistration_Click(object sender, EventArgs e)
        {
            //string accountType;
            if (radiobtnAdmin.Checked == true)
                accountType = "Admin";
            else accountType = "Teacher";
            bool passwordOk=false;
            string finalPassword="";
            if (txtPassRegistration.Text.Equals(txtConfirmPass.Text))
            {
                passwordOk = true;
            }
            else MessageBox.Show("Password doesn't match.");
            if (passwordOk)
            {
                finalPassword = txtConfirmPass.Text;

                //    query = "INSERT INTO LOGIN( id, password, name, acc_type)  VALUES (@id,@password,@name,@accountType);";

                //    SqlConnection con = new SqlConnection(@"Data Source=AVI-NOTEBOOK\SQLEXPRESS;Initial Catalog=TSCMS;Integrated Security=True");
                //    sqlConnection.Open();
                //    SqlCommand cmd = new SqlCommand("INSERT INTO LOGIN( id, password, name, acc_type)  VALUES (@id,@password,@name,@accountType)", con);
                //    cmd.Parameters.AddWithValue("@id", id.GetId());
                //    cmd.Parameters.AddWithValue("@password", txtPassRegistration.Text);
                //    cmd.Parameters.AddWithValue("@name", txtNameRegistration.Text);
                //    cmd.Parameters.AddWithValue("@accountType", accountType);
                //    int rowCount= cmd.ExecuteNonQuery();
                //    if (rowCount > 0)
                //    {
                //        MessageBox.Show("registration successful. login to access.");
                //    }
                //    else MessageBox.Show("registration failed. input data correctly.");
                //
                query = "INSERT INTO LOGIN( id, password, name, acc_type) VALUES ('" + id.GetId() + "','" + finalPassword + "','" + this.txtNameRegistration.Text + "','" + accountType + "');";
                int count = DataAccess.ExecuteUpdateQuery(query);
                if (count == 1)
                {
                    MessageBox.Show("Registration Successful!\nPlease Login to use features.Remember ID and Password for login.");
                }
                else
                {
                    MessageBox.Show("Registration error! Try again. Fill all the fields.");
                }
            }

        }

        private void radiobtnAdmin_CheckedChanged(object sender, EventArgs e)
        {
            id.AccountType = "A";
            txtIdRegistration.Text = "Automated ID: " + id.GetId();

        }

        private void radiobtnTeacher_CheckedChanged(object sender, EventArgs e)
        {
            id.AccountType = "T";
            txtIdRegistration.Text = "Automated ID: " + id.GetId();
        }

        private void txtPassRegistration_TextChanged(object sender, EventArgs e)
        {
            txtPassRegistration.UseSystemPasswordChar = true;
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            txtConfirmPass.UseSystemPasswordChar = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void txtLoginPassword_TextChanged(object sender, EventArgs e)
        {
            txtLoginPassword.UseSystemPasswordChar = true;
        }

        private void txtId_MouseClick(object sender, MouseEventArgs e)
        {
            txtId.Clear();
        }

        private void txtLoginPassword_MouseClick(object sender, MouseEventArgs e)
        {
            txtLoginPassword.Clear();
        }

        private void txtNameRegistration_MouseClick(object sender, MouseEventArgs e)
        {
            txtNameRegistration.Clear();
        }

        private void txtPassRegistration_MouseClick(object sender, MouseEventArgs e)
        {
            txtPassRegistration.Clear();
        }

        private void txtConfirmPass_MouseClick(object sender, MouseEventArgs e)
        {
            txtConfirmPass.Clear();
        }
    }
}

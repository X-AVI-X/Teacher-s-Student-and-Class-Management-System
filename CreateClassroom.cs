using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teacher_s_Student_And_Class_Management_System
{
    public partial class CreateClassroom : Form
    {
        private string query = "";
        //DataAccess da = new DataAccess();   //Connecting DB
        //DataSet ds;

        public static int totalClassNumber;

        internal string Id
        {
            get;set;
        }
        public CreateClassroom(string id)
        {
            Id = id;
            InitializeComponent();
        }

        private void btnCreateClass_Click(object sender, EventArgs e)
        {
                DataAccess.Ds = DataAccess.ExecuteQuery("Select *from "+Id+"classrooms;");
                totalClassNumber = DataAccess.Ds.Tables[0].Rows.Count;
                //MessageBox.Show(totalClassNumber.ToString());
                int u = DataAccess.ExecuteUpdateQuery(@"INSERT INTO " + Id + "classrooms" + " VALUES(" +
                    "" + (totalClassNumber+1) + ", " +
                    "'" + txtClassName.Text + "'); ");
                if (u == 1)
                {
                    MessageBox.Show("Classroom created successfully!");
                    this.Close();
                }
                else MessageBox.Show("Error!");
        }
    }
}

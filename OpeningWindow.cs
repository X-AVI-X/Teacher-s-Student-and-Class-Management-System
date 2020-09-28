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
    public partial class OpeningWindow : Form
    {
        public OpeningWindow()
        {
            InitializeComponent();
        }

        private void OpeningWindow_Load(object sender, EventArgs e)
        {
            Timer MyTimer = new Timer
            {
                Interval = (3 * 1000)                       // 3 sec
            };
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

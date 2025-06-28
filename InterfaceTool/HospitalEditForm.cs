using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace InterfaceConnect
{
    public partial class HospitalEditForm : Form
    {

        public HospitalEditForm()
        {
            InitializeComponent();

            textBox1.Text = AppSettingsTool.ReadSetting("HospitalName");
            textBox2.Text = AppSettingsTool.ReadSetting("RisConnectString");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hospitalName = textBox1.Text;
            string connection = textBox2.Text;

            if(string.IsNullOrEmpty(hospitalName))
            {
                MessageBox.Show("医院名称不允许为空！");
            }
            AppSettingsTool.AddUpdateAppSettings("HospitalName", hospitalName);
            AppSettingsTool.AddUpdateAppSettings("RisConnectString", connection);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

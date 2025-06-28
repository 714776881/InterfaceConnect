using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceConnect
{
    public partial class ApiConfigForm : Form
    {
        public ApiConfigForm()
        {
            InitializeComponent();
        }
        private InterfaceConfig _config;
        public void LoadConfig(InterfaceConfig config)
        {
            if(config != null)
            {
                textBox1.Text = config.Action;
                textBox5.Text = config.Name;
                if (config.Type == ConfigType.DB)
                {
                    comboBox1.Text = "数据库(Oracle、MySql、SqlService)";
                }
                if (config.Type == ConfigType.HTTP)
                {
                    comboBox1.Text = "网络(Http、WebService)";
                }
                if(config.Type == ConfigType.DATA)
                {
                    comboBox1.Text = "获取数据";
                }
                if(config.Type == ConfigType.SCRIPT)
                {
                    comboBox1.Text = "脚本(Python)";
                }
                textBox3.Text = config.Category;
                textBox2.Text = config.HitRule;
                textBox4.Text = config.PreAction;
                comboBox1.Enabled = false;
                _config = config;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string action = textBox1.Text;
            string name = textBox5.Text;
            string type = comboBox1.Text;
            string category = textBox3.Text;
            string hitRule = textBox2.Text;
            string preAction = textBox4.Text;

            if(string.IsNullOrEmpty(action) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("请填写Action、分类！");
                return;
            }
            // 判断是否
            InterfaceConfig api =  ConfigManager.Instance.GetInterfaceConfig(action);
            if(_config == null && api != null)
            {
                MessageBox.Show("Action已经存在！");
                return;
            }

            if (_config != null)
            {
                // 修改
                if (action != _config.Action)
                {
                    ConfigManager.Instance.RemoveApiAsync(_config.Action);
                    ControlManager.Instance.Update(_config.Action, action);
                }
                api = _config;
            }

            ConfigType apiType = ConfigType.HTTP;
            if(type == "网络(Http、WebService)")
            {
                apiType = ConfigType.HTTP;
            }
            else if (type == "数据库(Oracle、MySql、SqlService)")
            {
                apiType = ConfigType.DB;
            }
            else if (type == "获取数据")
            {
                apiType = ConfigType.DATA;
            }
            else if(type == "脚本(Python)")
            {
                apiType = ConfigType.SCRIPT;
            }
            if(api == null)
            {
                api = ConfigFactory.CreateApi(apiType);
            }
            api.Type = apiType;
            api.Action = action;
            api.Category = category;
            api.HitRule = hitRule;
            api.PreAction = preAction;
            api.Name = name;
            if (ConfigManager.Instance.SaveApiAsync(api))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("未保存成功！");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void ApiConfigForm_Load(object sender, EventArgs e)
        {

        }
    }
}

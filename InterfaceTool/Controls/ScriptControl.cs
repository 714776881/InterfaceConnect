using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace InterfaceConnect
{
    public partial class ScriptControl : UserControl
    {
        public ScriptControl(InterfaceConfig api)
        {
            InitializeComponent();
            if(api == null)
            {
                MessageBox.Show("API加载失败！");
                return;
            }
            this.api = (ScriptConfig)api;
            LoadApiConfig(this.api);
        }
        private ScriptConfig api;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                api = GetTempApi(api);
                if (ConfigManager.Instance.SaveApiAsync(api) == false)
                {
                    MessageBox.Show("保存失败！");
                }
                else
                {
                    MessageBox.Show("保存成功！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadApiConfig(ScriptConfig api)
        {
            groupBox1.Text = "脚本（" + api.Action + ")";
            text_input.Text = api.MockData;
            text_script.Text = api.Script;
            text_template.Text = api.Template;
            textBox_outTemplate.Text = api.OuterTemplate;
            if(api.MessageType == InfoType.JSON)
            {
                comboBox_messageType.Text = "JSON";
            }
            else
            {
                comboBox_messageType.Text = "TEXT";
            }
            if (api.Configs != null)
            {
                foreach (var item in api.Configs)
                {
                    dataGridView_configs.Rows.Add(item.Key, item.Value, item.Description);
                }
            }
        }
        private ScriptConfig GetTempApi(ScriptConfig api)
        {
            if(api == null)
            {
                throw new ArgumentNullException("GetTempApi's HttpApi is null");
            }
            var tempApi = JsonTool.Clone<ScriptConfig>(api);
            tempApi.MockData = text_input.Text;
            tempApi.Script = text_script.Text;
            tempApi.Template = text_template.Text;
            tempApi.OuterTemplate = textBox_outTemplate.Text;
            if(comboBox_messageType.Text == "JSON")
            {
                tempApi.MessageType = InfoType.JSON;
            }
            else
            {
                tempApi.MessageType = InfoType.TEXT;
            }
            List<KeyValue> configs = new List<KeyValue>();
            foreach (DataGridViewRow row in dataGridView_configs.Rows)
            {
                if (row.IsNewRow) continue;
                string key = CellValue(row.Cells[0]);
                string value = CellValue(row.Cells[1]);
                string description = CellValue(row.Cells[2]);
                KeyValue keyValue = new KeyValue(key, value, description);
                configs.Add(keyValue);
            }
            tempApi.Configs = configs;
            return tempApi;
        }
        private string CellValue(DataGridViewCell cell)
        {
            if (cell.Value == null)
            {
                return "";
            }
            return cell.Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                var config = GetTempApi(api);
                var message = text_input.Text;
                var command = InterfaceInvoker.ConnectorManager.GetConnector(config, true);
                stopwatch.Start();
                textBox_result.Text = InterfaceInvoker.Invoke(config.Action, message);
                stopwatch.Stop();
                button_test.ForeColor = PrintColorByRunSpeed(stopwatch.ElapsedMilliseconds);
                button_test.Text = "测试" + stopwatch.ElapsedMilliseconds + "ms";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Color PrintColorByRunSpeed(long time)
        {
            if (time < 80)
            {
                return Color.HotPink;
            }
            else if (time < 500)
            {
                return Color.Orange;
            }
            else
            {
                return Color.Red;
            }
        }

        private void text_script_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // 添加四个空格到文本框
                //text_script.SelectedText += "    ";

                // 防止Tab键继续移动到下一个控件
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 检查是否按下了 Tab 键
            if (keyData == Keys.Tab)
            {
                // 执行你想要触发的事件
                // 在这里可以调用你的函数或执行其他操作
                return true; // 返回 true 表示已经处理了该键，阻止继续传递给其他控件
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void text_script_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true; // 指示 Tab 键是一个输入键而不是控件导航键
            }
        }
    }
}

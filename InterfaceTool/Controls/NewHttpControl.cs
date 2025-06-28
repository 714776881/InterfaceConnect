using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace CommMediator
{
    public partial class NewHttpControl : UserControl
    {
        public NewHttpControl(Config api)
        {
            InitializeComponent();
            if(api == null)
            {
                MessageBox.Show("API加载失败！");
                return;
            }
            this.api = (HttpConfig)api;
            LoadApiConfig(this.api);
        }

        private HttpConfig api;

        private async void button4_Click(object sender, EventArgs e)
        {
            api = GetTempApi(api);
            if (await ConfigManager.Instance.SaveApiAsync(api) == false)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void LoadApiConfig(HttpConfig api)
        {
            groupBox1.Text = "HTTP通信（" + api.Action + ")";
            if(api.Method == RestSharp.Method.POST)
            {
                comboBox1.Text = "POST";
            }
            else
            {
                comboBox1.Text = "GET";
            }
            if(api.InParseConfigs != null)
            {
                foreach (var config in api.InParseConfigs)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.SET ? "集合" : "字符串", config.Formula, config.TargetPosition };
                    //dataGridView3.Rows.Add(obj);
                }
            }
            if (api.OutParseConfigs != null)
            {
                foreach (var config in api.OutParseConfigs)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.SET ? "集合" : "字符串", config.Formula, config.TargetPosition };
                    //dataGridView5.Rows.Add(obj);
                }
            }
            // 消息模板
            textBox2.Text = api.Template;
            // 测试数据
            textBox6.Text = api.MockData;
        }
        private HttpConfig GetTempApi(HttpConfig api)
        {
            if(api == null)
            {
                throw new ArgumentNullException("GetTempApi's HttpApi is null");
            }
            var tempApi = JsonHelper.Clone<HttpConfig>(api);
            // HTTP请求方法
            if (comboBox1.SelectedText == "POST")
            {
                tempApi.Method = RestSharp.Method.POST;
            }
            if (comboBox1.SelectedText == "GET")
            {
                tempApi.Method = RestSharp.Method.GET;
            }
            // 请求解析配置
            List<KeywordRule> inParseConfig = new List<KeywordRule>();
            //foreach (DataGridViewRow row in treeList1.)
            //{
            //    string key = CellValue(row.Cells[0]);

            //    KeyType keyType = KeyType.STRING;
            //    if (CellValue(row.Cells[1]) == "集合")
            //    {
            //        keyType = KeyType.SET;
            //    }
            //    else
            //    {
            //        keyType = KeyType.STRING;
            //    }
            //    string expression = CellValue(row.Cells[2]);
            //    string correspondNode = CellValue(row.Cells[3]);
            //    string value = CellValue(row.Cells[4]);
            //    KeywordRule config = new KeywordRule(key, keyType, expression, correspondNode);
            //    inParseConfig.Add(config);
            //}
            foreach(TreeListNode node in treeList1.Nodes)
            {

            }
            


            //tempApi.InParseConfigs = inParseConfig;
            //// 响应解析配置
            //List<KeywordRule> outParseConfigs = new List<KeywordRule>();
            //foreach (DataGridViewRow row in dataGridView5.Rows)
            //{
            //    string key = CellValue(row.Cells[0]);

            //    KeyType keyType = KeyType.STRING;
            //    if (CellValue(row.Cells[1]) == "集合")
            //    {
            //        keyType = KeyType.SET;
            //    }
            //    else
            //    {
            //        keyType = KeyType.STRING;
            //    }
            //    string expression = CellValue(row.Cells[2]);
            //    string correspondNode = CellValue(row.Cells[3]);
            //    string remark = CellValue(row.Cells[4]);
            //    KeywordRule config = new KeywordRule { Key = key, KeyType = keyType, Formula = expression, TargetPosition = correspondNode, Remark = remark };
            //    outParseConfigs.Add(config);
            //}
            //tempApi.OutParseConfigs = outParseConfigs;

            //// 请求参数
            //List<KeyValue> parameters = new List<KeyValue>();
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    string key = CellValue(row.Cells[0]);
            //    string value = CellValue(row.Cells[1]);
            //    string description = CellValue(row.Cells[2]);
            //    KeyValue keyValue = new KeyValue(key, value, description);
            //    parameters.Add(keyValue);
            //}
            //tempApi.Parmeters = parameters;
            //// 请求消息头
            //List<KeyValue> headers = new List<KeyValue>();
            //foreach (DataGridViewRow row in dataGridView2.Rows)
            //{
            //    string key = CellValue(row.Cells[0]);
            //    string value = CellValue(row.Cells[1]);
            //    string description = CellValue(row.Cells[2]);
            //    KeyValue keyValue = new KeyValue(key, value, description);
            //    headers.Add(keyValue);
            //}
            //tempApi.Hearders = headers;
            //// 消息类型，判断JSON、XMl、TEXT
            //string messageType = comboBox2.SelectedText;
            //// 消息模板
            //tempApi.Template = textBox2.Text;
            //// 测试数据
            //tempApi.MockData = textBox6.Text;
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
            try
            {
                HttpConfig tempApi = GetTempApi(api);
                var command = Invoker.APICommandManager.GetCommand(tempApi,true);
                var httpCommand = (CommandBase)command;
                var request = httpCommand.GetRequest(api.MockData);
                textBox3.Text = request;

                //var backInfo = httpCommand.Comm(request);

                var message = textBox4.Text;
                var response = httpCommand.GetResponse(message);
                textBox7.Text = response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}

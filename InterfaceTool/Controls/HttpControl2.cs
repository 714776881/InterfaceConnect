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
    public partial class HttpControl2 : UserControl
    {
        public HttpControl2(InterfaceConfig api)
        {
            InitializeComponent();
            if(api == null)
            {
                MessageBox.Show("API加载失败！");
                return;
            }
            this.api = (HttpInterfaceConfig)api;
            LoadApiConfig(this.api);
        }
        private HttpInterfaceConfig api;
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

        private void LoadApiConfig(HttpInterfaceConfig api)
        {
            groupBox1.Text = "HTTP接口（" + api.Action + ")";

            textBox1.Text = api.Url;

            if (api.Method == RestSharp.Method.POST)
            {
                comboBox1.Text = "POST";
            }
            else
            {
                comboBox1.Text = "GET";
            }
            if(api.Rules != null)
            {
                foreach (var config in api.Rules)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "对象", config.Formula, config.Position,config.Remark};
                    dataGridView3.Rows.Add(obj);
                }
            }
            if (api.DesRules != null)
            {
                foreach (var config in api.DesRules)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "对象", config.Formula, config.Position,config.Remark};
                    dataGridView5.Rows.Add(obj);
                }
            }
            if(api.Hearders != null)
            {
                foreach(var item in api.Hearders)
                {
                    dataGridView2.Rows.Add(item.Key, item.Value,item.Description);
                }
            }
            if(api.Parmeters != null)
            {
                foreach (var item in api.Parmeters)
                {
                    dataGridView1.Rows.Add(item.Key, item.Value,item.Description);
                }
            }
            if (api.Configs != null)
            {
                foreach (var item in api.Configs)
                {
                    dataGridView_configs.Rows.Add(item.Key, item.Value, item.Description);
                }
            }

            textBox_outerTemplate.Text = api.OuterTemplate;
            textBox_parsePath.Text = api.ParsePath;
            textBox_template.Text = api.Template;
            comboBox_infoType.Text = api.InfoType.ToString();
            comboBox_parseInfoType.Text = api.ParseInfoType.ToString();
            textBox_mockdata.Text = api.MockData;
        }
        private HttpInterfaceConfig GetTempApi(HttpInterfaceConfig api)
        {
            if(api == null)
            {
                throw new ArgumentNullException("GetTempApi's HttpApi is null");
            }
            var tempApi = JsonTool.Clone<HttpInterfaceConfig>(api);

            tempApi.Url = textBox1.Text;
            // HTTP请求方法
            if (comboBox1.Text == "POST")
            {
                tempApi.Method = RestSharp.Method.POST;
            }
            if (comboBox1.Text == "GET")
            {
                tempApi.Method = RestSharp.Method.GET;
            }
            // 请求解析配置
            List<DataSingleRule> inParseConfig = new List<DataSingleRule>();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow) continue;

                string key = CellValue(row.Cells[0]);
                KeyType keyType = KeyType.OBJECT;
                if (CellValue(row.Cells[1]) == "集合")
                {
                    keyType = KeyType.ARRAY;
                }
                else
                {
                    keyType = KeyType.OBJECT;
                }
                string expression = CellValue(row.Cells[2]);
                string correspondNode = CellValue(row.Cells[3]);
                string remark = CellValue(row.Cells[4]);
                DataSingleRule config = new DataSingleRule {Key = key,KeyType = keyType,Formula = expression, Position = correspondNode,Remark = remark };
                inParseConfig.Add(config);
            }
            tempApi.Rules = inParseConfig;
            // 响应解析配置
            List<DataSingleRule> outParseConfigs = new List<DataSingleRule>();
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.IsNewRow) continue;
                string key = CellValue(row.Cells[0]);
                KeyType keyType = KeyType.OBJECT;
                if (CellValue(row.Cells[1]) == "集合")
                {
                    keyType = KeyType.ARRAY;
                }
                else
                {
                    keyType = KeyType.OBJECT;
                }
                string expression = CellValue(row.Cells[2]);
                string correspondNode = CellValue(row.Cells[3]);
                string remark = CellValue(row.Cells[4]);
                DataSingleRule config = new DataSingleRule { Key = key, KeyType = keyType, Formula = expression, Position = correspondNode, Remark = remark };
                outParseConfigs.Add(config);
            }
            tempApi.DesRules = outParseConfigs;
            // 配置参数
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
            // 请求参数
            List<KeyValue> parameters = new List<KeyValue>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string key = CellValue(row.Cells[0]);
                string value = CellValue(row.Cells[1]);
                string description = CellValue(row.Cells[2]);
                KeyValue keyValue = new KeyValue(key, value, description);
                parameters.Add(keyValue);
            }
            tempApi.Parmeters = parameters;
            // 请求消息头
            List<KeyValue> headers = new List<KeyValue>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                string key = CellValue(row.Cells[0]);
                string value = CellValue(row.Cells[1]);
                string description = CellValue(row.Cells[2]);
                KeyValue keyValue = new KeyValue(key, value, description);
                headers.Add(keyValue);
            }
            tempApi.Hearders = headers;
            // 模板类型
            switch (comboBox_infoType.Text) 
            {
                case "JSON":
                    tempApi.InfoType = InfoType.JSON;
                    break;
                case "XML":
                    tempApi.InfoType = InfoType.XML;
                    break;
                default:
                    tempApi.InfoType = InfoType.TEXT;
                    break;
            }
            switch (comboBox_parseInfoType.Text)
            {
                case "JSON":
                    tempApi.ParseInfoType = InfoType.JSON;
                    break;
                case "XML":
                    tempApi.ParseInfoType = InfoType.XML;
                    break;
                default:
                    tempApi.ParseInfoType = InfoType.TEXT;
                    break;
            }

            // SOAP模板
            tempApi.OuterTemplate = textBox_outerTemplate.Text;
            // 解析路径
            tempApi.ParsePath = textBox_parsePath.Text;
            // 消息模板
            tempApi.Template = textBox_template.Text;
            // 测试数据
            tempApi.MockData = textBox_mockdata.Text;
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


        IInterfaceConnect command;

        PythonScriptExecutor pythonScript;
        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var tempApi = GetTempApi(api);
                command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi,true);
                var HttpInterfaceConnector = (BaseInterfaceConnect)command;

                var message = tempApi.MockData;
                var request = HttpInterfaceConnector.BuildRequest(message);

                pythonScript = new PythonScriptExecutor();
                request = Operation.ExeOperation(Operation.FindOperation("before_send_operation", tempApi), request, pythonScript);

                // 请求主体
                textBox3.Text = request;
                // 访问地址
                textBox5.Text = HttpSend.GetCompleteUrl(tempApi.Url, tempApi.Parmeters);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Start();
            button1.Text = "生成" + stopwatch.ElapsedMilliseconds + "ms";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                string message = textBox3.Text;
                var tempApi = GetTempApi(api);
                if(command == null)
                {
                    command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                }
                stopwatch.Start();
                var HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var response = HttpInterfaceConnector.SendRequest(message);

                if (pythonScript == null)
                {
                    pythonScript = new PythonScriptExecutor();
                }
                response = Operation.ExeOperation(Operation.FindOperation("after_send_operation", tempApi), response, pythonScript);

                textBox4.Text = response;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Start();
            button3.Text = "发送" + stopwatch.ElapsedMilliseconds + "ms";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            
            try
            {
                var tempApi = GetTempApi(api);
                var message = textBox4.Text;
                var command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                var httpInterfaceConnector = (BaseInterfaceConnect)command;
                stopwatch.Start();
                var response = httpInterfaceConnector.HandleResponse(message);
                // 后置操作
                if (pythonScript == null)
                {
                    pythonScript = new PythonScriptExecutor();
                }
                response = Operation.ExeOperation(Operation.FindOperation("after_operation", tempApi), response, pythonScript);

                textBox7.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Start();
            button5.Text = "解析" + stopwatch.ElapsedMilliseconds + "ms";
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                var config = GetTempApi(api);
                var message = textBox_mockdata.Text;
                var command = InterfaceInvoker.ConnectorManager.GetConnector(config,true);
                stopwatch.Start();
                textBox7.Text = command.Send(message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Stop();

            button_run.ForeColor =  PrintColorByRunSpeed(stopwatch.ElapsedMilliseconds);
            button_run.Text = "测试" + stopwatch.ElapsedMilliseconds + "ms";
        }
        private Color PrintColorByRunSpeed(long time)
        {
            if(time < 80)
            {
                return Color.HotPink;
            }
            else if(time < 500)
            {
                return Color.Orange;
            }
            else
            {
                return Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

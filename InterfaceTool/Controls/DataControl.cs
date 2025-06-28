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
    public partial class DataControl : UserControl
    {
        public DataControl(InterfaceConfig api)
        {
            InitializeComponent();
            if(api == null)
            {
                MessageBox.Show("API加载失败！");
                return;
            }
            this.api = (DBInterfaceConfig)api;
            LoadApiConfig(this.api);
        }
        private DBInterfaceConfig api;
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
                    command = null;
                    MessageBox.Show("保存成功！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadApiConfig(DBInterfaceConfig api)
        {
            groupBox1.Text = "DB通信（" + api.Action + ")";

            textBox1.Text = api.Connection;

            var type = api.DBtype;
            if(type == DBType.ORACLE)
            {
                comboBox1.Text = "ORACLE";
            }
            if (type == DBType.MYSQL)
            {
                comboBox1.Text = "MYSQL";
            }
            if (type == DBType.SQLSERVER)
            {
                comboBox1.Text = "SQLSERVER";
            }

            if(api.SQLType == SQLType.QUERY)
            {
                comboBox2.Text = "Query";
            }
            if (api.SQLType == SQLType.QUERYFIRST)
            {
                comboBox2.Text = "QueryFirst";
            }
            if (api.SQLType == SQLType.NOQUERY)
            {
                comboBox2.Text = "NoQuery";
            }
            if (api.SQLType == SQLType.STOREPROCEDUCE)
            {
                comboBox2.Text = "StoreProcedure";
            }

            if (comboBox2.Text == "StoreProcedure")
            {
                this.tabPage7.Parent = this.tabControl1;
            }
            else
            {
                this.tabPage7.Parent = null;
            }

            if (api.Rules != null)
            {
                foreach (var config in api.Rules)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "字符串", config.Formula, config.Position,config.Remark};
                    //dataGridView3.Rows.Add(obj);
                }
            }
            if (api.DesRules != null)
            {
                foreach (var config in api.DesRules)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "字符串", config.Formula, config.Position,config.Remark};
                    //dataGridView5.Rows.Add(obj);
                }
            }
            if(api.Configs != null)
            {
                foreach(var item in api.Configs)
                {
                    dataGridView1.Rows.Add(item.Key, item.Value,item.Description);
                }
            }
            if(api.Parameters != null)
            {
                foreach (var item in api.Parameters)
                {
                    var parameterType = string.Empty;
                    if (item.Type == DbType.String)
                    {
                        parameterType = "String";
                    }
                    if (item.Type == DbType.Int32)
                    {
                        parameterType = "Int32";
                    }
                    if (item.Type == DbType.Decimal)
                    {
                        parameterType = "Decimal";
                    }
                    if (item.Type == DbType.DateTime)
                    {
                        parameterType = "DateTime";
                    }

                    var direction = string.Empty;
                    if (item.Direction == ParameterDirection.Input)
                    {
                        direction = "IN";
                    }
                    if (item.Direction == ParameterDirection.Output)
                    {
                        direction = "OUT";
                    }
                    if (item.Direction == ParameterDirection.InputOutput)
                    {
                        direction = "INOUT";
                    }
                    if (item.Direction == ParameterDirection.ReturnValue)
                    {
                        direction = "ReturnValue";
                    }
                    var obj = new object[] { item.Name, parameterType, direction };
                    dataGridView2.Rows.Add(obj);
                }
            }

            textBox8.Text = api.OuterTemplate;
            textBox2.Text = api.Template;
            comboBox2.Text = api.InfoType.ToString();
            textBox6.Text = api.MockData;
        }
        private DBInterfaceConfig GetTempApi(DBInterfaceConfig api)
        {
            if(api == null)
            {
                throw new ArgumentNullException("GetTempApi's HttpApi is null");
            }
            var tempApi = JsonTool.Clone<DBInterfaceConfig>(api);
            
            tempApi.Connection = textBox1.Text;
            if(comboBox1.Text == "ORACLE")
            {
                tempApi.DBtype = DBType.ORACLE;
            }
            if (comboBox1.Text == "MYSQL")
            {
                tempApi.DBtype = DBType.MYSQL;
            }
            if (comboBox1.Text == "SQLSERVER")
            {
                tempApi.DBtype = DBType.SQLSERVER;
            }
            if(comboBox2.Text == "Query")
            {
                tempApi.SQLType = SQLType.QUERY;
            }
            if (comboBox2.Text == "QueryFirst")
            {
                tempApi.SQLType = SQLType.QUERYFIRST;
            }
            if (comboBox2.Text == "NoQuery")
            {
                tempApi.SQLType = SQLType.NOQUERY;
            }
            if (comboBox2.Text == "StoreProcedure")
            {
                tempApi.SQLType = SQLType.STOREPROCEDUCE;
            }
            // 请求解析配置
            List<DataSingleRule> inParseConfig = new List<DataSingleRule>();
            //foreach (DataGridViewRow row in dataGridView3.Rows)
            //{
            //    if (row.IsNewRow) continue;

            //    string key = CellValue(row.Cells[0]);
            //    KeyType keyType = KeyType.STRING;
            //    if (CellValue(row.Cells[1]) == "集合")
            //    {
            //        keyType = KeyType.ARRAY;
            //    }
            //    else
            //    {
            //        keyType = KeyType.STRING;
            //    }
            //    string expression = CellValue(row.Cells[2]);
            //    string correspondNode = CellValue(row.Cells[3]);
            //    string remark = CellValue(row.Cells[4]);
            //    KeywordRule config = new KeywordRule {Key = key,KeyType = keyType,Formula = expression, TargetPosition = correspondNode,Remark = remark };
            //    inParseConfig.Add(config);
            //}
            tempApi.Rules = inParseConfig;
            // 响应解析配置
            List<DataSingleRule> outParseConfigs = new List<DataSingleRule>();
            //foreach (DataGridViewRow row in dataGridView5.Rows)
            //{
            //    if (row.IsNewRow) continue;
            //    string key = CellValue(row.Cells[0]);
            //    KeyType keyType = KeyType.STRING;
            //    if (CellValue(row.Cells[1]) == "集合")
            //    {
            //        keyType = KeyType.ARRAY;
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
            tempApi.DesRules = outParseConfigs;
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
            tempApi.Configs = parameters;
            
            List<StoredProcedureParameter> paramenter = new List<StoredProcedureParameter>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                string key = CellValue(row.Cells[0]);
                string type = CellValue(row.Cells[1]);
                string direction = CellValue(row.Cells[2]);

                var parameter = new StoredProcedureParameter();
                parameter.Name = key;
                if(type == "String")
                {
                    parameter.Type = DbType.String;
                }
                if(type == "Int32")
                {
                    parameter.Type = DbType.Int32;
                }
                if (type == "Decimal")
                {
                    parameter.Type = DbType.Decimal;
                }
                if (type == "DateTime")
                {
                    parameter.Type = DbType.DateTime;
                }
                if(direction == "IN")
                {
                    parameter.Direction = ParameterDirection.Input;
                }
                if (direction == "OUT")
                {
                    parameter.Direction = ParameterDirection.Output;
                }
                if (direction == "INOUT")
                {
                    parameter.Direction = ParameterDirection.InputOutput;
                }
                if (direction == "ReturnValue")
                {
                    parameter.Direction = ParameterDirection.ReturnValue;
                }
                paramenter.Add(parameter);
            }
            tempApi.Parameters = paramenter;
            // 消息模板
            tempApi.Template = textBox2.Text;
            // 测试数据
            tempApi.MockData = textBox6.Text;
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var tempApi = GetTempApi(api);
                command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi,true);
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var request = HttpInterfaceConnector.BuildRequest(tempApi.MockData);
                // 请求主体
                textBox3.Text = request;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string request = textBox3.Text;
                var tempApi = GetTempApi(api);
                if(command == null)
                {
                    command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                } 
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var response = HttpInterfaceConnector.SendRequest(request);
                textBox4.Text = response;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var tempApi = GetTempApi(api);
                var command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var message = textBox4.Text;
                var response = HttpInterfaceConnector.HandleResponse(message);
                textBox7.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var text = comboBox2.Text;
            if(text == "StoreProcedure")
            {
                this.tabPage7.Parent = this.tabControl1;
            }
            else
            {
                this.tabPage7.Parent = null;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

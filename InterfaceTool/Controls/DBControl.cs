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
    public partial class DBControl : UserControl
    {
        public DBControl(InterfaceConfig api)
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
            groupBox1.Text = "数据库（" + api.Action + ")";

            textBox_dbConnecttion.Text = api.Connection;

            var type = api.DBtype;
            if(type == DBType.ORACLE)
            {
                comboBox_dbType.Text = "ORACLE";
            }
            if (type == DBType.MYSQL)
            {
                comboBox_dbType.Text = "MYSQL";
            }
            if (type == DBType.SQLSERVER)
            {
                comboBox_dbType.Text = "SQLSERVER";
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
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "对象", config.Formula, config.Position,config.Remark};
                    dataGridView_generativeRules.Rows.Add(obj);
                }
            }
            if (api.DesRules != null)
            {
                foreach (var config in api.DesRules)
                {
                    var obj = new object[] { config.Key, config.KeyType == KeyType.ARRAY ? "集合" : "对象", config.Formula, config.Position,config.Remark};
                    dataGridView_parseRule.Rows.Add(obj);
                }
            }
            if(api.Configs != null)
            {
                foreach(var item in api.Configs)
                {
                    dataGridView_configs.Rows.Add(item.Key, item.Value,item.Description);
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
                    if(item.Type == DbType.Double)
                    {
                        parameterType = "Double";
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
                    dataGridView_storeParams.Rows.Add(obj);
                }
            }

            textBox8.Text = api.OuterTemplate;

            textBox_sql.Text = api.Template;

            comboBox2.Text = api.InfoType.ToString();

            textBox_input.Text = api.MockData;
        }
        private DBInterfaceConfig GetTempApi(DBInterfaceConfig api)
        {
            if(api == null)
            {
                throw new ArgumentNullException("GetTempApi's HttpApi is null");
            }
            var tempApi = JsonTool.Clone<DBInterfaceConfig>(api);
            
            tempApi.Connection = textBox_dbConnecttion.Text;
            if(comboBox_dbType.Text == "ORACLE")
            {
                tempApi.DBtype = DBType.ORACLE;
            }
            if (comboBox_dbType.Text == "MYSQL")
            {
                tempApi.DBtype = DBType.MYSQL;
            }
            if (comboBox_dbType.Text == "SQLSERVER")
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
            foreach (DataGridViewRow row in dataGridView_generativeRules.Rows)
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
            foreach (DataGridViewRow row in dataGridView_parseRule.Rows)
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
            // 请求参数
            List<KeyValue> parameters = new List<KeyValue>();
            foreach (DataGridViewRow row in dataGridView_configs.Rows)
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
            foreach (DataGridViewRow row in dataGridView_storeParams.Rows)
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
                if(type == "Double")
                {
                    parameter.Type = DbType.Double;
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
            tempApi.Template = textBox_sql.Text;
            // 测试数据
            tempApi.MockData = textBox_input.Text;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var tempApi = GetTempApi(api);
                command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi,true);
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var request = HttpInterfaceConnector.BuildRequest(tempApi.MockData);
                // 请求主体
                textBox_request.Text = request;
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
            stopwatch.Start();
            try
            {
                string request = textBox_request.Text;
                var tempApi = GetTempApi(api);
                if(command == null)
                {
                    command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                } 
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var response = HttpInterfaceConnector.SendRequest(request);
                textBox_response.Text = response;
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
            stopwatch.Start();
            try
            {
                var tempApi = GetTempApi(api);
                var command = InterfaceInvoker.ConnectorManager.GetConnector(tempApi, true);
                BaseInterfaceConnect HttpInterfaceConnector = (BaseInterfaceConnect)command;
                var message = textBox_response.Text;
                var response = HttpInterfaceConnector.HandleResponse(message);
                textBox_result.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Start();
            button5.Text = "解析" + stopwatch.ElapsedMilliseconds + "ms";
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

        private void button_run_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var config = GetTempApi(api);
                var message = textBox_input.Text;
                var command = InterfaceInvoker.ConnectorManager.GetConnector(config, true);
                stopwatch.Start();
                textBox_result.Text = InterfaceInvoker.Invoke(config.Action, message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            stopwatch.Start();

            button_run.ForeColor = PrintColorByRunSpeed(stopwatch.ElapsedMilliseconds);
            button_run.Text = "测试" + stopwatch.ElapsedMilliseconds + "ms";
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

    }
}

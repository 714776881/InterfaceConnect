
namespace InterfaceConnect
{
    partial class DBControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_dbConnecttion = new System.Windows.Forms.TextBox();
            this.comboBox_dbType = new System.Windows.Forms.ComboBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView_generativeRules = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataGridView_parseRule = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.button_run = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.textBox_request = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox_response = new System.Windows.Forms.TextBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_configs = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox_sql = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.dataGridView_storeParams = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.参数类型 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.方向 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.Key = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DETAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_generativeRules)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_parseRule)).BeginInit();
            this.tabControl3.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configs)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_storeParams)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(958, 755);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button_save);
            this.splitContainer1.Panel1.Controls.Add(this.textBox_dbConnecttion);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox_dbType);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(952, 735);
            this.splitContainer1.SplitterDistance = 321;
            this.splitContainer1.TabIndex = 1;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_save.Location = new System.Drawing.Point(828, 9);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(114, 26);
            this.button_save.TabIndex = 17;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox_dbConnecttion
            // 
            this.textBox_dbConnecttion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_dbConnecttion.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_dbConnecttion.Location = new System.Drawing.Point(119, 9);
            this.textBox_dbConnecttion.Name = "textBox_dbConnecttion";
            this.textBox_dbConnecttion.Size = new System.Drawing.Size(690, 26);
            this.textBox_dbConnecttion.TabIndex = 15;
            // 
            // comboBox_dbType
            // 
            this.comboBox_dbType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_dbType.FormattingEnabled = true;
            this.comboBox_dbType.Items.AddRange(new object[] {
            "ORACLE",
            "MYSQL",
            "SQLSERVER"});
            this.comboBox_dbType.Location = new System.Drawing.Point(11, 9);
            this.comboBox_dbType.Name = "comboBox_dbType";
            this.comboBox_dbType.Size = new System.Drawing.Size(93, 24);
            this.comboBox_dbType.TabIndex = 14;
            this.comboBox_dbType.Text = "ORACLE";
            // 
            // tabControl2
            // 
            this.tabControl2.AllowDrop = true;
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(0, 41);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(952, 281);
            this.tabControl2.TabIndex = 19;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView_generativeRules);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(944, 255);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "消息生成规则";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView_generativeRules
            // 
            this.dataGridView_generativeRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView_generativeRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_generativeRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_generativeRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column4,
            this.Column6});
            this.dataGridView_generativeRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_generativeRules.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_generativeRules.Name = "dataGridView_generativeRules";
            this.dataGridView_generativeRules.RowHeadersWidth = 62;
            this.dataGridView_generativeRules.RowTemplate.Height = 23;
            this.dataGridView_generativeRules.Size = new System.Drawing.Size(938, 249);
            this.dataGridView_generativeRules.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "名称";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "类型";
            this.Column2.Items.AddRange(new object[] {
            "对象",
            "集合"});
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "表达式";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "关联路径";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "备注";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridView_parseRule);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(944, 255);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "消息解析规则";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView_parseRule
            // 
            this.dataGridView_parseRule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView_parseRule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_parseRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_parseRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dataGridView_parseRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_parseRule.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_parseRule.Name = "dataGridView_parseRule";
            this.dataGridView_parseRule.RowHeadersWidth = 62;
            this.dataGridView_parseRule.RowTemplate.Height = 23;
            this.dataGridView_parseRule.Size = new System.Drawing.Size(938, 249);
            this.dataGridView_parseRule.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "名称";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.FillWeight = 50F;
            this.dataGridViewComboBoxColumn1.HeaderText = "类型";
            this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
            "对象",
            "集合"});
            this.dataGridViewComboBoxColumn1.MinimumWidth = 8;
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "表达式";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "关联路径";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "备注";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPage10);
            this.tabControl3.Controls.Add(this.tabPage8);
            this.tabControl3.Controls.Add(this.tabPage6);
            this.tabControl3.Controls.Add(this.tabPage11);
            this.tabControl3.Location = new System.Drawing.Point(452, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(497, 404);
            this.tabControl3.TabIndex = 16;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.button_run);
            this.tabPage10.Controls.Add(this.button1);
            this.tabPage10.Controls.Add(this.textBox_input);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(489, 378);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "输入参数";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // button_run
            // 
            this.button_run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_run.Font = new System.Drawing.Font("宋体", 9F);
            this.button_run.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button_run.Location = new System.Drawing.Point(324, 4);
            this.button_run.Name = "button_run";
            this.button_run.Size = new System.Drawing.Size(78, 24);
            this.button_run.TabIndex = 18;
            this.button_run.Text = "测试";
            this.button_run.UseVisualStyleBackColor = true;
            this.button_run.Click += new System.EventHandler(this.button_run_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("宋体", 9F);
            this.button1.Location = new System.Drawing.Point(408, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 24);
            this.button1.TabIndex = 16;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_input
            // 
            this.textBox_input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_input.Location = new System.Drawing.Point(3, 33);
            this.textBox_input.Multiline = true;
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.Size = new System.Drawing.Size(483, 339);
            this.textBox_input.TabIndex = 6;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.textBox_request);
            this.tabPage8.Controls.Add(this.button3);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(489, 378);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "请求消息";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // textBox_request
            // 
            this.textBox_request.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_request.Location = new System.Drawing.Point(3, 33);
            this.textBox_request.Multiline = true;
            this.textBox_request.Name = "textBox_request";
            this.textBox_request.Size = new System.Drawing.Size(483, 342);
            this.textBox_request.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(410, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "发送";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.button5);
            this.tabPage6.Controls.Add(this.textBox_response);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(489, 378);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "响应消息";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(408, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "解析";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox_response
            // 
            this.textBox_response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_response.Location = new System.Drawing.Point(3, 33);
            this.textBox_response.Multiline = true;
            this.textBox_response.Name = "textBox_response";
            this.textBox_response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_response.Size = new System.Drawing.Size(483, 343);
            this.textBox_response.TabIndex = 1;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.textBox_result);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(489, 378);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "输出结果";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // textBox_result
            // 
            this.textBox_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_result.Location = new System.Drawing.Point(3, 3);
            this.textBox_result.Multiline = true;
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_result.Size = new System.Drawing.Size(483, 372);
            this.textBox_result.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 404);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_configs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "配置项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView_configs
            // 
            this.dataGridView_configs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_configs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_configs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.VALUE,
            this.DETAIL});
            this.dataGridView_configs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_configs.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_configs.Name = "dataGridView_configs";
            this.dataGridView_configs.RowHeadersWidth = 62;
            this.dataGridView_configs.RowTemplate.Height = 23;
            this.dataGridView_configs.Size = new System.Drawing.Size(433, 372);
            this.dataGridView_configs.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Controls.Add(this.textBox_sql);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SQL语句";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Query",
            "QueryFirst",
            "NoQuery",
            "StoreProcedure"});
            this.comboBox2.Location = new System.Drawing.Point(3, 7);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(82, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "Query";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // textBox_sql
            // 
            this.textBox_sql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_sql.Location = new System.Drawing.Point(3, 33);
            this.textBox_sql.Multiline = true;
            this.textBox_sql.Name = "textBox_sql";
            this.textBox_sql.Size = new System.Drawing.Size(433, 342);
            this.textBox_sql.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.dataGridView_storeParams);
            this.tabPage7.Controls.Add(this.textBox8);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(439, 378);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "存储过程参数";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // dataGridView_storeParams
            // 
            this.dataGridView_storeParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_storeParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_storeParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.参数类型,
            this.方向});
            this.dataGridView_storeParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_storeParams.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_storeParams.Name = "dataGridView_storeParams";
            this.dataGridView_storeParams.RowHeadersWidth = 62;
            this.dataGridView_storeParams.RowTemplate.Height = 23;
            this.dataGridView_storeParams.Size = new System.Drawing.Size(433, 372);
            this.dataGridView_storeParams.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // 参数类型
            // 
            this.参数类型.HeaderText = "类型";
            this.参数类型.Items.AddRange(new object[] {
            "String",
            "Int32",
            "Double",
            "Decimal",
            "DateTime"});
            this.参数类型.Name = "参数类型";
            // 
            // 方向
            // 
            this.方向.HeaderText = "方向";
            this.方向.Items.AddRange(new object[] {
            "IN",
            "OUT",
            "INOUT",
            "ReturnValue"});
            this.方向.Name = "方向";
            // 
            // textBox8
            // 
            this.textBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox8.Location = new System.Drawing.Point(3, 3);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(433, 372);
            this.textBox8.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Key.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Key.HeaderText = "名称";
            this.Key.MinimumWidth = 8;
            this.Key.Name = "Key";
            this.Key.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // VALUE
            // 
            this.VALUE.HeaderText = "值";
            this.VALUE.MinimumWidth = 8;
            this.VALUE.Name = "VALUE";
            // 
            // DETAIL
            // 
            this.DETAIL.HeaderText = "备注";
            this.DETAIL.MinimumWidth = 8;
            this.DETAIL.Name = "DETAIL";
            // 
            // DBControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DBControl";
            this.Size = new System.Drawing.Size(958, 755);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_generativeRules)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_parseRule)).EndInit();
            this.tabControl3.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configs)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_storeParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_dbConnecttion;
        private System.Windows.Forms.ComboBox comboBox_dbType;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView_generativeRules;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dataGridView_parseRule;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TextBox textBox_request;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox_response;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.TextBox textBox_result;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_configs;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox_sql;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.DataGridView dataGridView_storeParams;
        private System.Windows.Forms.Button button_run;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn 参数类型;
        private System.Windows.Forms.DataGridViewComboBoxColumn 方向;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewComboBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DETAIL;
    }
}

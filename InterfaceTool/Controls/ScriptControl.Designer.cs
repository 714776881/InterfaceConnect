
namespace InterfaceConnect
{
    partial class ScriptControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button_save = new System.Windows.Forms.Button();
            this.text_script = new System.Windows.Forms.TextBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.comboBox_messageType = new System.Windows.Forms.ComboBox();
            this.button_test = new System.Windows.Forms.Button();
            this.text_input = new System.Windows.Forms.TextBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView_configs = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DETAIL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.text_template = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.textBox_outTemplate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configs)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage7.SuspendLayout();
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
            this.groupBox1.Text = "脚本";
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
            this.splitContainer1.Panel1.Controls.Add(this.text_script);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(952, 735);
            this.splitContainer1.SplitterDistance = 478;
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
            // text_script
            // 
            this.text_script.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text_script.Location = new System.Drawing.Point(0, 0);
            this.text_script.Multiline = true;
            this.text_script.Name = "text_script";
            this.text_script.Size = new System.Drawing.Size(952, 478);
            this.text_script.TabIndex = 18;
            this.text_script.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_script_KeyDown);
            this.text_script.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.text_script_PreviewKeyDown);
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPage10);
            this.tabControl3.Controls.Add(this.tabPage11);
            this.tabControl3.Location = new System.Drawing.Point(452, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(497, 247);
            this.tabControl3.TabIndex = 16;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.comboBox_messageType);
            this.tabPage10.Controls.Add(this.button_test);
            this.tabPage10.Controls.Add(this.text_input);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(489, 221);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "输入参数";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // comboBox_messageType
            // 
            this.comboBox_messageType.FormattingEnabled = true;
            this.comboBox_messageType.Items.AddRange(new object[] {
            "JSON",
            "TEXT"});
            this.comboBox_messageType.Location = new System.Drawing.Point(6, 6);
            this.comboBox_messageType.Name = "comboBox_messageType";
            this.comboBox_messageType.Size = new System.Drawing.Size(88, 20);
            this.comboBox_messageType.TabIndex = 17;
            this.comboBox_messageType.Text = "JSON";
            // 
            // button_test
            // 
            this.button_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_test.Font = new System.Drawing.Font("宋体", 9F);
            this.button_test.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button_test.Location = new System.Drawing.Point(408, 4);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(78, 24);
            this.button_test.TabIndex = 16;
            this.button_test.Text = "测试";
            this.button_test.UseVisualStyleBackColor = true;
            this.button_test.Click += new System.EventHandler(this.button1_Click);
            // 
            // text_input
            // 
            this.text_input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_input.Location = new System.Drawing.Point(3, 33);
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(483, 182);
            this.text_input.TabIndex = 6;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.textBox_result);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(489, 221);
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
            this.textBox_result.Size = new System.Drawing.Size(483, 215);
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
            this.tabControl1.Size = new System.Drawing.Size(447, 247);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView_configs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 221);
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
            this.dataGridView_configs.Size = new System.Drawing.Size(433, 215);
            this.dataGridView_configs.TabIndex = 1;
            // 
            // Key
            // 
            this.Key.HeaderText = "名称";
            this.Key.MinimumWidth = 8;
            this.Key.Name = "Key";
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.text_template);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "消息模板";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // text_template
            // 
            this.text_template.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text_template.Location = new System.Drawing.Point(3, 3);
            this.text_template.Multiline = true;
            this.text_template.Name = "text_template";
            this.text_template.Size = new System.Drawing.Size(433, 215);
            this.text_template.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.textBox_outTemplate);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(439, 221);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "外层模板";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // textBox_outTemplate
            // 
            this.textBox_outTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_outTemplate.Location = new System.Drawing.Point(3, 3);
            this.textBox_outTemplate.Multiline = true;
            this.textBox_outTemplate.Name = "textBox_outTemplate";
            this.textBox_outTemplate.Size = new System.Drawing.Size(433, 215);
            this.textBox_outTemplate.TabIndex = 0;
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(958, 755);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_configs)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Button button_test;
        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TextBox textBox_result;
        private System.Windows.Forms.TextBox text_script;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView_configs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DETAIL;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox text_template;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox textBox_outTemplate;
        private System.Windows.Forms.ComboBox comboBox_messageType;
    }
}

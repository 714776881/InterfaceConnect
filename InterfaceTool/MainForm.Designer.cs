
namespace InterfaceConnect
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("门诊电子单");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("门诊电子单列表");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("门诊手工单");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("IEIS申请单", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("门诊登机前通费");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("门诊登记后通费");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("IEIS通费", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("报告上传");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("报告状态上传");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("IEIS报告", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("危急值发送");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("危急值", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("获取镜子");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("使用镜子");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("内镜相关", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.treeView_actions = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtApiName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_actions
            // 
            this.treeView_actions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_actions.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_actions.Location = new System.Drawing.Point(0, 31);
            this.treeView_actions.Name = "treeView_actions";
            treeNode1.Name = "节点1";
            treeNode1.Tag = "";
            treeNode1.Text = "门诊电子单";
            treeNode2.Name = "节点0";
            treeNode2.Text = "门诊电子单列表";
            treeNode3.Name = "节点7";
            treeNode3.Text = "门诊手工单";
            treeNode4.Name = "节点0";
            treeNode4.Text = "IEIS申请单";
            treeNode5.Name = "节点4";
            treeNode5.Text = "门诊登机前通费";
            treeNode6.Name = "节点5";
            treeNode6.Text = "门诊登记后通费";
            treeNode7.Name = "报告";
            treeNode7.Text = "IEIS通费";
            treeNode8.Name = "节点9";
            treeNode8.Text = "报告上传";
            treeNode9.Name = "节点10";
            treeNode9.Text = "报告状态上传";
            treeNode10.Name = "IEIS报告";
            treeNode10.Text = "IEIS报告";
            treeNode11.Name = "节点15";
            treeNode11.Text = "危急值发送";
            treeNode12.Name = "危急值";
            treeNode12.Text = "危急值";
            treeNode13.Name = "节点18";
            treeNode13.Text = "获取镜子";
            treeNode14.Name = "节点19";
            treeNode14.Text = "使用镜子";
            treeNode15.Name = "内镜相关";
            treeNode15.Text = "内镜相关";
            this.treeView_actions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode10,
            treeNode12,
            treeNode15});
            this.treeView_actions.Size = new System.Drawing.Size(248, 720);
            this.treeView_actions.TabIndex = 1;
            this.treeView_actions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button_edit);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.treeView_actions);
            this.splitContainer1.Size = new System.Drawing.Size(1407, 754);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(131, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "复制";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(191, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_edit
            // 
            this.button_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_edit.Location = new System.Drawing.Point(61, 3);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(64, 23);
            this.button_edit.TabIndex = 6;
            this.button_edit.Text = "修改";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(0, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1297, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "浙江省第一人民医院";
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1226, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "医院名称：";
            // 
            // TxtApiName
            // 
            this.TxtApiName.AutoSize = true;
            this.TxtApiName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtApiName.Location = new System.Drawing.Point(12, 9);
            this.TxtApiName.Name = "TxtApiName";
            this.TxtApiName.Size = new System.Drawing.Size(68, 17);
            this.TxtApiName.TabIndex = 5;
            this.TxtApiName.Text = "接口列表：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1431, 786);
            this.Controls.Add(this.TxtApiName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "接口配置工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView_actions;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Label TxtApiName;
        private System.Windows.Forms.Button button4;
    }
}


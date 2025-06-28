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
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
            label2.Text = AppSettingsTool.ReadSetting("HospitalName");
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            RefreshTreeView();
        }
        private ControlManager apiControlManager = ControlManager.Instance;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectNode = treeView_actions.SelectedNode;
            if(selectNode.Nodes.Count > 0)
            {
                return;
            }

            string action = ((InterfaceConfig)selectNode.Tag).Action;
            InterfaceConfig api = ConfigManager.Instance.GetInterfaceConfig(action);

            splitContainer1.Panel2.Controls.Clear();
            UserControl control =  apiControlManager.GetControl(api);
            control.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(control);
        }
        private void RefreshTreeView()
        {
            var configs = ConfigManager.Instance.GetAllApis();
            treeView_actions.BeginUpdate();
            treeView_actions.Nodes.Clear();
            foreach (var config in configs.Values)
            {
                var category = config.Category;

                if (!treeView_actions.Nodes.ContainsKey(category))
                {
                    treeView_actions.Nodes.Add(category, category);
                }

                TreeNode categoryNode = treeView_actions.Nodes[category];
                TreeNode configNode = GetNodeByName(config.Name);
                if (configNode != null)
                {
                    if(configNode.Nodes.Count == 0)
                    {
                        var action  = ((InterfaceConfig)configNode.Tag).Action;
                        TreeNode node = new TreeNode(action);
                        node.Tag = configNode.Tag;
                        configNode.Nodes.Add(node);
                    }
                    TreeNode childNode = new TreeNode(config.Action);
                    childNode.Tag = config;
                    configNode.Nodes.Add(childNode);
                    configNode.Expand();
                }
                else
                {
                    TreeNode childNode = new TreeNode(config.Name);
                    childNode.Tag = config;
                    categoryNode.Nodes.Add(childNode);
                    categoryNode.Expand();
                }
            }
            treeView_actions.EndUpdate();
        }
        private TreeNode GetNodeByName(string name)
        {
            foreach (TreeNode node in treeView_actions.Nodes)
            {
                foreach(TreeNode sonNode in node.Nodes)
                {
                    if (sonNode.Text == name)
                    {
                        return sonNode;
                    }
                }
            }
            return null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ApiConfigForm menuForm = new ApiConfigForm();
            menuForm.StartPosition = FormStartPosition.CenterParent;
            if (menuForm.ShowDialog() == DialogResult.OK)
            {
                RefreshTreeView();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var selectNode = treeView_actions.SelectedNode;
            if (selectNode == null || selectNode.Nodes.Count > 0)
            {
                return;
            }
            string action = ((InterfaceConfig)selectNode.Tag).Action;
            var config = ConfigManager.Instance.GetInterfaceConfig(action);
            if (config != null)
            {
                ApiConfigForm menuForm = new ApiConfigForm();
                menuForm.LoadConfig(config);
                menuForm.StartPosition = FormStartPosition.CenterParent;
                if (menuForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshTreeView();
                    // 刷新窗口
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var selectNode = treeView_actions.SelectedNode;
            if (selectNode.Nodes.Count > 0)
            {
                return;
            }
            if(DialogResult.OK != MessageBox.Show("将删除此API配置("+ selectNode.Text + ")，请确认！", "警告", MessageBoxButtons.OKCancel)){
                return;
            }
            string action = ((InterfaceConfig)selectNode.Tag).Action;
            if (ConfigManager.Instance.RemoveApiAsync(action))
            {
                RefreshTreeView();
            }
            else
            {
                MessageBox.Show("文件删除失败！");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var selectNode = treeView_actions.SelectedNode;
            if (selectNode.Nodes.Count > 0)
            {
                return;
            }
            string action = ((InterfaceConfig)selectNode.Tag).Action;
            var config = ConfigManager.Instance.GetInterfaceConfig(action);
            if (config != null)
            {
                var newConfig = ConfigFactory.Clone(config);
                newConfig.Action = newConfig.Action + "(复制)";
                ConfigManager.Instance.SaveApiAsync(newConfig);
                RefreshTreeView();
            }
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            HospitalEditForm form = new HospitalEditForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
            label2.Text = AppSettingsTool.ReadSetting("HospitalName");
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toto.Forms
{
    public partial class DirectoryTreeView: UserControl
    {
        public DirectoryTreeView()
        {
            InitializeComponent();
            this.Tag = Environment.CurrentDirectory;

            TreeNode rootNode = new TreeNode("此电脑");//初始化总节点
            rootNode.Nodes.Add("");
            tree.Nodes.Add(rootNode);
        }

        private void tree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            tree_AfterSelect(sender, e);
            e.Node.Nodes.Clear();
            ShowNode(e.Node);
        }

        private void ShowNode(TreeNode node)
        {
            try
            {
                if (node.Nodes.Count == 0) // 没有设置子节点
                {
                    if (node.Parent == null) // 没有设置父节点
                    {
                        foreach (String drv in Directory.GetLogicalDrives())
                        {
                            TreeNode drvNode = new TreeNode(drv);
                            drvNode.Tag = drv;
                            drvNode.Nodes.Add("");
                            node.Nodes.Add(drvNode);
                        }

                    } else //  已经设置了父节点
                    {
                        foreach (String dir in Directory.GetDirectories((String)node.Tag))
                        {
                            TreeNode dirNode = new TreeNode(dir);
                            dirNode.Tag = dir;
                            dirNode.Nodes.Add("");
                            node.Nodes.Add(dirNode);
                        }
                    }

                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Tag = (string)e.Node.Tag;
        }

    }
}

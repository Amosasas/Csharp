using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form2();
            frm.Show(this);
            
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 全部最大化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var item in this.MdiChildren)
            {
                item.WindowState = FormWindowState.Maximized;
            }
        }

        private void 全部最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.WindowState = FormWindowState.Minimized;
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.Close();
            }
        }

        private void 层叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.MdiChildren)
            {
                item.LayoutMdi(MdiLayout.Cascade);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            
        }

        private void 新建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new Form3();
            frm.MdiParent = this;
            frm.Show();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            var frm = new Form3();
            frm.MdiParent = this;

            frm.textBox1.Text = File.ReadAllText(openFileDialog1.FileName);

            frm.Show();

        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild != null)
            {
                saveFileDialog1.ShowDialog();
            }

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var filepath = saveFileDialog1.FileName;

            var content = (ActiveMdiChild as Form3).textBox1.Text;


            File.WriteAllText(filepath, content);    
        }
    }
}

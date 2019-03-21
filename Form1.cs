using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;//用到了正则表达式  
 
namespace SCUT
{
    public partial class Form1 : Form
    {
        DB db;
        public Form1()
        {
            InitializeComponent();
            db = new DB();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            //员工表 增删改查 中“查部分”
            //生成表头
            listView1.Columns.Add("员工号", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView1.Columns.Add("员工姓名", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView1.Columns.Add("性别", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView1.Columns.Add("年龄", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            //表的内容
            DataTable table = db.getBySql(@"select * from [EMPLOYEE]");
            listView1.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();//生成每一列  
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j <= 0)
                    {
                        listViewItem.Text = table.Rows[i][j] + "";
                    }
                    else
                    {
                        listViewItem.SubItems.Add(table.Rows[i][j] + "");
                    }
                }
                listView1.Items.Add(listViewItem);
            }
            listView1.EndUpdate();//结束数据处理，UI界面一次性绘制
 
            //查询3-1
            //加载现有的员工号
            table = db.getBySql(@"select [EmpNo] from [EMPLOYEE]");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    comboBox1.Items.Add(table.Rows[i][j] + "");
                }
            }
            comboBox1.SelectedIndex = 0;
            //加载现有的员工名
            table = db.getBySql(@"select [EmpName] from [EMPLOYEE]");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    comboBox2.Items.Add(table.Rows[i][j] + "");
                }
            }
            comboBox2.SelectedIndex = 0;
 
            //表3-2
            //统计年龄至少为40岁员工的总工资，工资按从大到小顺序排列
            //生成表头
            listView3.Columns.Add("员工号", listView1.Width / 5 - 1, HorizontalAlignment.Left);
            listView3.Columns.Add("员工姓名", listView1.Width / 5 - 1, HorizontalAlignment.Left);
            listView3.Columns.Add("性别", listView1.Width / 5 - 1, HorizontalAlignment.Left);
            listView3.Columns.Add("年龄", listView1.Width / 5 - 1, HorizontalAlignment.Left);
            listView3.Columns.Add("总工资", listView1.Width / 5 - 1, HorizontalAlignment.Left);
            //表的内容
            table = db.getBySql(@"select [EMPLOYEE].[EmpNo],[EMPLOYEE].[EmpName],[EMPLOYEE].[EmpSex],[EMPLOYEE].[EmpAge],sum([WORKS].[Salary]) as '总工资' " +
                " from [EMPLOYEE],[WORKS]" +
                " where [EMPLOYEE].[EmpAge]>=40" +
                " and [EMPLOYEE].[EmpNo]=[WORKS].[EmpNo]" +
                " group by [EMPLOYEE].[EmpNo],[EMPLOYEE].[EmpName],[EMPLOYEE].[EmpSex],[EMPLOYEE].[EmpAge]" +
                " order by '总工资' desc");
            listView3.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();//生成每一列  
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j <= 0)
                    {
                        listViewItem.Text = table.Rows[i][j] + "";
                    }
                    else
                    {
                        listViewItem.SubItems.Add(table.Rows[i][j] + "");
                    }
                }
                listView3.Items.Add(listViewItem);
            }
            listView3.EndUpdate();//结束数据处理，UI界面一次性绘制。 
 
            //表3-3
            //查询至少具有两份工作员工的姓名和其公司名
            //生成表头
            listView4.Columns.Add("员工姓名", listView1.Width / 2 - 2, HorizontalAlignment.Left);
            listView4.Columns.Add("公司名", listView1.Width / 2 - 2, HorizontalAlignment.Left);
            //表的内容
            table = db.getBySql(@"select [EMPLOYEE].[EmpName],[COMPANY].[CmpName] from [EMPLOYEE],[COMPANY],[WORKS],(" +
            " select [EmpName],count([CmpName]) as 'CmpNum'" +
            " from [EMPLOYEE],[WORKS],[COMPANY]" +
            " where [EMPLOYEE].[EmpNo]=[WORKS].[EmpNo]" +
            " and [COMPANY].[CmpNo]=[WORKS].[CmpNo]" +
            " group by [EmpName]" +
            " having count([CmpName])>1" +
            " ) as t1" +
            " where [EMPLOYEE].[EmpNo]=[WORKS].[EmpNo]" +
            " and [COMPANY].[CmpNo]=[WORKS].[CmpNo]" +
            " and [EMPLOYEE].[EmpName]=t1.[EmpName]");
            listView4.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();//生成每一列  
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j <= 0)
                    {
                        listViewItem.Text = table.Rows[i][j] + "";
                    }
                    else
                    {
                        listViewItem.SubItems.Add(table.Rows[i][j] + "");
                    }
                }
                listView4.Items.Add(listViewItem);
            }
            listView4.EndUpdate();//结束数据处理，UI界面一次性绘制。 
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();//声明要使用form2窗体    
            //将form1当前的位置压入Intent中的dict    
            Intent.dict["form1_text"] = this.Text;
            Intent.dict["form1_flag"] = 0;//传个flag进去代表这是“添加”  
            if (form2.ShowDialog() == DialogResult.OK)
            {//这个判断，将会等到form2被关闭之后才执行，如果form2返回一个OK值 
                bool canAdd = true;
                foreach (ListViewItem item in this.listView1.Items)
                {
                    if (Intent.dict["form2_textbox1_text"] + "" == item.SubItems[0].Text)
                    {
                        canAdd = false;
                        MessageBox.Show("已存在该员工号！", this.Text);
                        break;
                    }
                }
                Regex regex = new Regex("^[0-9]*$");
                if (!regex.IsMatch(Intent.dict["form2_textbox3_text"] + ""))//利用正则表达式判断是否输入的是数字  
                {
                    canAdd = false;
                    MessageBox.Show("年龄不为正数！", this.Text);
                }
                if (canAdd)
                {
                    ListViewItem listViewItem = new ListViewItem();//在listview中添加一项  
                    listViewItem.Text = Intent.dict["form2_textbox1_text"] + "";
                    listViewItem.SubItems.Add(Intent.dict["form2_textbox2_text"] + "");
                    listViewItem.SubItems.Add(Intent.dict["form2_radioButton"] + "");
                    listViewItem.SubItems.Add(Intent.dict["form2_textbox3_text"] + "");
                    listView1.Items.Add(listViewItem);
                    db.setBySql("insert into [EMPLOYEE] values('" + Intent.dict["form2_textbox1_text"] + "','" + Intent.dict["form2_textbox2_text"] + "','" + Intent.dict["form2_radioButton"] + "'," + Intent.dict["form2_textbox3_text"] + ")");
                }
            }
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();//声明要使用form2窗体    
            //将form1当前的位置压入Intent中的dict    
            Intent.dict["form1_text"] = this.Text;
            Intent.dict["form1_flag"] = 1;//传个flag进去代表这是“修改”  
            Intent.dict["form1_selectedItems0"] = listView1.SelectedItems[0].SubItems[0].Text;
            Intent.dict["form1_selectedItems1"] = listView1.SelectedItems[0].SubItems[1].Text;
            Intent.dict["form1_selectedItems2"] = listView1.SelectedItems[0].SubItems[2].Text;
            Intent.dict["form1_selectedItems3"] = listView1.SelectedItems[0].SubItems[3].Text;
            Intent.dict["form1_flag"] = 1;//传个flag进去代表这是“修改”  
            if (form2.ShowDialog() == DialogResult.OK)
            {//这个判断，将会等到form2被关闭之后才执行，如果form2返回一个OK值 
                bool canUpdate = true;
                if (!((Intent.dict["form1_selectedItems0"] + "") == (Intent.dict["form2_textbox1_text"] + "")))//仅当用户修改过员工号才进行遍历
                {
                    foreach (ListViewItem item in this.listView1.Items)
                    {
                        if (Intent.dict["form2_textbox1_text"] + "" == item.SubItems[0].Text)
                        {
                            canUpdate = false;
                            MessageBox.Show("已存在该员工号！", this.Text);
                            break;
                        }
                    }
                }
                Regex regex = new Regex("^[0-9]*$");
                if (!regex.IsMatch(Intent.dict["form2_textbox3_text"] + ""))//利用正则表达式判断是否输入的是数字  
                {
                    canUpdate = false;
                    MessageBox.Show("年龄不为正数！", this.Text);
                }
                if (canUpdate)
                {
                    ListViewItem listViewItem = new ListViewItem();//在listview中添加一项  
                    listView1.SelectedItems[0].SubItems[0].Text = Intent.dict["form2_textbox1_text"] + "";
                    listView1.SelectedItems[0].SubItems[1].Text = Intent.dict["form2_textbox2_text"] + "";
                    listView1.SelectedItems[0].SubItems[2].Text = Intent.dict["form2_radioButton"] + "";
                    listView1.SelectedItems[0].SubItems[3].Text = Intent.dict["form2_textbox3_text"] + "";
                    db.setBySql("update [EMPLOYEE] set [EmpNo]='" + Intent.dict["form2_textbox1_text"] + "' where [EmpNo]='" + Intent.dict["form1_selectedItems0"] + "';");
                    db.setBySql("update [EMPLOYEE] set [EmpName]='" + Intent.dict["form2_textbox2_text"] + "' where [EmpName]='" + Intent.dict["form1_selectedItems1"] + "';");
                    db.setBySql("update [EMPLOYEE] set [EmpSex]='" + Intent.dict["form2_radioButton"] + "' where [EmpSex]='" + Intent.dict["form1_selectedItems2"] + "';");
                    db.setBySql("update [EMPLOYEE] set [EmpAge]='" + Intent.dict["form2_textbox3_text"] + "' where [EmpAge]='" + Intent.dict["form1_selectedItems3"] + "';");
                }
            }
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            db.setBySql("delete from [EMPLOYEE] where [EmpNo]='" + listView1.SelectedItems[0].SubItems[0].Text + "';");
            listView1.SelectedItems[0].Remove();//删除一定要放在数据库操作之后，不然选中项再也取不到了  
        }
 
        private void button4_Click(object sender, EventArgs e)
        {
            listView2.Clear();
            //生成表头  
            listView2.Columns.Add("员工号", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("员工名", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("公司名", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("薪水", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            //表的内容
            DataTable table = db.getBySql(@"select [EMPLOYEE].[EmpNo],[EMPLOYEE].[EmpName],[COMPANY].[CmpName],[WORKS].[Salary] from [EMPLOYEE],[COMPANY],[WORKS]" +
            " where [EMPLOYEE].[EmpNo]=[WORKS].[EmpNo]" +
            " and [COMPANY].[CmpNo]=[WORKS].[CmpNo]" +
            " and [EMPLOYEE].[EmpNo]='" + comboBox1.Text + "'");
            listView2.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();//生成每一列  
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j <= 0)
                    {
                        listViewItem.Text = table.Rows[i][j] + "";
                    }
                    else
                    {
                        listViewItem.SubItems.Add(table.Rows[i][j] + "");
                    }
                }
                listView2.Items.Add(listViewItem);
            }
            listView2.EndUpdate();//结束数据处理，UI界面一次性绘制
        }
 
        private void button5_Click(object sender, EventArgs e)
        {
            listView2.Clear();
            //生成表头  
            listView2.Columns.Add("员工号", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("员工名", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("公司名", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            listView2.Columns.Add("薪水", listView1.Width / 4 - 1, HorizontalAlignment.Left);
            //表的内容
            DataTable table = db.getBySql(@"select [EMPLOYEE].[EmpNo],[EMPLOYEE].[EmpName],[COMPANY].[CmpName],[WORKS].[Salary] from [EMPLOYEE],[COMPANY],[WORKS]" +
            " where [EMPLOYEE].[EmpNo]=[WORKS].[EmpNo]" +
            " and [COMPANY].[CmpNo]=[WORKS].[CmpNo]" +
            " and [EMPLOYEE].[EmpName]='" + comboBox2.Text + "'");
            listView2.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();//生成每一列  
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j <= 0)
                    {
                        listViewItem.Text = table.Rows[i][j] + "";
                    }
                    else
                    {
                        listViewItem.SubItems.Add(table.Rows[i][j] + "");
                    }
                }
                listView2.Items.Add(listViewItem);
            }
            listView2.EndUpdate();//结束数据处理，UI界面一次性绘制
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
namespace SCUT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            if ((int)Intent.dict["form1_flag"] == 0)
            {
                this.Text = Intent.dict["form1_text"] + "";
                textBox1.Focus();//设置焦点停留在textBox1中   
            }
            else
            {
                this.Text = Intent.dict["form1_text"] + "";
                textBox1.Text = Intent.dict["form1_selectedItems0"] + "";
                textBox2.Text = Intent.dict["form1_selectedItems1"] + "";
                if (Intent.dict["form1_selectedItems2"] + "" == "男")
                {
                    radioButton1.Checked = true;
                }
                else {
                    radioButton2.Checked = true;
                }
                textBox3.Text = Intent.dict["form1_selectedItems3"] + "";
                textBox1.Focus();//设置焦点停留在textBox1中   
                textBox1.SelectAll();//要先有焦点才能全选  
            }  
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("任意一项没有完成填写！", this.Text);
            }
            else
            {
                //关闭form2之间，将要传给form1的值压入Intent中的dict    
                Intent.dict["form2_textbox1_text"] = textBox1.Text;
                Intent.dict["form2_textbox2_text"] = textBox2.Text;
                if (radioButton1.Checked)
                {
                    Intent.dict["form2_radioButton"] = "男";
                }
                else
                {
                    Intent.dict["form2_radioButton"] = "女";
                }
                Intent.dict["form2_textbox3_text"] = textBox3.Text;
                this.DialogResult = DialogResult.OK;//同时设置返回值为OK，不设置的话，默认返回Cancel    
                this.Close();
            }
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}

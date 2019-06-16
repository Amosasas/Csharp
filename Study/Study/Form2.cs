using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study
{
    public partial class Form2 : Form
    {

        private string FontS = string.Empty;

        public Form2()
        {
            InitializeComponent();
        }

        private void ModifyFont(object sender, EventArgs e)
        {
            foreach (var ctr in groupBox1.Controls)
            {
                if (ctr is RadioButton)
                {
                    if ((ctr as RadioButton).Checked)
                    {
                        FontS = (ctr as RadioButton).Text;
                        (this.Owner as Form1).Font = new Font((ctr as RadioButton).Text, 14);
                        break;
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach(var ctr in groupBox1.Controls)
            {
                if(ctr is RadioButton)
                {
                    (ctr as RadioButton).CheckedChanged += ModifyFont;   
                }
            }

            for(var i = 1; i <= 20; i++)
            {
                comboBox1.Items.Add(i);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
               (this.Owner as Form1).Font = new Font(FontS, float.Parse(comboBox1.Text));
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}

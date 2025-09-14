using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTLVC_Control_Panel
{
    public partial class AddContestant : Form
    {
        public AddContestant()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalConts.GlobalCont.newContName = textBox1.Text;
            textBox1.Text = "";

            GlobalConts.GlobalCont.updateFlag1 = true;
            this.Close();
        }
    }
}

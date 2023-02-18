using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_tac_toe
{
    public partial class MenuForm : Form
    {
        public static  MenuForm instance;
        public MenuForm()
        {
            instance = this;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPlayerVsPlayer formPlayerVsPlayer = new FormPlayerVsPlayer();
            formPlayerVsPlayer.Show();
        }
    }
}

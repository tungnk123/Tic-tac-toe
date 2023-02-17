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
    public partial class Form1 : Form
    {
        bool turn = true; //true = left Turn; false = right turn
        public bool Turn
        {
            get
            {
                return turn;
            }
            set
            {
                turn = value;
            }
        }
        int turn_count = 0;
        public int Turn_Count
        {
            get
            {
                return turn_count;
            }
            set
            {
                turn_count = value;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (turn)
            {
                button.Text = "X";
            }
            else
            {
                button.Text = "O";
            }
            turn = !turn;
            button.Enabled = false;
            turn_count++;
            CheckWinner();
        }
        public void CheckWinner()
        {
            if (turn_count == 9)
            {
                MessageBox.Show("Draw!");
                return;
            }
            bool there_is_a_winner = false;
            // horizontal check
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && !B1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && !C1.Enabled)
            {
                there_is_a_winner = true;
            }

            //
            // vertical check
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && !A2.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && !A3.Enabled)
            {
                there_is_a_winner = true;
            }

            //
            // diagonal check
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && !A3.Enabled)
            {
                there_is_a_winner = true;
            }

            

            if (there_is_a_winner)
            {
                string winner = "";
                if (turn)
                {
                    winner = "O";
                }
                else
                {
                    winner = "X";
                }

                MessageBox.Show($"{winner} is win!");
            }
            
        }
    }
}

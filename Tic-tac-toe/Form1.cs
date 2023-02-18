using System;
using System.Windows.Forms;

namespace Tic_tac_toe
{
    public partial class FormPlayerVsPlayer : Form
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
        public int count_left = 0;
        public int count_right = 0;
        public FormPlayerVsPlayer()
        {
            InitializeComponent();
        }
        #region Button Events
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (turn)
            {
                button.Tag = "X";
                button.BackgroundImage = Tic_tac_toe.Properties.Resources.x;
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                button.Tag = "O";
                button.BackgroundImage = Tic_tac_toe.Properties.Resources.O;
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            turn = !turn;
            button.Enabled = false;
            turn_count++;
            CheckWinner();
        }
        public void CheckWinner()
        {
            bool there_is_a_winner = false;
            //
            // horizontal check
            if ((A1.Tag == A2.Tag) && (A2.Tag == A3.Tag) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((B1.Tag == B2.Tag) && (B2.Tag == B3.Tag) && !B1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((C1.Tag == C2.Tag) && (C2.Tag == C3.Tag) && !C1.Enabled)
            {
                there_is_a_winner = true;
            }

            //
            // vertical check
            if ((A1.Tag == B1.Tag) && (B1.Tag == C1.Tag) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A2.Tag == B2.Tag) && (B2.Tag == C2.Tag) && !A2.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A3.Tag == B3.Tag) && (B3.Tag == C3.Tag) && !A3.Enabled)
            {
                there_is_a_winner = true;
            }

            //
            // diagonal check
            if ((A1.Tag == B2.Tag) && (B2.Tag == C3.Tag) && !A1.Enabled)
            {
                there_is_a_winner = true;
            }
            else if ((A3.Tag == B2.Tag) && (B2.Tag == C1.Tag) && !A3.Enabled)
            {
                there_is_a_winner = true;
            }

            if (there_is_a_winner)
            {
                string winner = "";
                if (turn)
                {
                    winner = "O";
                    count_right++;
                }
                else
                {
                    winner = "X";
                    count_left++;
                }

                MessageBox.Show($"{winner} is win!");
                labelCountLeft.Text = count_left.ToString();
                labelCountRight.Text = count_right.ToString();
                DisableButton();
                buttonRestart.Visible = true;
                return;
            }
            if (turn_count == 9)
            {
                MessageBox.Show("Draw!");
                buttonRestart.Visible = true;
                return;
            }

        }
        public void DisableButton()
        {
            foreach (Button button in panel1.Controls)
            {
                button.Enabled = false;
            }
        }
        private void Button_Enter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Enabled)
            {
                if (turn)
                {
                    button.Tag = "X";
                    button.BackgroundImage = Tic_tac_toe.Properties.Resources.x;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    button.Tag = "O";
                    button.BackgroundImage = Tic_tac_toe.Properties.Resources.O;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void Button_Leave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Enabled)
            {
                button.Enabled = true;
                button.Tag = "";
                button.BackgroundImage = null;
            }
        }
        #endregion

        #region Menu Events
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
            buttonRestart.Visible = false;
            count_left = 0;
            count_right = 0;
            labelCountLeft.Text = count_left.ToString();
            labelCountRight.Text = count_right.ToString();
            foreach (Button button in panel1.Controls)
            {
                button.Enabled = true;
                button.Tag = "";
                button.BackgroundImage = null;
            }
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
        }

        private void FormPlayerVsPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MenuForm.instance.Show();
        }

        #endregion

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
            buttonRestart.Visible = false;
            foreach (Button button in panel1.Controls)
            {
                button.Enabled = true;
                button.Tag = "";
                button.BackgroundImage = null;
            }
        }


    }
}

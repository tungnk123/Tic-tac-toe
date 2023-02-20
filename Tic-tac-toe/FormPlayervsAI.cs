using System;
using System.Windows.Forms;

namespace Tic_tac_toe
{
    public partial class FormPlayerVsAI : Form
    {
        bool turn = true; //true = left Turn; false = right turn
        public bool isPlayerWin = false;

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
        public FormPlayerVsAI()
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
                turn_count++;
                button.Enabled = false;
                CheckWinner();
                if (isPlayerWin)
                {
                    return;
                }
                

                if (turn_count != 9)
                {
                    Button computer_button = computer_make_move();
                    if (computer_button != null)
                    {
                        computer_button.Tag = "O";
                        computer_button.BackgroundImage = Tic_tac_toe.Properties.Resources.o_white;
                        computer_button.BackgroundImageLayout = ImageLayout.Stretch;
                        computer_button.Enabled = false;
                    }
                    turn_count++;
                    CheckWinner();
                }
            }
            

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
                if (turn_count % 2 == 0)
                {
                    winner = "Computer";
                    count_right++;
                }
                else
                {
                    winner = "Player";
                    count_left++;
                    isPlayerWin = true;
                }

                MessageBox.Show($"{winner} is win!", "Tic Tac Toe Tung");
                labelCountLeft.Text = count_left.ToString();
                labelCountRight.Text = count_right.ToString();
                DisableButton();
                buttonRestart.Visible = true;
                return;
            }
            if (turn_count == 9)
            {
                MessageBox.Show("Draw!", "Tic Tac Toe Tung");
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
            labelCountLeft.Tag = count_left.ToString();
            labelCountRight.Tag = count_right.ToString();
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
            MenuForm.instance.Show();
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
            isPlayerWin = false;
            foreach (Button button in panel1.Controls)
            {
                button.Enabled = true;
                button.Tag = "";
                button.BackgroundImage = null;
            }
        }

        #region AI code
        private Button computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space

            Button move = null;

            //look for tic tac toe opportunities
            move = look_for_win_or_block("O"); //look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if

            //if (move != null)
            //{
            //    move.PerformClick();
            //}
            return move;
        }
        private Button look_for_open_space()
        {
            Button b = null;
            foreach (Button c in panel1.Controls)
            {
                b = (Button)c;

                if (b != null)
                {
                    if ((string)b.Tag == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            if ((string)B2.Tag == "")
            {
                return B2;
            }
            if ((string)A1.Tag == "O")
            {
                if ((string)A3.Tag == "")
                    return A3;
                if ((string)C3.Tag == "")
                    return C3;
                if ((string)C1.Tag == "")
                    return C1;
            }

            if ((string)A3.Tag == "O")
            {
                
                if ((string)A1.Tag == "")
                    return A1;
                if ((string)C3.Tag == "")
                    return C3;
                if ((string)C1.Tag == "")
                    return C1;
            }

            if ((string)C3.Tag == "O")
            {
                
                if ((string)A1.Tag == "")
                    return A3;
                if ((string)A3.Tag == "")
                    return A3;
                if ((string)C1.Tag == "")
                    return C1;
            }

            if ((string)C1.Tag == "O")
            {
                
                if ((string)A1.Tag == "")
                    return A3;
                if ((string)A3.Tag == "")
                    return A3;
                if ((string)C3.Tag == "")
                    return C3;
            }

            if ((string)A1.Tag == "")
                return A1;
            if ((string)A3.Tag == "")
                return A3;
            if ((string)C1.Tag == "")
                return C1;
            if ((string)C3.Tag == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(object mark)
        {
            //HORIZONTAL TESTS
            if ((A1.Tag == mark) && (A2.Tag == mark) && ((string)A3.Tag == ""))
                return A3;
            if ((A2.Tag == mark) && (A3.Tag == mark) && ((string)A1.Tag == ""))
                return A1;
            if ((A1.Tag == mark) && (A3.Tag == mark) && ((string)A2.Tag == ""))
                return A2;

            if ((B1.Tag == mark) && (B2.Tag == mark) && ((string)B3.Tag == ""))
                return B3;
            if ((B2.Tag == mark) && (B3.Tag == mark) && ((string)B1.Tag == ""))
                return B1;
            if ((B1.Tag == mark) && (B3.Tag == mark) && ((string)B2.Tag == ""))
                return B2;

            if ((C1.Tag == mark) && (C2.Tag == mark) && ((string)C3.Tag == ""))
                return C3;
            if ((C2.Tag == mark) && (C3.Tag == mark) && ((string)C1.Tag == ""))
                return C1;
            if ((C1.Tag == mark) && (C3.Tag == mark) && ((string)C2.Tag == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Tag == mark) && (B1.Tag == mark) && ((string)C1.Tag == ""))
                return C1;
            if ((B1.Tag == mark) && (C1.Tag == mark) && ((string)A1.Tag == ""))
                return A1;
            if ((A1.Tag == mark) && (C1.Tag == mark) && ((string)B1.Tag == ""))
                return B1;

            if ((A2.Tag == mark) && (B2.Tag == mark) && ((string)C2.Tag == ""))
                return C2;
            if ((B2.Tag == mark) && (C2.Tag == mark) && ((string)A2.Tag == ""))
                return A2;
            if ((A2.Tag == mark) && (C2.Tag == mark) && ((string)B2.Tag == ""))
                return B2;

            if ((A3.Tag == mark) && (B3.Tag == mark) && ((string)C3.Tag == ""))
                return C3;
            if ((B3.Tag == mark) && (C3.Tag == mark) && ((string)A3.Tag == ""))
                return A3;
            if ((A3.Tag == mark) && (C3.Tag == mark) && ((string)B3.Tag == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Tag == mark) && (B2.Tag == mark) && ((string)C3.Tag == ""))
                return C3;
            if ((B2.Tag == mark) && (C3.Tag == mark) && ((string)A1.Tag == ""))
                return A1;
            if ((A1.Tag == mark) && (C3.Tag == mark) && ((string)B2.Tag == ""))
                return B2;

            if ((A3.Tag == mark) && (B2.Tag == mark) && ((string)C1.Tag == ""))
                return C1;
            if ((B2.Tag == mark) && (C1.Tag == mark) && ((string)A3.Tag == ""))
                return A3;
            if ((A3.Tag == mark) && (C1.Tag == mark) && ((string)B2.Tag == ""))
                return B2;
            //Special TESTS
            if ((B3.Tag == mark) && (C1.Tag == mark) && ((string)C3.Tag == ""))
                return C3;
            if ((C3.Tag == mark) && (A1.Tag == mark) && ((string)A2.Tag == ""))
                return A2;
            if ((A3.Tag == mark) && (C1.Tag == mark) && ((string)A2.Tag == ""))
                return A2;

            return null;
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

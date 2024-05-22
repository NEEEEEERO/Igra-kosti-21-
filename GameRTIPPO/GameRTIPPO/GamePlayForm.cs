using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GameRTIPPO
{

    public partial class GamePlayForm : Form
    {
        public event EventHandler RollButtonClicked;
        //Перемещение формы
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        //Перемещение формы
        private void GamePlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void GamePlayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void GamePlayForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        Bitmap[] dice_wall = new Bitmap[6];
        Dice dice = new Dice();

        public GamePlayForm()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //Картинки куба
            dice_wall[0] = new Bitmap(GameRTIPPO.Properties.Resources.One);
            dice_wall[1] = new Bitmap(GameRTIPPO.Properties.Resources.Two);
            dice_wall[2] = new Bitmap(GameRTIPPO.Properties.Resources.Three);
            dice_wall[3] = new Bitmap(GameRTIPPO.Properties.Resources.Four);
            dice_wall[4] = new Bitmap(GameRTIPPO.Properties.Resources.Five);
            dice_wall[5] = new Bitmap(GameRTIPPO.Properties.Resources.Six);
        }

        private void GamePlayForm_Load(object sender, EventArgs e)
        {
           
        }

        public void RemoveLabel(int line)
        {
            switch (line)
            {
                case 2:
                    panel5.Dispose();
                    panel10.Dispose();
                    break;
                case 3:
                    panel4.Dispose();
                    panel9.Dispose();
                    panel5.Dispose();
                    panel10.Dispose();
                    break;
                case 4:
                    panel3.Dispose();
                    panel8.Dispose();
                    panel4.Dispose();
                    panel9.Dispose();
                    panel5.Dispose();
                    panel10.Dispose();
                    break;
            }
        }
        List<int> players = new List<int>();
        Player[] AllPeople;
        List<System.Windows.Forms.TextBox> pointLabelPlayer;
        Dictionary<string, int> playerPoints = new Dictionary<string, int>();

        public void FillLobby(int line, Player[] player)
        {
            ChangeStepColor();
            var avatars = new List<System.Windows.Forms.PictureBox>();
            avatars.AddRange(new System.Windows.Forms.PictureBox[]  {
                AvatarRed,
                AvatarBlue,
                AvatarGreen,
                AvatarPurple,
                AvatarOrange,
            });

            var nicknamesBalancePoints = new List<System.Windows.Forms.Label>();
            nicknamesBalancePoints.AddRange(new System.Windows.Forms.Label[]  {
                NicknameRed,
                NicknameBlue,
                NicknameGreen,
                NicknamePurple,
                NicknameOrange,
                BalanceRed,
                BalanceBlue,
                BalanceGreen,
                BalancePurple,
                BalanceOrange,
            });

            var Points = new List<System.Windows.Forms.TextBox>();
            Points.AddRange(new System.Windows.Forms.TextBox[]  {
                textBoxRed,
                textBoxBlue,
                textBoxGreen,
                textBoxPurple,
                textBoxOrange
            });

            pointLabelPlayer = Points;
            player[10].Balance = 3;

            int k;
            AllPeople = player;
            switch (line)
            {
                case 1:
                    k = 3;
                    for (int i = 0; i < 4; i++)
                    {
                        avatars[i].Image = player[k].Image;
                        nicknamesBalancePoints[i].Text = player[k].Name;
                        nicknamesBalancePoints[i + 5].Text = Convert.ToString(player[k].Balance-1);
                        Points[i].Text = "***";
                        players.Add(k);
                        k--;
                    }
                    avatars[4].Image = player[10].Image;
                    nicknamesBalancePoints[4].Text = player[10].Name;
                    nicknamesBalancePoints[9].Text = Convert.ToString(player[10].Balance-1);
                    Points[4].Text = "***";
                    players.Add(10);
                    break;
                case 2:
                    k = 6;
                    for (int i = 0; i < 3; i++)
                    {
                        avatars[i].Image = player[k].Image;
                        nicknamesBalancePoints[i].Text = player[k].Name;
                        nicknamesBalancePoints[i + 5].Text = Convert.ToString(player[k].Balance-1);
                        Points[i].Text = "***";
                        players.Add(k);
                        k--;
                    }
                    avatars[3].Image = player[10].Image;
                    nicknamesBalancePoints[3].Text = player[10].Name;
                    nicknamesBalancePoints[8].Text = Convert.ToString(player[10].Balance-1);
                    Points[4].Text = "***";

                    players.Add(10);
                    break;
                case 3:
                    k = 8;
                    for (int i = 0; i < 2; i++)
                    {
                        avatars[i].Image = player[k].Image;
                        nicknamesBalancePoints[i].Text = player[k].Name;
                        nicknamesBalancePoints[i + 5].Text = Convert.ToString(player[k].Balance-1);
                        Points[i].Text = "***";
                        players.Add(k);
                        k--;
                    }
                    avatars[2].Image = player[10].Image;
                    nicknamesBalancePoints[2].Text = player[10].Name;
                    nicknamesBalancePoints[7].Text = Convert.ToString(player[10].Balance-1);
                    Points[3].Text = "***";
                    players.Add(10);
                    break;
                case 4:
                    //Бот
                    avatars[0].Image = player[9].Image;
                    nicknamesBalancePoints[0].Text = player[9].Name;
                    nicknamesBalancePoints[5].Text = Convert.ToString(player[9].Balance-1);
                    Points[1].Text = "***";
                    players.Add(9);
                    //Вы
                    avatars[1].Image = player[10].Image;
                    nicknamesBalancePoints[1].Text = player[10].Name;
                    nicknamesBalancePoints[6].Text = Convert.ToString(player[10].Balance-1);
                    Points[2].Text = "***";
                    players.Add(10);
                    break;
            }
            BetPoints();
        }

        Bank bankPoints = new Bank(0);
        int winners = 0;
        private void BetPoints()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (AllPeople[players[i]].Balance != 0)
                {
                    AllPeople[players[i]].Balance = AllPeople[players[i]].Balance - 1;
                    bankPoints.AddPoints(1);
                }

            }
            textBox3.Text = "Банк: " + bankPoints.TotalPoints.ToString();
        }

        private void ChangeStepColor()
        {
            var panelsStep = new List<Panel>();
            panelsStep.AddRange(new Panel[]  {
                panel6,
                panel7,
                panel8,
                panel9,
                panel10,
            });

            var Points = new List<System.Windows.Forms.TextBox>();
            Points.AddRange(new System.Windows.Forms.TextBox[]  {
                textBoxRed,
                textBoxBlue,
                textBoxGreen,
                textBoxPurple,
                textBoxOrange
            });

            Color newColor = Color.FromArgb(254, 254, 254);
            panelsStep[number].BackColor = newColor;
            Points[number].BackColor = newColor;
        }

        private void ResetStepColor()
        {
            var panelsStep = new List<Panel>();
            panelsStep.AddRange(new Panel[]  {
                panel6,
                panel7,
                panel8,
                panel9,
                panel10,
            });

            var Points = new List<System.Windows.Forms.TextBox>();
            Points.AddRange(new System.Windows.Forms.TextBox[]  {
                textBoxRed,
                textBoxBlue,
                textBoxGreen,
                textBoxPurple,
                textBoxOrange
            });

            Color newColor = Color.FromArgb(0, 0, 0);
            panelsStep[number].BackColor = newColor;
            Points[number].BackColor = newColor;
        }

        bool gameEnd = false;
        int number = 0;
        int totalDiedPlayers = 0;

        private async void ButtonRoll_Click(object sender, EventArgs e)
        {
            if(number != players.Count)
            {
                ButtonSkip.Enabled = false;
                ButtonRoll.Enabled = false;
                for (int i = 0; i < 5; i++)
                {
                    Random roller = new Random();
                    pictureBox2.Image = dice_wall[roller.Next(0, 5) + 1];
                    await Task.Delay(100);
                }
                pictureBox2.Image = dice_wall[await Roll()];
                UpdateValue();
                await Task.Delay(2000);
            }
            else if(number == players.Count)
            {
                ButtonSkip.Enabled = false;
                ButtonRoll.Enabled = false;
                gameEnd = true;
                GameEnd();
            }
        }

        private async void UpdateValue()
        {
            var panelsHidden = new List<Panel>();
            panelsHidden.AddRange(new Panel[]  {
                panel1,
                panel2,
                panel3,
                panel4,
                panel5,
            });
            while (AllPeople[players[number]].isKicked == true)
            {
                number++;
                ChangeStepColor();
            }
            if (dice.TotalValues < 21)
            {
                ShowSum.Text = dice.TotalValues.ToString();
                textBox1.Text = AllPeople[players[number]].Name + " выбил число " + (dice.LastRoll + 1);
                AllPeople[players[number]].Points = Convert.ToInt32(dice.TotalValues);
            }
            else if (dice.TotalValues == 21)
            {
                AllPeople[players[number]].isWinner = true;
                AllPeople[players[number]].Points = Convert.ToInt32(dice.TotalValues);
                textBox1.Text = AllPeople[players[number]].Name + ", вы выбили 21!";
                await Task.Delay(3050);
                textBox1.Text = " ";
                dice = new Dice();
                ShowSum.Text = dice.TotalValues.ToString();
                ResetStepColor();
                winners++;
                //textBox4.Text = winners.ToString();
                number++;
                await Task.Delay(750);
                if (number == players.Count)
                {
                    ButtonSkip.Enabled = false;
                    ButtonRoll.Enabled = false;
                    gameEnd = true;
                    GameEnd();
                }
                ChangeStepColor();
            }
            else
            {
                textBox1.Text = AllPeople[players[number]].Name + ", вы проиграли! Сумма: " + dice.TotalValues.ToString();
                AllPeople[players[number]].Points = Convert.ToInt32(dice.TotalValues);
                dice = new Dice();
                ShowSum.Text = dice.TotalValues.ToString();
                ResetStepColor();
                number++;
                await Task.Delay(3050);
                textBox1.Text = " ";
                if (number == players.Count)
                {
                    ButtonSkip.Enabled = false;
                    ButtonRoll.Enabled = false;
                    gameEnd = true;
                    GameEnd();
                }
                ChangeStepColor();
            }
            ButtonSkip.Enabled = true;
            ButtonRoll.Enabled = true;

        }

        private async void GameEnd()
        {
            var panelsHidden = new List<Panel>();
            panelsHidden.AddRange(new Panel[]  {
                panel1,
                panel2,
                panel3,
                panel4,
                panel5,
            });

            int max = 0;
            if (gameEnd == true)
            {
                if (winners == 0 && (totalDiedPlayers == players.Count - 1) == false)
                {
                    //Поиск максимального числа в игроках
                    max = 0;
                    foreach (var checking in playerPoints)
                    {
                        if (checking.Value >= max && checking.Value < 22)
                        {
                            max = checking.Value;
                        }
                    }

                    foreach (var checking in playerPoints)
                    {
                        //Здесь проверяем количество победителей
                        if (checking.Value == max)
                        {
                            winners++;
                        }

                        //Здесь ищем победителя и делаем его победителем
                        foreach (var checkwinner in AllPeople)
                        {

                            if (checkwinner.Name == checking.Key && checkwinner.Points == max)
                            {
                                checkwinner.isWinner = true;
                                break;
                            }
                        }
                    }
                    if(winners != 0)
                    {
                        double bankValue = bankPoints.TakeAllPoints() / winners;
                        int bankValueRounded = (int)Math.Round(bankValue);
                        //Показать очки в конце раунда
                        var Points = new List<System.Windows.Forms.TextBox>();
                        Points.AddRange(new System.Windows.Forms.TextBox[]  {
                                textBoxRed,
                                textBoxBlue,
                                textBoxGreen,
                                textBoxPurple,
                                textBoxOrange
                            });

                        for (int i = 0; i < players.Count; i++)
                        {
                            Points[i].Text = AllPeople[players[i]].Points.ToString();
                        }
                        await Task.Delay(4000);
                        for (int i = 0; i < players.Count; i++)
                        {
                            Points[i].Text = "***";
                        }
                        //
                        for (int i = 0; i < players.Count; i++)
                        {

                            if (AllPeople[players[i]].isWinner == true)
                            {
                                Color newColor = Color.FromArgb(255, 215, 0);
                                panelsHidden[i].BackColor = newColor;
                                textBox4.Text = "Победитель: " + AllPeople[players[i]].Name;
                                AllPeople[players[i]].Balance += bankValueRounded;
                                AllPeople[players[i]].isWinner = false;
                                await Task.Delay(2000);
                                gameEnd = false;
                            }
                            else if (AllPeople[players[i]].isKicked != true)
                            {
                                if (AllPeople[players[i]].Balance == 0)
                                {
                                    AllPeople[players[i]].isKicked = true;
                                    totalDiedPlayers++;
                                }
                            }
                            //Здесь скрываются суммы игроков по окончании раунда
                            if (AllPeople[players[i]].isKicked != true) Points[i].Text = "***";
                        }
                    }

                }
                else if (winners > 0 && (totalDiedPlayers == players.Count - 1) == false)
                {
                    double bankValue = bankPoints.TakeAllPoints() / winners;
                    bankValue = Math.Round(bankValue, 2);
                    //Показать очки в конце раунда
                    var Points = new List<System.Windows.Forms.TextBox>();
                    Points.AddRange(new System.Windows.Forms.TextBox[]  {
                                textBoxRed,
                                textBoxBlue,
                                textBoxGreen,
                                textBoxPurple,
                                textBoxOrange
                            });

                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isKicked != true) Points[i].Text = AllPeople[players[i]].Points.ToString();
                    }
                    await Task.Delay(4000);
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isKicked != true) Points[i].Text = "***";
                    }
                    //
                    foreach (var playerAlive in AllPeople)
                    {
                        if (playerAlive.isWinner == true)
                        {
                            playerAlive.Balance += Convert.ToInt32(bankValue);
                            playerAlive.isWinner = false;
                        }
                        if (playerAlive.Balance == 0)
                        {
                            playerAlive.isKicked = true;
                            totalDiedPlayers++;
                        }
                    }
                }

                var colors = new List<Color>();
                colors.AddRange(new Color[]  {
                        Color.FromArgb(198, 69, 66),
                        Color.FromArgb(49, 161, 232),
                        Color.FromArgb(132, 197, 70),
                        Color.FromArgb(195, 133, 231),
                        Color.FromArgb(228, 157, 70),
                        });

                var balanceUpdate = new List<System.Windows.Forms.Label>();
                balanceUpdate.AddRange(new System.Windows.Forms.Label[]  {
                        BalanceRed,
                        BalanceBlue,
                        BalanceGreen,
                        BalancePurple,
                        BalanceOrange,
                        });

                if (totalDiedPlayers == players.Count - 1)
                {
                    foreach (var checking in playerPoints)
                    {
                        //Здесь ищем победителя и делаем его победителем
                        foreach (var checkwinner in AllPeople)
                        {
                            if (checkwinner.Name == checking.Key && checkwinner.Points == max)
                            {
                                checkwinner.isWinner = true;
                            }
                        }
                    }
                    double bankValue = bankPoints.TakeAllPoints() / winners;
                    int bankValueRounded = (int)Math.Round(bankValue);
                    //Показать очки в конце раунда
                    var Points = new List<System.Windows.Forms.TextBox>();
                    Points.AddRange(new System.Windows.Forms.TextBox[]  {
                                textBoxRed,
                                textBoxBlue,
                                textBoxGreen,
                                textBoxPurple,
                                textBoxOrange
                     });

                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isKicked != true) Points[i].Text = AllPeople[players[i]].Points.ToString();
                    }
                    await Task.Delay(4000);
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isKicked != true) Points[i].Text = "***";
                    }
                    //***
                    //Конец матча и показываем победителя
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isWinner == true)
                        {
                            AllPeople[players[i]].Balance += bankValueRounded;
                            AllPeople[players[i]].isWinner = false;
                            textBox2.Text = "Победа!";
                            textBox1.Text = AllPeople[players[i]].Name + " победил!";
                            textBox3.Visible = false;
                            ButtonSkip.Enabled = false;
                            ButtonRoll.Enabled = false;
                            buttonClose.Visible = true;
                            buttonClose.Enabled = true;
                            Color newColor = Color.FromArgb(255, 215, 0);
                            panelsHidden[i].BackColor = newColor;
                        }
                        else if(AllPeople[players[i]].Balance == 0)
                        {
                            AllPeople[players[i]].Points = 0;
                            pointLabelPlayer[i].Text = "L";
                            panelsHidden[i].BackColor = Color.FromArgb(36, 41, 44);
                        }
                    }
                    if (players.Count == 2)
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                            if (AllPeople[players[i]].isKicked == true) { 
                                AllPeople[players[i]].Points = 0;
                                pointLabelPlayer[i].Text = "L";
                                balanceUpdate[i].Text = AllPeople[players[i]].Balance.ToString();
                                panelsHidden[i].BackColor = Color.FromArgb(36, 41, 44);
                            }
                        }
                    }
                    foreach (var ResetInfo in AllPeople)
                    {
                        ResetInfo.Points = 0;
                        ResetInfo.Balance = 3;
                        ResetInfo.isWinner = false;
                        ResetInfo.isKicked = false;
                    }
                }
                else
                {
                    ButtonSkip.Enabled = false;
                    ButtonRoll.Enabled = false;
                    await Task.Delay(1500);
                    if(winners != 0)
                    {
                        BetPoints();
                    }
                    ResetStepColor();
                    number = 0;
                    winners = 0;
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (AllPeople[players[i]].isKicked == false)
                        {
                            AllPeople[players[i]].Points = 0;
                            balanceUpdate[i].Text = AllPeople[players[i]].Balance.ToString();
                            panelsHidden[i].BackColor = colors[i];
                        }
                        else
                        {
                            AllPeople[players[i]].Points = 0;
                            pointLabelPlayer[i].Text = "L";
                            balanceUpdate[i].Text = AllPeople[players[i]].Balance.ToString();
                            panelsHidden[i].BackColor = Color.FromArgb(36, 41, 44);
                        }
                    }

                    playerPoints.Clear();
                    textBox4.Text = " ";
                    textBox2.Text = "Начинаем новый раунд!";
                    await Task.Delay(2000);
                    textBox2.Text = "ㅤ";
                    textBox1.Text = " ";
                    ChangeStepColor();

                    await Task.Delay(2000);
                    ButtonSkip.Enabled = true;
                    ButtonRoll.Enabled = true;
                    gameEnd = false;
                }

            }
        }

        //Завершить ход
        private void ButtonSkip_Click(object sender, EventArgs e)
        {
            playerPoints.Add(AllPeople[players[number]].Name, AllPeople[players[number]].Points);
            ShowSum.Text = "0";
            ChatInfo.Text = "ㅤ";
            textBox1.Text = " ";
            dice = new Dice();
            ResetStepColor();
            while ((number != players.Count - 1) && AllPeople[players[number + 1]].isKicked == true)
            {
                number += 1;
            }
            number++;
            ChangeStepColor();
            if (number == players.Count || (AllPeople[players[number]].isKicked == true && number+1 == players.Count))
            {
                ButtonSkip.Enabled = false;
                ButtonRoll.Enabled = false;
                gameEnd = true;
                GameEnd();
            }
        }

        //Прокрутка с задержкой для анимации
        private async Task<int> Roll()
        {
            //Brief pause
            await Task.Delay(500);
            //Roll dice
            return dice.RollNumber();
        }

        //Назад в лобби
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Application.OpenForms["Form1"] != null)
            {
                Form1 form1 = (Form1)Application.OpenForms["Form1"];
                form1.Visible = true;
            }
        }
    }
}

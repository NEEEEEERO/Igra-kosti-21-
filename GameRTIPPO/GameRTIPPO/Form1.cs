using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace GameRTIPPO
{
    public partial class Form1 : Form
    {
        private GamePlayForm GamePlayForm; //Ссылка на вторую форму
        //Перемещение формы
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        //Закругление углов
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-Координата левого угла
            int nTopRect,      // y-Координата левого угла
            int nRightRect,    // x-Координата правого угла
            int nBottomRect,   // y-Координата правого угла
            int nWidthEllipse, // ширина эллипса
            int nHeightEllipse // высота эллипса
        );

        //Перемещение формы
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //Закругление панельки
            panel21.Paint += panel21_Paint;

            AssignRandomImage();
        }

        //Закругление панельки
        public void panel21_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(panel21.Parent.BackColor);
            Control control = panel21;
            int radius = 15;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddLine(radius, 0, control.Width - radius, 0);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddLine(control.Width, radius, control.Width, control.Height - radius);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddLine(control.Width - radius, control.Height, radius, control.Height);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.AddLine(0, control.Height - radius, 0, radius);
                path.AddArc(0, 0, radius, radius, 180, 90);
                using (SolidBrush brush = new SolidBrush(control.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AvPlusLineOne_Click(object sender, EventArgs e)
        {
            GamePlayForm Start = new GamePlayForm();
            Start.FillLobby(1, LobbyList);
            Start.Show();
            Start.FormClosed += (s, args) => this.Visible = true;
            this.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Player[] LobbyList;
        private void AssignRandomImage()
        {
            var boxes = new List<PictureBox>();
            boxes.AddRange(new PictureBox[]  {
                pictureBox4,
                pictureBox5,
                pictureBox6,
                pictureBox7,
                pictureBox8,
                pictureBox9,
                pictureBox10,
                pictureBox11,
                pictureBox12,
                pictureBox13,
            });

            var labels = new List<System.Windows.Forms.Label>();
            labels.AddRange(new System.Windows.Forms.Label[]  {
                label5,
                label6,
                label7,
                label8,
                label9,
                label10,
                label11,
                label12,
                label13,
                label14,
            });

            Player[] players = Player.CreatePlayers();
            for(int i = 0; i<10; i++)
            {
                labels[i].Text = players[i].Name;
                boxes[i].Image = players[i].Image;
            }
            LobbyList = players;
        }

        private void AvPlusLineFour_Click(object sender, EventArgs e)
        {
            GamePlayForm Start = new GamePlayForm();
            Start.RemoveLabel(4);
            Start.FillLobby(4, LobbyList);
            Start.Show();
            Start.FormClosed += (s, args) => this.Visible = true;
            this.Visible = false;
        }

        private void AvPlusLineThree_Click(object sender, EventArgs e)
        {
            GamePlayForm Start = new GamePlayForm();
            Start.RemoveLabel(3);
            Start.FillLobby(3, LobbyList);
            Start.Show();
            Start.FormClosed += (s, args) => this.Visible = true;
            this.Visible = false;
        }

        private void AvPlusLineTwo_Click(object sender, EventArgs e)
        {
            GamePlayForm Start = new GamePlayForm();
            Start.RemoveLabel(2);
            Start.FillLobby(2, LobbyList);
            Start.Show();
            Start.FormClosed += (s, args) => this.Visible = true;
            this.Visible = false;
        }
    }
}

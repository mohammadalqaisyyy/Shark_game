using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Mohammad Al-Qaisy

namespace Task_7
{
    public partial class Form1 : Form
    {
        private bool up, down, left, right;
        private int speed = 5, score = 100, _height, _width;
        private List<Panel> list = new List<Panel>();
        private List<bool> _in = new List<bool>();
        Random random = new Random(System.DateTime.Now.Millisecond);

        private GifImage gifImage = null;
        private string filePath = "diver3.gif";
        public Form1()
        {
            InitializeComponent();
            gifImage = new GifImage(filePath);
            gifImage.ReverseAtEnd = false;
            panel1.Location = new Point(0, 0);
            label1.Text = score.ToString();
            label1.Left = this.Width - 100;
            label2.Left = this.Width - 100;
            button1.Left = this.Width - button1.Width - 20;
            button1.Top = this.Height - button1.Height - 50;
            _height = this.Height;
            _width = this.Width;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                list.Add(shark(i, 0));
                this.Controls.Add(list[i]);
                _in.Add(false);
            }
        }

        Panel shark(int l,int add)
        {
            Panel x = new Panel();
            x.BackColor = Color.Transparent;
            x.BackgroundImageLayout = ImageLayout.Stretch;
            x.BackgroundImage = Properties.Resources.shark;
            x.Size= new System.Drawing.Size(220, 80);
            x.Location = new Point(random.Next(0, this.Width - x.Width),
                    random.Next(0, this.Height - x.Height));
            return x;
        }

        private void moveShark_Tick(object sender, EventArgs e)
        {
            reset();
            for (int i = 0; i < 5; i++)
            {
                list[i].Left -= 5;
                if (list[i].Left + list[i].Width <= 0)
                    list[i].Left = this.Width;
                this.Controls.Add(list[i]);
            }
            decScore();
        }

        void reset()
        {
            for (int i = 0; i < 5; i++)
                this.Controls.Remove(list[i]);
        }

        private void win_Tick(object sender, EventArgs e)
        {
            up = false; down = false; right = false; left = false;
            randomMove.Enabled = false; move.Enabled = false; win.Enabled = false;
            moveShark.Enabled = false; gif.Enabled = false;
            if (score > 0)
                MessageBox.Show("You Win");
            else
                MessageBox.Show("Game over");

            replay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            replay();
        }

        void replay()
        {
            panel1.Location = new Point(0, 0);
            up = false;down = false;right = false;left = false;
            reset();
            for (int i = 0; i < 5; i++)
            {
                list[i].Location = new Point(random.Next(0, this.Width - list[i].Width),
                    random.Next(0, this.Height - list[i].Height));
                this.Controls.Add(list[i]);
            }
            randomMove.Enabled = true; move.Enabled = true; win.Enabled = true;
            moveShark.Enabled = true; gif.Enabled = true;
            score = 100;
            label1.Text = score.ToString();
        }

        private void randomMove_Tick(object sender, EventArgs e)
        {
            reset(); 
            for (int i = 0; i < 5; i++)
            {
                list[i].Location = new Point(random.Next(0, this.Width-list[i].Width),
                    random.Next(0, this.Height-list[i].Height));
                this.Controls.Add(list[i]);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            label1.Left = this.Width - 100;
            label2.Left = this.Width - 100;
            button1.Left = this.Width - button1.Width - 20;
            button1.Top = this.Height - button1.Height - 50;
            double x = 0, y = 0;
            for (int i = 0; i < 5; i++)
            {
                if (_width == 0 || _height == 0)
                    break;
                x = (list[i].Location.X * 1.0 / _width) * this.Width;
                y = (list[i].Location.Y *1.0 / _height) * this.Height;
                list[i].Location = new Point((int)x, (int)y);
                this.Controls.Add(list[i]);
            }
            x = (panel1.Location.X * 1.0 / _width) * this.Width;
            y = (panel1.Location.Y * 1.0 / _height) * this.Height;
            panel1.Location = new Point((int)x, (int)y);
            _width = this.Width;
            _height = this.Height;
        }

        void decScore()
        {
            int xLeftPoint = panel1.Location.X, xRightPoint = panel1.Location.X + panel1.Width,
                    yTopPoint = panel1.Location.Y, yDownPoint = panel1.Location.Y + panel1.Height;
            int pLx, pRx, pTy, pDy;
            foreach (Panel t in list)
            {
                pLx = t.Location.X; pRx = t.Location.X + t.Width;
                pTy = t.Location.Y; pDy = t.Location.Y + t.Height;
                if (((pLx <= xLeftPoint && xLeftPoint <= pRx) || (pLx <= xRightPoint && xRightPoint <= pRx)) &&
                    ((pTy <= yTopPoint && yTopPoint <= pDy) || (pTy <= yDownPoint && yDownPoint <= pDy)))
                {
                    if (_in[list.IndexOf(t)] == false)
                    {
                        _in[list.IndexOf(t)] = true;
                        score -= 10;
                        label1.Text = score.ToString();
                        if (score == 0)
                        {
                            up = false; down = false; right = false; left = false;
                            randomMove.Enabled = false; move.Enabled = false; win.Enabled = false;
                            moveShark.Enabled = false; gif.Enabled = false;
                            MessageBox.Show("Game over");
                            replay();
                        }
                    }
                }
                else {
                    _in[list.IndexOf(t)] = false;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                up = true;
            if (e.KeyCode == Keys.Down)
                down = true;
            if (e.KeyCode == Keys.Left)
                left = true;
            if (e.KeyCode == Keys.Right)
                right = true;
            if (panel1.Left+ panel1.Width < 0)
                panel1.Location = new Point(this.Width, panel1.Top);
            if (panel1.Left > this.Width)
                panel1.Location = new Point(-panel1.Width, panel1.Top);
            if (panel1.Top + panel1.Height < 0)
                panel1.Location = new Point(panel1.Left, this.Height-50);
            if (panel1.Top > this.Height)
                panel1.Location = new Point(panel1.Left, -panel1.Height);
            decScore();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.BackgroundImage = gifImage.GetNextFrame();
        }

        private void moveTimer(object sender, EventArgs e)
        {
            if (left)
                panel1.Left -= speed;
            if (right)
                panel1.Left += speed;
            if (up)
                panel1.Top -= speed;
            if (down)
                panel1.Top += speed;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                up = false;
            if (e.KeyCode == Keys.Down)
                down = false;
            if (e.KeyCode == Keys.Left)
                left = false;
            if (e.KeyCode == Keys.Right)
                right = false;
            decScore();
        }
    }
}

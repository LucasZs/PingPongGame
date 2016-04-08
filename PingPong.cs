using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace PingPongGame
{
	public partial class formPingPong : Form
	{
		Graphics graphics;

		int xLeftRec = 150;
		int yTopRec = 400;
		int widthRec = 200;
		int heightRec = 20;

		int xLeftBall = 250;
		int yTopBall = 50;
		int widthBall = 50;
		int heightBall = 50;

		Rectangle paddle;
		Rectangle ball;

		static double exBall = new Random().Next(200, 800)/1000.0;
		static double eyBall = Math.Sqrt(1 - exBall * exBall);
		static int vBall = 7;

		static int score = 0;
		static int level = 1;
		
		static Image[] gifts = { new Bitmap(@"..\..\gifts\gift1.jpg"),
								 new Bitmap(@"..\..\gifts\gift2.jpg"),
								 new Bitmap(@"..\..\gifts\gift3.jpg"),
								 new Bitmap(@"..\..\gifts\gift4.jpg"),
								 new Bitmap(@"..\..\gifts\gift5.jpg") };

		SoundPlayer boing = new SoundPlayer(@"..\..\sounds\boing.wav");
		SoundPlayer looser = new SoundPlayer(@"..\..\sounds\looser.wav");

		public formPingPong()
		{
			InitializeComponent();
		}
		
		private void DrawPaddle()
		{
			graphics.FillRectangle(Brushes.Cyan, paddle);
			graphics.DrawRectangle(Pens.Red, paddle);
		}

		private void DrawBall()
		{
			graphics.FillEllipse(Brushes.Coral, ball);
			graphics.DrawEllipse(Pens.BlueViolet, ball);
		}

		private void DrawGameElements()
		{
			graphics = this.CreateGraphics();
			graphics.Clear(BackColor);
			DrawPaddle();
			DrawBall(); 
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			this.Text = "PING PONG GAME";
			buttonStart.Visible = false;
			buttonExit.Visible = false;
			timer1.Enabled = true;
			label1.Parent = this;
			label1.BackColor = Color.Transparent;
			label1.Visible = true;
			label1.Text = "Score: 0";

			label2.Enabled = true;
			label2.Parent = this;
			label2.BackColor = Color.Transparent;
			label2.Visible = true;
			label2.Text = "Level: 1";

			paddle = new Rectangle(xLeftRec, yTopRec, widthRec, heightRec);
			ball = new Rectangle(xLeftBall, yTopBall, widthBall, heightBall);

			DrawGameElements();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			xLeftBall += (int) (exBall * vBall);
			ball.X = xLeftBall;
			yTopBall += (int) (eyBall * vBall);
			ball.Y = yTopBall;
			boing.Dispose();
			if (xLeftBall+exBall <= 3 || xLeftBall +exBall>= this.Width - widthBall - 20)
			{
				exBall *= -1;
				boing.Play();
			}
			if (yTopBall +eyBall<= 3)
			{
				eyBall *= -1;
				boing.Play();
			}
			if ((xLeftBall + widthBall+exBall >= xLeftRec) && (xLeftBall+exBall <= xLeftRec + widthRec)
				&& (yTopBall +eyBall<= yTopRec - heightBall) && (yTopBall+eyBall >= yTopRec - heightBall - vBall))
			{
				if (xLeftBall + widthBall/2 > xLeftRec + (int)2.0/3.0*widthRec)
				{
					exBall = new Random().Next(200, 800) / 1000.0;
				}
				else if (xLeftBall + widthBall/2 < xLeftRec + (int)1.0/3.0*widthRec)
				{
					exBall = - new Random().Next(200, 800) / 1000.0;
				}
				boing.Play();
				eyBall = - Math.Sqrt(1 - exBall * exBall);
				score += 1;
				vBall += score/5;
				level = score/5 + 1;
				if (score%5==0)
				{
					Form1 giftswindow=new Form1();
					giftswindow.BackgroundImage = gifts[level - 2];
					giftswindow.Show();
				}
				
				if (level==6)
				{
					label1.Visible = false;
					label2.Visible = false;
					timer1.Enabled = false;
					MessageBox.Show("You are a really (Code)cool guy, bro!\n Check out your gifts before close", "Congratulation!");
				}
				label1.Text = "Score: " + score;
				label2.Text = "Level: " + level.ToString() + " (" + score % 5 * 20 + "%)";
				
			}
			else if (((xLeftBall < xLeftRec) || (xLeftBall > xLeftRec + widthRec - widthBall)) && (yTopBall > yTopRec))
			{
				timer1.Enabled = false;
				looser.Play();
				MessageBox.Show("You are a looser...\nScore: " + score + "\nLevel: " + level, "Looser");
			}
			DrawGameElements();
		}

		private void FormPingPong_MouseMove(object sender, MouseEventArgs e)
		{
			if (timer1.Enabled)
			{
				if (e.X <= widthRec / 2)
					xLeftRec = 0;
				else if (e.X >= this.Width - widthRec / 2)
					xLeftRec = this.Width - widthRec;
				else {
					xLeftRec = e.X - widthRec / 2;
				}
				paddle.X = xLeftRec;
			}
			else BeforeStart(e);
		}

		private void BeforeStart(MouseEventArgs e)
		{
			int xLeftButton = buttonExit.Location.X;
			int yTopButton = buttonExit.Location.Y;
			int widthButton = buttonExit.Width;
			int heightButton = buttonExit.Height;
			int xRightButton = xLeftButton + widthButton;
			int yBottomButton = yTopButton + heightButton;
			int delta = 50;

			if ((e.X <= xLeftButton) && (e.X >= xLeftButton - delta) && (e.Y <= yBottomButton) && (e.Y >= yTopButton))
			{
				xLeftButton += 10;
				buttonExit.Location = new Point(xLeftButton, yTopButton);
			}
			else if ((e.X >= xRightButton) && (e.X <= xRightButton + delta) && (e.Y <= yBottomButton) && (e.Y >= yTopButton))
			{
				xLeftButton -= 10;
				buttonExit.Location = new Point(xLeftButton, yTopButton);
			}
			else if ((e.Y <= yTopButton) && (e.Y >= yTopButton - delta) && (e.X >= xLeftButton) && (e.X <= xRightButton))
			{
				yTopButton += 10;
				buttonExit.Location = new Point(xLeftButton, yTopButton);
			}
			else if ((e.Y >= yBottomButton) && (e.Y <= yBottomButton + delta) && (e.X >= xLeftButton) && (e.X <= xRightButton))
			{
				yTopButton -= 10;
				buttonExit.Location = new Point(xLeftButton, yTopButton);
			}
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			MessageBox.Show(":P");
		}

		private void formPingPong_KeyDown(object sender, KeyEventArgs e)
		{
		
			if ((timer1.Enabled) && (e.KeyCode == Keys.Space))
			{
				timer1.Enabled = false;
				var box = MessageBox.Show("Score: " + score + "\nLevel: " + level, "Pause");
				if (box == DialogResult.OK)
				{
					timer1.Enabled = true;
				}
			}
			else if ((e.KeyCode == Keys.Escape))
			{
				timer1.Enabled = false;
				this.Close();
			}
		}
	}
}

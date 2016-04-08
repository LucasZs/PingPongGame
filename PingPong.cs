using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongGame
{
	public partial class formPingPong : Form
	{
		Graphics graphics;
		Rectangle paddle;
		Rectangle ball;

		int xLeftRec = 150;
		int yTopRec = 400;
		int widthRec = 200;
		int heightRec = 20;

		int xLeftBall = 150;
		int yTopBall = 100;
		int widthBall = 50;
		int heightBall = 50;

		static double exBall = new Random().Next(200, 800)/1000.0;
		static double eyBall = Math.Sqrt(1 - exBall* exBall);
		static int vBall = 10;

		static int score = 0;

		public formPingPong()
		{
			InitializeComponent();
		}

		private void DrawPaddle()
		{
			paddle = new Rectangle(xLeftRec, yTopRec, widthRec, heightRec);
			graphics.FillRectangle(Brushes.Cyan, paddle);
			graphics.DrawRectangle(Pens.Red, paddle);
		}

		private void DrawBall()
		{
			ball = new Rectangle(xLeftBall, yTopBall, widthBall, heightBall);
			graphics.FillEllipse(Brushes.Coral, ball);
			graphics.DrawEllipse(Pens.BlueViolet, ball);
		}

		private void DrawGameElements()
		{
			graphics = this.CreateGraphics();
			graphics.Clear(Color.White);
			DrawPaddle();
			DrawBall();
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			this.Text = "PING PONG GAME";
			buttonStart.Visible = false;
			buttonExit.Visible = false;
			DrawGameElements();
			timer1.Enabled = true;
			label1.Visible = true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			xLeftBall += (int) (exBall * vBall);
			yTopBall += (int) (eyBall * vBall);
			if (xLeftBall <= 3 || xLeftBall >= this.Width - widthBall - 20)
			{
				exBall *= -1;
			}
			else if (yTopBall <= 3)
			{
				eyBall *= -1;
			}
			else if ((xLeftBall + widthBall >= xLeftRec) && (xLeftBall <= xLeftRec + widthRec)
				&& (yTopBall <= yTopRec - heightBall) && (yTopBall >= yTopRec - heightBall - 10))
			{
				eyBall *= -1;
			}
			else if (((xLeftBall <= xLeftRec) || (xLeftBall >= xLeftRec + widthRec - widthBall)) && (yTopBall >= yTopRec))
			{
				timer1.Enabled = false;
				MessageBox.Show("You are a looser...", "Looser");
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
				else
					xLeftRec = e.X - widthRec / 2;
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
			MessageBox.Show(":P", "No exit");
		}

		private void formPingPong_FormClosing(object sender, FormClosingEventArgs e)
		{
			var box = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (box == DialogResult.No)
			{
				e.Cancel = true;
				return;
			}
			MessageBox.Show("Then F*CK YOU!", "F*CK YOU!");
		}
	}
}

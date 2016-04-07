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
		int xRec = 150;
		int yRec = 400;
		int widthRec = 200;
		int height = 20;

		int xEll = 150;
		int yEll = 100;

		public formPingPong()
		{
			InitializeComponent();
		}

		private void DrawIt(int xRed, int yRec, int xEll, int yEll)
		{
			this.Text = "PING PONG GAME";
			Graphics g = this.CreateGraphics();
			g.Clear(Color.White);
			Rectangle r = new Rectangle(xRec, yRec, 200, 20);
			Rectangle ball = new Rectangle(xEll, yEll, 80, 80);

			g.FillRectangle(Brushes.Cyan, r);
			g.DrawRectangle(Pens.Red, r);
			g.FillEllipse(Brushes.Coral, ball);
			g.DrawEllipse(Pens.BlueViolet, ball);
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			buttonStart.Hide();
			buttonExit.Hide();
			DrawIt(xRec, yRec, xEll, yEll);
			timer1.Enabled = true;
		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
			var box = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (box == DialogResult.No)
				return;
			MessageBox.Show("Then F*CK YOU!", "F*CK YOU!");
			Close();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			yEll += 10;
			DrawIt(xRec, yRec, xEll, yEll);
		}

		private void FormPingPong_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.X <= widthRec / 2)
				xRec = 0;
			else if (e.X >= this.Width - widthRec / 2)
				xRec = this.Width - widthRec;
			else
				xRec = e.X - widthRec/2;
		}
	}
}

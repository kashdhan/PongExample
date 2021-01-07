using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongExample
{
    public partial class Form1 : Form
    {
        int playerTurn = 1;
        int paddle1X = 10;
        int paddle1Y = 140;
        int player1Score = 0;

        int paddle2X = 10;
        int paddle2Y = 210;
        int player2Score = 0;

        int paddleWidth = 10;
        int paddleHeight = 60;
        int paddleSpeed = 4;

        int ballX = 295;
        int ballY = 195;
        int ballXSpeed = 6;
        int ballYSpeed = -6;//faster
        int ballWidth = 10;
        int ballHeight = 10;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;//new keys
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;//new keys

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font screenFont = new Font("Consolas", 12);
        Pen borderPen = new Pen(Color.White, 5);//new pen


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1
            if (wDown == true && paddle1Y > 0)//move P1 up
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)//move P1 down
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 0)//move P1 left
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && paddle1X < this.Width - paddleWidth)//move P1 right
            {
                paddle1X += paddleSpeed;
            }
            //move player 2
            if (upArrowDown == true && paddle2Y > 0)//move P2 up
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)//move P2 down
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > 0)//move P2 left
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X < this.Width - paddleWidth)//move P2 right
            {
                paddle2X += paddleSpeed;
            }

            //top and bottom wall collision
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }

            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit



            if (playerTurn == 1 && ballXSpeed < 0 && player1Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                ballX = paddle1X + paddleWidth + 1;
                playerTurn = 2;
            }
            else if (playerTurn == 2 && ballXSpeed < 0 && player2Rec.IntersectsWith(ballRec))
            {
                ballXSpeed *= -1;
                ballX = paddle2X - ballWidth - 1;
                playerTurn = 1;
            }

            if (ballX < 0)
            {
                if (playerTurn == 1)
                {
                    player2Score++;
                }
                else if (playerTurn == 2)
                {
                    player1Score++;
                }

                ballX = 295;
                ballY = 195;

                paddle1Y = 140;
                paddle2Y = 210;
            }
            else if (ballX > 600)//right wall collision 
            {
                ballXSpeed *= -1;//reverse direction
            }

            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(blueBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);


            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 280, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 310, 10);

            if (playerTurn == 1)
            {
                e.Graphics.DrawRectangle(borderPen, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            }
            else if (playerTurn == 2)
            {
                e.Graphics.DrawRectangle(borderPen, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            }


        }
    }
}

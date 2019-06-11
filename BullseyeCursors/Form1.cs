using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Web;

namespace BullseyeCursors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Target variables
        private static Bitmap targetBmp;
        private Graphics targetGfx;

        // x cursor variables
        private static Bitmap xCursorBmp;
        private Graphics xCursorGfx;
        private int xCursorCoordinate = 0, xTemp = -10;

        // y cursor variables
        private static Bitmap yCursorBmp;
        private Graphics yCursorGfx;
        private int yCursorCoordinate = 0, yTemp = -10;

        /// <summary>
        /// This method sets up the UI and visual elements of the application.
        /// Whenever the form is loaded, all the visual elements will be displayed.
        /// Also, the cursor on x is moving when the application is launched.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            SetUpLabels();
            DrawTarget();
            DrawXCursor();
            DrawYCursor();

            // Gets the X cursor moving.
            xCursorTimer.Interval = 1;
            xCursorTimer.Enabled = true;
            xCursorTimer.Start();
        }

        /// <summary>
        /// Deals with the design of the labels.
        /// </summary>
        private void SetUpLabels()
        {
            //Attempts label
            attemptsLabel.Location = new Point(50, 0);
            attemptsLabel.Size = new Size(100, 50);
            attemptsLabel.AutoSize = true;
            attemptsLabel.Text = "Attempts:" + attempts.ToString();

            //Points label
            pointsLabel.Location = new Point(250, 0);
            pointsLabel.Size = new Size(100, 50);
            pointsLabel.AutoSize = true;
            pointsLabel.Text = "Points:" + points.ToString();

            // Instructions label
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Text = "Press Space to shoot.\nPress R to retry.\nPress Escape to exit.";
        }

        /// <summary>
        /// Draws the bullseye target as a bitmap image.
        /// </summary>
        private void DrawTarget()
        {
            
            targetBmp = new Bitmap(400, 400);
            targetGfx = Graphics.FromImage(targetBmp);
            targetGfx.FillEllipse(new SolidBrush(Color.DarkSlateGray), 0, 0, 400, 400);
            targetGfx.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, 300, 300);
            targetGfx.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, 200, 200);
            targetGfx.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, 100, 100);
            targetGfx.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, 20, 20);
            targetPictureBox.Image = targetBmp;
        }

        /// <summary>
        /// Draws the x cursor and the line along which it's moving.
        /// </summary>
        private void DrawXCursor()
        {
            // x cursor graphics
            xCursorBmp = new Bitmap(400, 10);
            xCursorGfx = Graphics.FromImage(xCursorBmp);
            xCursorGfx.Clear(Color.LightGray);
            xCursorPictureBox.Image = xCursorBmp;
        }

        /// <summary>
        /// Draws the y cursors and the line along which it's moving.
        /// </summary>
        private void DrawYCursor()
        {
            // y cursor graphics
            yCursorBmp = new Bitmap(10, 400);
            yCursorGfx = Graphics.FromImage(yCursorBmp);
            yCursorGfx.Clear(Color.LightGray);
            yCursorPictureBox.Image = yCursorBmp;
        }

        

        private void XCursorTimer_Tick(object sender, EventArgs e)
        {
            xCursorGfx.Clear(Color.LightGray);
            XCursorMovement();
            xCursorPictureBox.Image = xCursorBmp;
        }

        /// <summary>
        /// Dictates the way x cursor is moving.
        /// </summary>
        private void XCursorMovement()
        {
            if (xCursorCoordinate >= 0 && xCursorCoordinate <= 390 && xCursorCoordinate - xTemp > 0)
            {
                xCursorGfx.FillRectangle(new SolidBrush(Color.LightSeaGreen), xCursorCoordinate, 0, 10, 10);
                xTemp = xCursorCoordinate;
                xCursorCoordinate += 10;
            }
            else if (xCursorCoordinate == 400)
            {
                xTemp = xCursorCoordinate;
                xCursorCoordinate -= 10;
            }
            else if (xCursorCoordinate >= 0 && xCursorCoordinate <= 390 && xCursorCoordinate - xTemp < 0)
            {
                xCursorGfx.FillRectangle(new SolidBrush(Color.LightSeaGreen), xCursorCoordinate, 0, 10, 10);
                xTemp = xCursorCoordinate;
                xCursorCoordinate -= 10;
            }
            else if (xCursorCoordinate == -10)
            {
                xTemp = xCursorCoordinate;
                xCursorCoordinate += 10;
            }
        }

        private void YCursorTimer_Tick(object sender, EventArgs e)
        {
            yCursorGfx.Clear(Color.LightGray);
            YCursorMovement();
            yCursorPictureBox.Image = yCursorBmp;
        }

        /// <summary>
        /// Dictates the way y cursor is moving.
        /// </summary>
        private void YCursorMovement()
        {
            if (yCursorCoordinate >= 0 && yCursorCoordinate <= 390 && yCursorCoordinate - yTemp > 0)
            {
                yCursorGfx.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, yCursorCoordinate, 10, 10);
                yTemp = yCursorCoordinate;
                yCursorCoordinate += 10;
            }
            else if (yCursorCoordinate == 400)
            {
                yTemp = yCursorCoordinate;
                yCursorCoordinate -= 10;
            }
            else if (yCursorCoordinate >= 0 && yCursorCoordinate <= 390 && yCursorCoordinate - yTemp < 0)
            {
                yCursorGfx.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, yCursorCoordinate, 10, 10);
                yTemp = yCursorCoordinate;
                yCursorCoordinate -= 10;
            }
            else if (yCursorCoordinate == -10)
            {
                yTemp = yCursorCoordinate;
                yCursorCoordinate += 10;
            }
        }

        private int points = 0, attempts = 5;
        private readonly double centerX = 200;
        private readonly double centerY = 200;
        private bool greenFlag = false, yellowFlag = false, redFlag = false, blueFlag = false, blackFlag = false;
        private int xCoordinate = 0, yCoordinate = 0, spaceKeyPressedContor = 0;


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ESCAPE
            if ((Keys)e.KeyValue == Keys.Escape)
            {
                this.Close();
            }
            
            // RETRY
            if ((Keys)e.KeyValue == Keys.R)
            {
                targetGfx.FillEllipse(new SolidBrush(Color.DarkSlateGray), 0, 0, 400, 400);
                targetGfx.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, 300, 300);
                targetGfx.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, 200, 200);
                targetGfx.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, 100, 100);
                targetGfx.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, 20, 20);

                targetPictureBox.Image = targetBmp;

                points = 0;
                pointsLabel.Text = "Points:" + points.ToString();

                attempts = 5;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                spaceKeyPressedContor = 0;
                xCursorTimer.Enabled = true;
                xCursorTimer.Start();
                xCursorCoordinate = 0;
                xTemp = -1;

                yCursorCoordinate = 0;
                yTemp = -1;
            }

            // SPACE
            if((Keys)e.KeyValue == Keys.Space)
            {
                ++spaceKeyPressedContor;

                if(attempts > 0 && spaceKeyPressedContor == 1)
                {
                    xCursorTimer.Stop();
                    xCursorTimer.Enabled = false;
                    if (xCursorCoordinate - xTemp >= 0)
                        xCoordinate = xCursorCoordinate - 7;
                    else if (xCursorCoordinate - xTemp < 0)
                        xCoordinate = xCursorCoordinate + 14;
                    xCursorPictureBox.Image = xCursorBmp;

                    yCursorTimer.Interval = 1;
                    yCursorTimer.Enabled = true;
                    yCursorTimer.Start(); 
                }

                if (attempts > 0 && spaceKeyPressedContor == 2)
                {
                    yCursorTimer.Stop();
                    yCursorTimer.Enabled = false;
                    if (yCursorCoordinate - yTemp >= 0)
                        yCoordinate = yCursorCoordinate - 7;
                    else if (yCursorCoordinate - yTemp < 0)
                        yCoordinate = yCursorCoordinate + 14;
                    yCursorPictureBox.Image = yCursorBmp;
                    
                    timer.Enabled = true;
                    timer.Start();
                    timer.Interval = (750);

                    // Green area 100p
                    if (Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) < 10)
                    {
                        pointsLabel.Text = "Points:" + points.ToString() + "+100";
                        attemptsLabel.Text = "Attempts:" + attempts.ToString() + "-1";

                        greenFlag = true;
                        DrawTargetHole();
                    }

                    // Yellow area 50p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) > 10) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) < 50))
                    {
                        pointsLabel.Text = "Points:" + points.ToString() + "+50";
                        attemptsLabel.Text = "Attempts:" + attempts.ToString() + "-1";

                        yellowFlag = true;
                        DrawTargetHole();
                    }

                    // Red area 25p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) > 50) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) < 100))
                    {
                        pointsLabel.Text = "Points:" + points.ToString() + "+25";
                        attemptsLabel.Text = "Attempts:" + attempts.ToString() + "-1";

                        redFlag = true;
                        DrawTargetHole();
                    }

                    // Blue area 10p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) > 100) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) < 150))
                    {
                        pointsLabel.Text = "Points:" + points.ToString() + "+10";
                        attemptsLabel.Text = "Attempts:" + attempts.ToString() + "-1";

                        blueFlag = true;
                        DrawTargetHole();
                    }

                    // Black area 0p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) > 150) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - centerX, 2f) + Math.Pow(yCoordinate - centerY, 2f)) < 200))
                    {
                        pointsLabel.Text = "Points:" + points.ToString() + "+0";
                        attemptsLabel.Text = "Attempts:" + attempts.ToString() + "-1";

                        blackFlag = true;
                        DrawTargetHole();
                    }

                    spaceKeyPressedContor = 0;
                }
            }
        }

        /// <summary>
        /// Draws a hole at the of (x, y) coordinates.
        /// </summary>
        private void DrawTargetHole()
        {
            targetGfx.FillEllipse(new SolidBrush(Color.White), xCoordinate - 4, yCoordinate - 4, 8, 8);
            targetPictureBox.Image = targetBmp;
        }

        /// <summary>
        /// Makes label display dynamic.
        /// Resets cursors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (greenFlag == true)
            {
                points += 100;
                pointsLabel.Text = "Points:" + points.ToString();
                --attempts;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                greenFlag = false;
            }

            if (yellowFlag == true)
            {
                points += 50;
                pointsLabel.Text = "Points:" + points.ToString();
                --attempts;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                yellowFlag = false;
            }

            if (redFlag == true)
            {
                points += 25;
                pointsLabel.Text = "Points:" + points.ToString();
                --attempts;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                redFlag = false;
            }

            if (blueFlag == true)
            {
                points += 10;
                pointsLabel.Text = "Points:" + points.ToString();
                --attempts;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                blueFlag = false;
            }

            if (blackFlag == true)
            {
                points += 0;
                pointsLabel.Text = "Points:" + points.ToString();
                --attempts;
                attemptsLabel.Text = "Attempts:" + attempts.ToString();

                blackFlag = false;
            }
            timer.Stop();
            timer.Enabled = false;


            if(attempts > 0)
            {
                spaceKeyPressedContor = 0;
                xCursorTimer.Enabled = true;
                xCursorTimer.Start();
                xCursorCoordinate = 0;
                xTemp = -1;

                yCursorCoordinate = 0;
                yTemp = -1;
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Target variables
        private static Bitmap _targetBitmap;
        private Graphics targetGraphics;

        // x cursor variables
        private static Bitmap _xCursorBitmap;
        private Graphics xCursorGraphics;
        private int xCursorCoordinate, xTemp = -10;

        // y cursor variables
        private static Bitmap _yCursorBitmap;
        private Graphics yCursorGraphics;
        private int yCursorCoordinate, yTemp = -10;

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
            attemptsLabel.Text = StringResources.GetAttemptsText(attempts);

            //Points label
            pointsLabel.Location = new Point(250, 0);
            pointsLabel.Size = new Size(100, 50);
            pointsLabel.AutoSize = true;
            pointsLabel.Text = StringResources.GetPointsText(points);

            // Instructions label
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Text = StringResources.GetInstructions();
        }

        /// <summary>
        /// Draws the bullseye target as a bitmap image.
        /// </summary>
        private void DrawTarget()
        {
            
            _targetBitmap = new Bitmap(400, 400);
            targetGraphics = Graphics.FromImage(_targetBitmap);
            targetGraphics.FillEllipse(new SolidBrush(Color.DarkSlateGray), 0, 0, 400, 400);
            targetGraphics.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, 300, 300);
            targetGraphics.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, 200, 200);
            targetGraphics.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, 100, 100);
            targetGraphics.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, 20, 20);
            targetPictureBox.Image = _targetBitmap;
        }

        /// <summary>
        /// Draws the x cursor and the line along which it's moving.
        /// </summary>
        private void DrawXCursor()
        {
            // x cursor graphics
            _xCursorBitmap = new Bitmap(400, 10);
            xCursorGraphics = Graphics.FromImage(_xCursorBitmap);
            xCursorGraphics.Clear(Color.LightGray);
            xCursorPictureBox.Image = _xCursorBitmap;
        }

        /// <summary>
        /// Draws the y cursors and the line along which it's moving.
        /// </summary>
        private void DrawYCursor()
        {
            // y cursor graphics
            _yCursorBitmap = new Bitmap(10, 400);
            yCursorGraphics = Graphics.FromImage(_yCursorBitmap);
            yCursorGraphics.Clear(Color.LightGray);
            yCursorPictureBox.Image = _yCursorBitmap;
        }

        

        private void XCursorTimer_Tick(object sender, EventArgs e)
        {
            xCursorGraphics.Clear(Color.LightGray);
            XCursorMovement();
            xCursorPictureBox.Image = _xCursorBitmap;
        }

        /// <summary>
        /// Dictates the way x cursor is moving.
        /// </summary>
        private void XCursorMovement()
        {
            if (xCursorCoordinate >= 0 && xCursorCoordinate <= 390 && xCursorCoordinate - xTemp > 0)
            {
                xCursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), xCursorCoordinate, 0, 10, 10);
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
                xCursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), xCursorCoordinate, 0, 10, 10);
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
            yCursorGraphics.Clear(Color.LightGray);
            YCursorMovement();
            yCursorPictureBox.Image = _yCursorBitmap;
        }

        /// <summary>
        /// Dictates the way y cursor is moving.
        /// </summary>
        private void YCursorMovement()
        {
            if (yCursorCoordinate >= 0 && yCursorCoordinate <= 390 && yCursorCoordinate - yTemp > 0)
            {
                yCursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, yCursorCoordinate, 10, 10);
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
                yCursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, yCursorCoordinate, 10, 10);
                yTemp = yCursorCoordinate;
                yCursorCoordinate -= 10;
            }
            else if (yCursorCoordinate == -10)
            {
                yTemp = yCursorCoordinate;
                yCursorCoordinate += 10;
            }
        }

        private int points;
        private int attempts = 5;
            
        private const double CenterX = 200;
        private const double CenterY = 200;
        
        private bool greenFlag;
        private bool yellowFlag;
        private bool redFlag;
        private bool blueFlag;
        private bool blackFlag;

        private int xCoordinate;
        private int yCoordinate;
        private int spaceKeyPressedCounter;


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
                targetGraphics.FillEllipse(new SolidBrush(Color.DarkSlateGray), 0, 0, 400, 400);
                targetGraphics.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, 300, 300);
                targetGraphics.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, 200, 200);
                targetGraphics.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, 100, 100);
                targetGraphics.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, 20, 20);

                targetPictureBox.Image = _targetBitmap;

                points = 0;
                pointsLabel.Text = StringResources.GetPointsText(points);

                attempts = 5;
                attemptsLabel.Text = StringResources.GetAttemptsText(attempts);

                spaceKeyPressedCounter = 0;
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
                ++spaceKeyPressedCounter;

                if(attempts > 0 && spaceKeyPressedCounter == 1)
                {
                    xCursorTimer.Stop();
                    xCursorTimer.Enabled = false;
                    if (xCursorCoordinate - xTemp >= 0)
                        xCoordinate = xCursorCoordinate - 7;
                    else if (xCursorCoordinate - xTemp < 0)
                        xCoordinate = xCursorCoordinate + 14;
                    xCursorPictureBox.Image = _xCursorBitmap;

                    yCursorTimer.Interval = 1;
                    yCursorTimer.Enabled = true;
                    yCursorTimer.Start(); 
                }

                if (attempts > 0 && spaceKeyPressedCounter == 2)
                {
                    yCursorTimer.Stop();
                    yCursorTimer.Enabled = false;
                    if (yCursorCoordinate - yTemp >= 0)
                        yCoordinate = yCursorCoordinate - 7;
                    else if (yCursorCoordinate - yTemp < 0)
                        yCoordinate = yCursorCoordinate + 14;
                    yCursorPictureBox.Image = _yCursorBitmap;
                    
                    timer.Enabled = true;
                    timer.Start();
                    timer.Interval = (750);

                    var pointsToAdd = 0;
                    // Green area 100p
                    if (Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) < 10)
                    {
                        pointsToAdd = 100;
                        greenFlag = true;
                        DrawTargetHole();
                    }

                    // Yellow area 50p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) > 10) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) < 50))
                    {
                        pointsToAdd = 50;
                        yellowFlag = true;
                        DrawTargetHole();
                    }

                    // Red area 25p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) > 50) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) < 100))
                    {
                        pointsToAdd = 25;
                        redFlag = true;
                        DrawTargetHole();
                    }

                    // Blue area 10p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) > 100) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) < 150))
                    {
                        pointsToAdd = 10;
                        blueFlag = true;
                        DrawTargetHole();
                    }

                    // Black area 0p
                    if ((Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) > 150) &&
                        (Math.Sqrt(Math.Pow(xCoordinate - CenterX, 2f) + Math.Pow(yCoordinate - CenterY, 2f)) < 200))
                    {
                        blackFlag = true;
                        DrawTargetHole();
                    }
                    
                    pointsLabel.Text = StringResources.GetPointsWithPointsToAddText(points, pointsToAdd);
                    attemptsLabel.Text = StringResources.GetAttemptsWithMinusOneText(attempts);
                    spaceKeyPressedCounter = 0;
                }
            }
        }

        /// <summary>
        /// Draws a hole at the of (x, y) coordinates.
        /// </summary>
        private void DrawTargetHole()
        {
            targetGraphics.FillEllipse(new SolidBrush(Color.White), xCoordinate - 4, yCoordinate - 4, 8, 8);
            targetPictureBox.Image = _targetBitmap;
        }

        /// <summary>
        /// Makes label display dynamic.
        /// Resets cursors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (greenFlag)
            {
                points += 100;
                greenFlag = false;
            }

            if (yellowFlag)
            {
                points += 50;
                yellowFlag = false;
            }

            if (redFlag)
            {
                points += 25;
                redFlag = false;
            }

            if (blueFlag)
            {
                points += 10;
                blueFlag = false;
            }

            if (blackFlag)
            {
                points += 0;
                blackFlag = false;
            }
            
            --attempts;
            pointsLabel.Text = StringResources.GetPointsText(points);
            attemptsLabel.Text = StringResources.GetAttemptsText(attempts);
            
            timer.Stop();
            timer.Enabled = false;


            if(attempts > 0)
            {
                spaceKeyPressedCounter = 0;
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

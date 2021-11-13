using System;
using System.Windows.Forms;
using BullseyeCursors.Models;
using Cursor = BullseyeCursors.Models.Cursor;

namespace BullseyeCursors
{
    public partial class BullseyeForm : Form
    {
        private Target target;
        private Cursor xCursor;
        private Cursor yCursor;
        private AttemptsManager attemptsManager;
        private PointsManager pointsManager;
        
        private TimerWrapper xTimer;
        private TimerWrapper yTimer;
        
        public BullseyeForm()
        {
            InitializeComponent();
        }

        private const double CenterX = 200;
        private const double CenterY = 200;
        
        private bool greenFlag;
        private bool yellowFlag;
        private bool redFlag;
        private bool blueFlag;

        private int xCoordinate;
        private int yCoordinate;
        private int spaceKeyPressedCounter;

        /// <summary>
        /// This method sets up the UI and visual elements of the application.
        /// Whenever the form is loaded, all the visual elements will be displayed.
        /// Also, the cursor on x is moving when the application is launched.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: move TimerWrapper inside Cursor

            target = new Target();
            xCursor = new Cursor(400, 10);
            yCursor = new Cursor(10, 400);
            attemptsManager = new AttemptsManager(5, attemptsLabel);
            pointsManager = new PointsManager(pointsLabel);
            
            InitializeTimers();
            InitializeInstructionsLabel();
            DrawNewImages();
            xTimer.Start();
        }

        private void XCursorTimer_Tick(object sender, EventArgs e)
        {
            xCursorPictureBox.Image = xCursor.DrawOnTickAndUpdateCoordinateValue();
        }

        private void YCursorTimer_Tick(object sender, EventArgs e)
        {
            yCursorPictureBox.Image = yCursor.DrawOnTickAndUpdateCoordinateValue();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Escape)
            {
                HandleClose();
            }
            
            if ((Keys)e.KeyValue == Keys.R)
            {
                HandleRetry();
            }

            // SPACE
            if((Keys)e.KeyValue == Keys.Space)
            {
                ++spaceKeyPressedCounter;

                if(attemptsManager.RemainingNoOfAttempts > 0 && spaceKeyPressedCounter == 1)
                {
                    xTimer.Stop();
                    
                    if (xCursor.Coordinate - xCursor.PreviousCoordinate >= 0)
                    {
                        xCoordinate = xCursor.Coordinate - 7;
                    }
                    else if (xCursor.Coordinate - xCursor.PreviousCoordinate < 0)
                    {
                        xCoordinate = xCursor.Coordinate + 14;
                    }
                    xCursorPictureBox.Image = xCursor.Bitmap;

                    yTimer.Start();
                }

                if (attemptsManager.RemainingNoOfAttempts > 0 && spaceKeyPressedCounter == 2)
                {
                    yTimer.Stop();
                    if (yCursor.Coordinate - yCursor.PreviousCoordinate >= 0)
                        yCoordinate = yCursor.Coordinate - 7;
                    else if (yCursor.Coordinate - yCursor.PreviousCoordinate < 0)
                        yCoordinate = yCursor.Coordinate + 14;
                    yCursorPictureBox.Image = yCursor.Bitmap;
                    
                    timer.Enabled = true;
                    timer.Start();
                    timer.Interval = (750);

                    var pointsToAdd = 0;
                    if (target.IsGreenArea(xCoordinate, yCoordinate))
                    {
                        pointsToAdd = 100;
                        greenFlag = true;
                    }
                    else if (target.IsYellowArea(xCoordinate, yCoordinate))
                    {
                        pointsToAdd = 50;
                        yellowFlag = true;
                    }
                    else if (target.IsRedArea(xCoordinate, yCoordinate))
                    {
                        pointsToAdd = 25;
                        redFlag = true;
                    }
                    else if (target.IsBlueArea(xCoordinate, yCoordinate))
                    {
                        pointsToAdd = 10;
                        blueFlag = true;
                    }
                    
                    DrawHoleInTarget();

                    attemptsManager.DisplayAttemptsMinusOneText();
                    pointsManager.DisplayPointsWithAmountToAdd(pointsToAdd);
                    spaceKeyPressedCounter = 0;
                }
            }
        }

        private void HandleClose() => this.Close();

        private void HandleRetry()
        {
            xTimer.Stop();
            yTimer.Stop();
            ResetValuesAndLabelsForPointsAndAttempts();
            DrawNewImages();
            spaceKeyPressedCounter = 0;
            xTimer.Start();
        }
        
        private void ResetValuesAndLabelsForPointsAndAttempts()
        {
            attemptsManager.Reset();
            pointsManager.Reset();
        }

        /// <summary>
        /// Draws a hole at the of (x, y) coordinates.
        /// </summary>
        private void DrawHoleInTarget()
        {
            targetPictureBox.Image = target.DrawHoleAt(xCoordinate, yCoordinate);
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
                pointsManager.AddPoints(100);
                greenFlag = false;
            }
            else if (yellowFlag)
            {
                pointsManager.AddPoints(50);
                yellowFlag = false;
            }
            else if (redFlag)
            {
                pointsManager.AddPoints(25);
                redFlag = false;
            }
            else if (blueFlag)
            {
                pointsManager.AddPoints(10);
                blueFlag = false;
            }
            else
            {
                pointsManager.AddPoints(0);
            }
            
            attemptsManager.Decrement();

            timer.Stop();
            timer.Enabled = false;


            if(attemptsManager.RemainingNoOfAttempts > 0)
            {
                spaceKeyPressedCounter = 0;
                DrawNewCursors();
                xTimer.Start();
            }
        }

        private void InitializeTimers()
        {
            xTimer = new TimerWrapper(xCursorTimer);
            yTimer = new TimerWrapper(yCursorTimer);
        }

        private void InitializeInstructionsLabel()
        {
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Text = StringResources.GetInstructions();
        }

        private void DrawNewImages()
        {
            DrawNewTarget();
            DrawNewCursors();
        }

        private void DrawNewTarget()
        {
            targetPictureBox.Image = target.DrawNew();
        }

        private void DrawNewCursors()
        {
            xCursorPictureBox.Image = xCursor.DrawNew();
            yCursorPictureBox.Image = yCursor.DrawNew();
        }
    }
}

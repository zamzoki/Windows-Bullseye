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

        public BullseyeForm()
        {
            InitializeComponent();
        }

        private bool greenFlag;
        private bool yellowFlag;
        private bool redFlag;
        private bool blueFlag;

        private int xHitCoordinate;
        private int yHitCoordinate;
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
            target = new Target(targetPictureBox);
            xCursor = new Cursor(400, 10, xCursorPictureBox, xCursorTimer);
            yCursor = new Cursor(10, 400, yCursorPictureBox, yCursorTimer);
            attemptsManager = new AttemptsManager(5, attemptsLabel);
            pointsManager = new PointsManager(pointsLabel);
            
            InitializeInstructionsLabel();
            DrawNewImages();
            xCursor.StartTimer();
        }

        private void XCursorTimer_Tick(object sender, EventArgs e)
        {
            xCursor.DrawOnTickAndUpdateCoordinateValue();
        }

        private void YCursorTimer_Tick(object sender, EventArgs e)
        {
            yCursor.DrawOnTickAndUpdateCoordinateValue();
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
                    xCursor.StopTimer();
                    
                    // TODO investigate these adjustments
                    if (xCursor.Coordinate - xCursor.PreviousCoordinate >= 0)
                    {
                        xHitCoordinate = xCursor.Coordinate - 7;
                    }
                    else if (xCursor.Coordinate - xCursor.PreviousCoordinate < 0)
                    {
                        xHitCoordinate = xCursor.Coordinate + 14;
                    }

                    yCursor.StartTimer();
                }

                if (attemptsManager.RemainingNoOfAttempts > 0 && spaceKeyPressedCounter == 2)
                {
                    // TODO investigate these adjustments
                    yCursor.StopTimer();
                    if (yCursor.Coordinate - yCursor.PreviousCoordinate >= 0)
                        yHitCoordinate = yCursor.Coordinate - 7;
                    else if (yCursor.Coordinate - yCursor.PreviousCoordinate < 0)
                        yHitCoordinate = yCursor.Coordinate + 14;

                    timer.Enabled = true;
                    timer.Start();
                    timer.Interval = (750);

                    var pointsToAdd = 0;
                    if (target.IsGreenArea(xHitCoordinate, yHitCoordinate))
                    {
                        pointsToAdd = 100;
                        greenFlag = true;
                    }
                    else if (target.IsYellowArea(xHitCoordinate, yHitCoordinate))
                    {
                        pointsToAdd = 50;
                        yellowFlag = true;
                    }
                    else if (target.IsRedArea(xHitCoordinate, yHitCoordinate))
                    {
                        pointsToAdd = 25;
                        redFlag = true;
                    }
                    else if (target.IsBlueArea(xHitCoordinate, yHitCoordinate))
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
            StopAllTimers();
            ResetValuesAndLabelsForPointsAndAttempts();
            DrawNewImages();
            spaceKeyPressedCounter = 0;
            xCursor.StartTimer();
        }

        private void StopAllTimers()
        {
            xCursor.StopTimer();
            yCursor.StopTimer();
            timer.Stop();
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
            target.DrawHoleAt(xHitCoordinate, yHitCoordinate);
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
                xCursor.StartTimer();
            }
        }

        private void InitializeInstructionsLabel()
        {
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Text = StringResources.GetInstructions();
        }

        private void DrawNewImages()
        {
            target.DrawNew();
            DrawNewCursors();
        }

        private void DrawNewCursors()
        {
            xCursor.DrawNew();
            yCursor.DrawNew();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BullseyeCursors.Models;
using Cursor = BullseyeCursors.Models.Cursor;

namespace BullseyeCursors
{
    public partial class BullseyeForm : Form
    {
        private Dictionary<TargetAreas, int> targetAreaPointsDictionary = new Dictionary<TargetAreas, int>
        {
            {TargetAreas.Green, 100},
            {TargetAreas.Yellow, 50},
            {TargetAreas.Red, 25},
            {TargetAreas.Blue, 10},
            {TargetAreas.Gray, 0}
        };
        private Target target;
        private Cursor xCursor;
        private Cursor yCursor;
        private AttemptsManager attemptsManager;
        private PointsManager pointsManager;
        private TargetAreas hitArea;
         
        public BullseyeForm()
        {
            InitializeComponent();
        }

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
            // TODO create parent Cursor and children: HorizontalCursor and VerticalCursor
            // TODO create Theme file with all colors
            // TODO investigate if 'timer' is actually required

            target = new Target(targetPictureBox);
            xCursor = new Cursor(400, 10, xCursorPictureBox, xCursorTimer);
            yCursor = new Cursor(10, 400, yCursorPictureBox, yCursorTimer);
            attemptsManager = new AttemptsManager(attemptsLabel);
            pointsManager = new PointsManager(pointsLabel);
            
            InitializeInstructionsLabel();
            DrawNewImages();
            xCursor.StartMoving();
        }

        private void XCursorTimer_Tick(object sender, EventArgs e)
        {
            xCursor.UpdateCoordinateAndDraw();
        }

        private void YCursorTimer_Tick(object sender, EventArgs e)
        {
            yCursor.UpdateCoordinateAndDraw();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Escape)
            {
                HandleClose();
            }
            
            if ((Keys) e.KeyValue == Keys.R)
            {
                HandleRetry();
            }
            
            if ((Keys) e.KeyValue != Keys.Space || attemptsManager.RemainingNoOfAttempts <= 0)
            {
                return;
            }

            ++spaceKeyPressedCounter;
            switch (spaceKeyPressedCounter)
            {
                case 1:
                    HandleFirstSpaceBar();
                    break;
                case 2:
                    HandleSecondSpaceBar();
                    break;
            }
        }

        private void HandleFirstSpaceBar()
        {
            xCursor.StopMoving();
            xHitCoordinate = xCursor.Coordinate;
            yCursor.StartMoving();
        }

        private void HandleSecondSpaceBar()
        {
            yCursor.StopMoving();
            yHitCoordinate = yCursor.Coordinate;

            target.DrawHoleAt(xHitCoordinate, yHitCoordinate);
            hitArea = Target.ComputeHitArea(xHitCoordinate, yHitCoordinate);
            
            attemptsManager.DisplayAttemptsMinusOneText();
            pointsManager.DisplayPointsWithAmountToAdd(targetAreaPointsDictionary[hitArea]);

            timer.Enabled = true;
            timer.Start();
            timer.Interval = 750;
        }

        private void HandleClose() => this.Close();

        private void HandleRetry()
        {
            StopAllTimers();
            ResetValuesAndLabelsForPointsAndAttempts();
            DrawNewImages();
            spaceKeyPressedCounter = 0;
            xCursor.StartMoving();
        }

        private void StopAllTimers()
        {
            xCursor.StopMoving();
            yCursor.StopMoving();
            timer.Stop();
        }
        
        private void ResetValuesAndLabelsForPointsAndAttempts()
        {
            attemptsManager.Reset();
            pointsManager.Reset();
        }

        /// <summary>
        /// Makes label display dynamic.
        /// Resets cursors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Enabled = false;
            
            pointsManager.AddPoints(targetAreaPointsDictionary[hitArea]);
            attemptsManager.Decrement();
            if (attemptsManager.RemainingNoOfAttempts == 0)
            {
                return;
            }

            spaceKeyPressedCounter = 0;
            DrawNewCursors();
            xCursor.StartMoving();
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

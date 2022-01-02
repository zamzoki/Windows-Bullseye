using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BullseyeCursors.Models;
using Cursor = BullseyeCursors.Models.Cursor;

namespace BullseyeCursors
{
    public partial class BullseyeForm : Form
    {
        private readonly Dictionary<TargetAreas, int> targetAreaPointsDictionary = new Dictionary<TargetAreas, int>
        {
            {TargetAreas.Green, 100},
            {TargetAreas.Yellow, 50},
            {TargetAreas.Red, 25},
            {TargetAreas.Blue, 10},
            {TargetAreas.Gray, 0}
        };
        
        private int xHitCoordinate;
        private int yHitCoordinate;
        private int spaceKeyPressedCounter;
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
        
        private void BullseyeForm_Load(object sender, EventArgs e)
        {
            // TODO add XML documentation to public methods, at least
            
            target = new Target(targetPictureBox);
            xCursor = new Cursor(400, 10, xCursorPictureBox, xCursorTimer);
            yCursor = new Cursor(10, 400, yCursorPictureBox, yCursorTimer);
            attemptsManager = new AttemptsManager(attemptsLabel);
            pointsManager = new PointsManager(pointsLabel);
            
            InitializeInstructionsLabel();
            DrawNewForm();
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

        private void BullseyeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.Escape:
                    HandleClose();
                    break;
                case (int)Keys.R:
                    HandleRetry();
                    break;
                case (int)Keys.Space when attemptsManager.RemainingNoOfAttempts > 0:
                    HandleSpaceBar();
                    break;
                default:
                    return;
            }
        }
        
        private void HandleClose() => this.Close();

        private void HandleRetry()
        {
            StopAllTimers();
            ResetValuesAndLabelsForPointsAndAttempts();
            DrawNewForm();
            spaceKeyPressedCounter = 0;
            xCursor.StartMoving();
        }

        private void HandleSpaceBar()
        {
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

            StartDynamicDisplayTimer();
        }
        
        private void DynamicDisplayTimer_Tick(object sender, EventArgs e)
        {
            StopDynamicDisplayTimer();
            
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

        private void StopAllTimers()
        {
            xCursor.StopMoving();
            yCursor.StopMoving();
            dynamicDisplayTimer.Stop();
        }
        
        private void ResetValuesAndLabelsForPointsAndAttempts()
        {
            attemptsManager.Reset();
            pointsManager.Reset();
        }

        private void InitializeInstructionsLabel()
        {
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Text = StringResources.GetInstructions();
        }

        private void DrawNewForm()
        {
            target.DrawNew();
            DrawNewCursors();
        }

        private void DrawNewCursors()
        {
            xCursor.DrawNew();
            yCursor.DrawNew();
        }

        private void StartDynamicDisplayTimer()
        {
            dynamicDisplayTimer.Enabled = true;
            dynamicDisplayTimer.Interval = 750;
            dynamicDisplayTimer.Start();
            
        }

        private void StopDynamicDisplayTimer()
        {
            dynamicDisplayTimer.Stop();
            dynamicDisplayTimer.Enabled = false;
        }
    }
}

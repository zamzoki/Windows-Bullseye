using System;
using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class Cursor
    {
        private const int CursorThickness = 10;
        private const int TickStep = 10;
        
        private readonly int width;
        private readonly int height;
        private readonly bool isHorizontal;
        private readonly Graphics cursorGraphics;
        private readonly PictureBox pictureBox;
        private readonly Timer timer;

        public Cursor(int widthArg, int heightArg, PictureBox pictureBoxArg, Timer timerArg)
        {
            EnsureDimensionsAreValid(widthArg, heightArg);
            
            this.width = widthArg;
            this.height = heightArg;
            this.isHorizontal = widthArg >= heightArg;
            this.Bitmap = new Bitmap(widthArg, heightArg);
            this.cursorGraphics = Graphics.FromImage(this.Bitmap);
            this.pictureBox = pictureBoxArg;
            this.timer = timerArg;
        }
        
        private int Length => this.isHorizontal ? this.width : this.height;

        private Bitmap Bitmap { get; }

        public int Coordinate { get; private set; }

        private int PreviousCoordinate { get; set; } = -10;

        public void DrawNew()
        {
            this.ResetCoordinate();
            this.DrawElements();
            this.UpdateImage();
        }

        public void UpdateCoordinateAndDraw()
        {
            this.HandleCoordinateIncrement();
            this.DrawElements();
            this.UpdateImage();
        }

        public void ResetCoordinate()
        {
            this.Coordinate = 0;
            this.PreviousCoordinate = -1;
        }

        public void StartMoving()
        {
            this.timer.Interval = 1;
            this.timer.Enabled = true;
            this.timer.Start();
        }

        public void StopMoving()
        {
            this.timer.Stop();
            this.timer.Enabled = false;
        }

        private void DrawElements()
        {
            this.DrawTrack();
            this.DrawCursor();
        }

        private void DrawTrack()
        {
            this.cursorGraphics.Clear(Color.LightGray);
        }

        private void DrawCursor()
        {
            if (this.isHorizontal)
            {
                this.DrawCursorForHorizontalTrack();
            }
            else
            {
                this.DrawCursorForVerticalTrack();
            }
        }

        private void DrawCursorForHorizontalTrack()
        {
            this.cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), this.Coordinate, 0, CursorThickness, this.height);
        }

        private void DrawCursorForVerticalTrack()
        {
            this.cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, this.Coordinate, this.width, CursorThickness);
        }

        private void HandleCoordinateIncrement()
        {
            var delta = this.Coordinate - this.PreviousCoordinate; 
            if (delta == 0)
            {
                throw new Exception($"Displacement cannot be zero.");
            }
            
            this.PreviousCoordinate = this.Coordinate;
            if (this.IsAtStart() || this.IsBetweenStartAndEnd() && IsMovingToTheRight(delta))
            {
                this.Coordinate += TickStep;
            }
            else if (this.IsAtEnd() || this.IsBetweenStartAndEnd() && IsMovingToTheLeft(delta))
            {
                this.Coordinate -= TickStep;
            }
        }

        private bool IsAtStart() => this.Coordinate == 0;

        private bool IsAtEnd() => this.Coordinate == this.Length - TickStep;

        private bool IsBetweenStartAndEnd() => this.Coordinate > 0 && this.Coordinate < this.Length - TickStep;

        private static bool IsMovingToTheRight(int deltaArg) => deltaArg > 0;

        private static bool IsMovingToTheLeft(int deltaArg) => deltaArg < 0;

        private static void EnsureDimensionsAreValid(int widthArg, int heightArg)
        {
            EnsureDimensionIsValid(widthArg, nameof(widthArg));
            EnsureDimensionIsValid(heightArg, nameof(heightArg));
        }

        private static void EnsureDimensionIsValid(int dimension, string nameOfDimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException($"\"{nameOfDimension}\" must be an integer greater than 0.");
            }
        }

        private void UpdateImage() => this.pictureBox.Image = this.Bitmap;
    }
}
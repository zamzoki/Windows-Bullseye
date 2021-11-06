using System;
using System.Drawing;

namespace BullseyeCursors
{
    public class Cursor
    {
        private const int CursorThickness = 10;
        private const int TickStep = 10;
        
        private readonly int width;
        private readonly int height;
        private readonly bool isHorizontal;
        private readonly Bitmap bitmap;
        private readonly Graphics cursorGraphics;

        private int coordinate;
        private int previousCoordinate = -10;

        public Cursor(int widthArg, int heightArg)
        {
            EnsureDimensionsAreValid(widthArg, heightArg);
            
            this.width = widthArg;
            this.height = heightArg;
            this.isHorizontal = widthArg >= heightArg;
            this.bitmap = new Bitmap(widthArg, heightArg);
            this.cursorGraphics = Graphics.FromImage(bitmap);
        }
        
        private int Length => this.isHorizontal ? this.width : this.height;

        public Bitmap Bitmap => this.bitmap;

        public int Coordinate => this.coordinate;

        public int PreviousCoordinate => this.previousCoordinate;

        public Bitmap DrawNew()
        {
            this.ResetCoordinate();
            this.DrawElements();
            return this.bitmap;
        }

        public Bitmap DrawOnTickAndUpdateCoordinateValue()
        {
            this.DrawElements();
            this.UpdateCoordinateOnTick();
            return this.bitmap;
        }

        public void ResetCoordinate()
        {
            this.coordinate = 0;
            this.previousCoordinate = -1;
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
                this.DrawCursorForHorizontalSlider();
            }
            else
            {
                this.DrawCursorForVerticalSlider();
            }
        }

        private void DrawCursorForHorizontalSlider()
        {
            this.cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), this.coordinate, 0, CursorThickness, this.height);
        }

        private void DrawCursorForVerticalSlider()
        {
            this.cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, this.coordinate, this.width, CursorThickness);
        }

        private void UpdateCoordinateOnTick()
        {
            var delta = this.coordinate - this.previousCoordinate;
            if (delta == 0)
            {
                throw new Exception($"\"{nameof(delta)}\" cannot be zero.");
            }
            
            this.previousCoordinate = this.coordinate;
            if (this.IsAtStart() || this.IsBetweenStartAndEnd() && this.IsSlidingToTheRight(delta))
            {
                this.coordinate += TickStep;
            }
            else if (this.IsAtEnd() || this.IsBetweenStartAndEnd() && this.IsSlidingToTheLeft(delta))
            {
                this.coordinate -= TickStep;
            }

        }

        private bool IsAtStart() => this.coordinate == 0;

        private bool IsAtEnd() => this.coordinate == this.Length - TickStep;

        private bool IsBetweenStartAndEnd() => this.coordinate > 0 && this.coordinate < this.Length - TickStep;

        private bool IsSlidingToTheRight(int delta) => delta > 0;

        private bool IsSlidingToTheLeft(int delta) => delta < 0;

        private void EnsureDimensionsAreValid(int widthArg, int heightArg)
        {
            EnsureDimensionIsValid(widthArg, nameof(widthArg));
            EnsureDimensionIsValid(heightArg, nameof(heightArg));
        }

        private void EnsureDimensionIsValid(int dimension, string nameOfDimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException($"\"{nameOfDimension}\" must be an integer greater than 0.");
            }
        }
    }
}
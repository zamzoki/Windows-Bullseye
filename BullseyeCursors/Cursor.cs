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
        private readonly bool horizontal;
        private readonly Bitmap bitmap;
        private readonly Graphics cursorGraphics;

        private int coordinate;
        private int previousCoordinate = -10;

        public Cursor(int widthArg, int heightArg)
        {
            EnsureDimensionsAreValid(widthArg, heightArg);
            
            this.width = widthArg;
            this.height = heightArg;
            this.horizontal = widthArg >= heightArg;
            this.bitmap = new Bitmap(widthArg, heightArg);
            this.cursorGraphics = Graphics.FromImage(bitmap);
        }
        
        private int Length => this.horizontal ? this.width : this.height;

        public Bitmap Bitmap => bitmap;

        public int Coordinate => coordinate;

        public int PreviousCoordinate
        {
            get => previousCoordinate;
            set => previousCoordinate = value;
        }

        public Bitmap DrawNew()
        {
            cursorGraphics.Clear(Color.LightGray);
            return bitmap;
        }

        public Bitmap DrawOnTickAndUpdateCoordinateValue()
        {
            cursorGraphics.Clear(Color.LightGray);
            HandleDrawingCursor();
            this.UpdateCoordinateOnTick();
            return bitmap;
        }

        public void ResetCoordinate()
        {
            this.coordinate = 0;
        }

        private void HandleDrawingCursor()
        {
            if (horizontal)
            {
                DrawCursorForHorizontalSlider();
            }
            else
            {
                DrawCursorForVerticalSlider();
            }
        }

        private void DrawCursorForHorizontalSlider()
        {
            cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), this.coordinate, 0, CursorThickness, this.height);
        }

        private void DrawCursorForVerticalSlider()
        {
            cursorGraphics.FillRectangle(new SolidBrush(Color.LightSeaGreen), 0, this.coordinate, this.width, CursorThickness);
        }

        private void UpdateCoordinateOnTick()
        {
            var delta = coordinate - previousCoordinate;
            if (delta == 0)
            {
                throw new Exception($"\"{nameof(delta)}\" cannot be zero.");
            }
            
            previousCoordinate = coordinate;
            if (IsAtStart() || IsBetweenStartAndEnd() && IsSlidingToTheRight(delta))
            {
                coordinate += TickStep;
            }
            else if (IsAtEnd() || IsBetweenStartAndEnd() && IsSlidingToTheLeft(delta))
            {
                coordinate -= TickStep;
            }

        }

        private bool IsAtStart() => coordinate == 0;

        private bool IsAtEnd() => coordinate == this.Length - TickStep;

        private bool IsBetweenStartAndEnd() => coordinate > 0 && coordinate < this.Length - TickStep;

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
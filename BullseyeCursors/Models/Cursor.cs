using System;
using System.Drawing;

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

        public Cursor(int widthArg, int heightArg)
        {
            EnsureDimensionsAreValid(widthArg, heightArg);
            
            this.width = widthArg;
            this.height = heightArg;
            this.isHorizontal = widthArg >= heightArg;
            this.Bitmap = new Bitmap(widthArg, heightArg);
            this.cursorGraphics = Graphics.FromImage(Bitmap);
        }
        
        private int Length => this.isHorizontal ? this.width : this.height;

        public Bitmap Bitmap { get; }

        public int Coordinate { get; private set; }

        public int PreviousCoordinate { get; private set; } = -10;

        public Bitmap DrawNew()
        {
            this.ResetCoordinate();
            this.DrawElements();
            return this.Bitmap;
        }

        public Bitmap DrawOnTickAndUpdateCoordinateValue()
        {
            this.DrawElements();
            this.UpdateCoordinateOnTick();
            return this.Bitmap;
        }

        public void ResetCoordinate()
        {
            this.Coordinate = 0;
            this.PreviousCoordinate = -1;
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

        private void UpdateCoordinateOnTick()
        {
            if (this.Coordinate - this.PreviousCoordinate == 0)
            {
                throw new Exception($"Displacement cannot be zero.");
            }
            
            this.PreviousCoordinate = this.Coordinate;
            if (this.IsAtStart() || this.IsBetweenStartAndEnd() && this.IsMovingToTheRight())
            {
                this.Coordinate += TickStep;
            }
            else if (this.IsAtEnd() || this.IsBetweenStartAndEnd() && this.IsMovingToTheLeft())
            {
                this.Coordinate -= TickStep;
            }

        }

        private bool IsAtStart() => this.Coordinate == 0;

        private bool IsAtEnd() => this.Coordinate == this.Length - TickStep;

        private bool IsBetweenStartAndEnd() => this.Coordinate > 0 && this.Coordinate < this.Length - TickStep;

        private bool IsMovingToTheRight() => this.Coordinate - this.PreviousCoordinate > 0;

        private bool IsMovingToTheLeft() => this.Coordinate - this.PreviousCoordinate < 0;

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
    }
}
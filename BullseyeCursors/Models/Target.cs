using System;
using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class Target
    {
        private const int Width = 400;
        private const int Height = 400;
        private const int HoleDiameter = 10;
        private const int GreenAreaDiameter = 20;
        private const int YellowAreaDiameter = 100;
        private const int RedAreaDiameter = 200;
        private const int BlueAreaDiameter = 300;
        
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly PictureBox pictureBox;

        public Target(PictureBox pictureBoxArg)
        {
            this.bitmap = new Bitmap(Width, Height);
            this.graphics = Graphics.FromImage(bitmap);
            this.pictureBox = pictureBoxArg;
        }
        
        private static int CenterX => Width / 2;

        private static int CenterY => Height / 2;

        public void DrawNew()
        {
            this.Initialize();
            this.UpdateImage();
        }

        public void DrawHoleAt(int x, int y)
        {
            this.graphics.FillEllipse(new SolidBrush(Color.White), x, y, HoleDiameter, HoleDiameter);
            this.UpdateImage();
        }

        public static TargetAreas ComputeHitArea(int x, int y)
        {
            if (IsGreenArea(x, y, out var greenArea))
            {
                return greenArea;
            }
            
            if (IsYellowArea(x, y, out var yellowArea))
            {
                return yellowArea;
            }

            if (IsRedArea(x, y, out var redArea))
            {
                return redArea;
            }

            return IsBlueArea(x, y, out var blueArea) 
                ? blueArea : 
                TargetAreas.Gray;
        }

        private static bool IsGreenArea(int x, int y, out TargetAreas area)
        {
            area = TargetAreas.Green;
            return IsInArea(x, y, 0, (double) GreenAreaDiameter / 2);
        }

        private static bool IsYellowArea(int x, int y, out TargetAreas area)
        {
            area = TargetAreas.Yellow;
            return IsInArea(x, y, (double) GreenAreaDiameter / 2, (double) YellowAreaDiameter / 2);
        }

        private static bool IsRedArea(int x, int y, out TargetAreas area)
        {
            area = TargetAreas.Red;
            return IsInArea(x, y, (double) YellowAreaDiameter / 2, (double) RedAreaDiameter / 2);
        }

        private static bool IsBlueArea(int x, int y, out TargetAreas area)
        {
            area = TargetAreas.Blue;
            return IsInArea(x, y, (double) RedAreaDiameter / 2, (double) BlueAreaDiameter / 2);
        }
        
        private static bool IsInArea(int x, int y, double minRadius, double maxRadius)
        {
            var distanceFromTargetCenterToHitPoint = GetRadiusFor(x, y); 
            return distanceFromTargetCenterToHitPoint >= minRadius
                   && distanceFromTargetCenterToHitPoint < maxRadius;
        }

        private void Initialize()
        {
            this.graphics.Clear(Color.DarkSlateGray);
            this.graphics.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, BlueAreaDiameter, BlueAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, RedAreaDiameter, RedAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, YellowAreaDiameter, YellowAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, GreenAreaDiameter, GreenAreaDiameter);
        }

        private static double GetRadiusFor(int x, int y)
            => Math.Sqrt(Square(ComputeHoleCenterOnAxis(x, CenterX)) + Square(ComputeHoleCenterOnAxis(y, CenterY)));

        private static double Square(double value) => Math.Pow(value, 2f);

        private void UpdateImage() => this.pictureBox.Image = this.bitmap;

        private static double ComputeHoleCenterOnAxis(int hitCoordinate, int targetCenter)
            => (hitCoordinate - targetCenter + HoleDiameter / 2);
    }
}
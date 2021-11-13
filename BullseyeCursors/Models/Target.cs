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

        public static bool IsGreenArea(int x, int y)
            => GetRadiusFor(x, y) <= (double) GreenAreaDiameter / 2;

        public static bool IsYellowArea(int x, int y)
            => GetRadiusFor(x, y) > (double) GreenAreaDiameter / 2
               && GetRadiusFor(x, y) <= (double) YellowAreaDiameter / 2;
        
        public static bool IsRedArea(int x, int y)
            => GetRadiusFor(x, y) > (double) YellowAreaDiameter / 2
               && GetRadiusFor(x, y) <= (double) RedAreaDiameter / 2;
        
        public static bool IsBlueArea(int x, int y)
            => GetRadiusFor(x, y) > (double) RedAreaDiameter / 2
               && GetRadiusFor(x, y) <= (double) BlueAreaDiameter / 2;

        private void Initialize()
        {
            this.graphics.Clear(Color.DarkSlateGray);
            this.graphics.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, BlueAreaDiameter, BlueAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, RedAreaDiameter, RedAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, YellowAreaDiameter, YellowAreaDiameter);
            this.graphics.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, GreenAreaDiameter, GreenAreaDiameter);
        }

        private static double GetRadiusFor(int x, int y)
            => Math.Sqrt(Square(x - CenterX) + Square(y - CenterY));

        private static double Square(int value) => Math.Pow(value, 2f);

        private void UpdateImage() => this.pictureBox.Image = this.bitmap;
    }
}
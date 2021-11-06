using System.Drawing;

namespace BullseyeCursors
{
    public class Target
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public Target()
        {
            bitmap = new Bitmap(400, 400);
            graphics = Graphics.FromImage(bitmap);
        }
        
        public Bitmap DrawNew()
        {
            Initialize();
            return bitmap;
        }

        public Bitmap DrawHoleAt(int x, int y)
        {
            graphics.FillEllipse(new SolidBrush(Color.White), x - 4, y - 4, 8, 8);
            return bitmap;
        }

        private void Initialize()
        {
            graphics.FillEllipse(new SolidBrush(Color.DarkSlateGray), 0, 0, 400, 400);
            graphics.FillEllipse(new SolidBrush(Color.DarkBlue), 50, 50, 300, 300);
            graphics.FillEllipse(new SolidBrush(Color.Crimson), 100, 100, 200, 200);
            graphics.FillEllipse(new SolidBrush(Color.Goldenrod), 150, 150, 100, 100);
            graphics.FillEllipse(new SolidBrush(Color.ForestGreen), 190, 190, 20, 20);
        }
    }
}
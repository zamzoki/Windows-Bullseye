using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class PointsManager
    {
        private Label pointsLabel;

        public PointsManager(Label pointsLabelArg)
        {
            InitializePointsLabel(pointsLabelArg);
        }

        private int NoOfPoints { get; set; }

        public void AddPoints(int valueArg)
        {
            this.NoOfPoints += valueArg;
            this.UpdateLabelText();
        }

        public void Reset()
        {
            this.NoOfPoints = 0;
            this.UpdateLabelText();
        }
        
        public void DisplayPointsWithAmountToAdd(int pointsToAddArg) 
            => this.pointsLabel.Text = StringResources.GetPointsWithPointsToAddText(this.NoOfPoints, pointsToAddArg);
        
        private void UpdateLabelText() => this.pointsLabel.Text = StringResources.GetPointsText(this.NoOfPoints);

        private void InitializePointsLabel(Label pointsLabelArg)
        {
            this.pointsLabel = pointsLabelArg;
            this.pointsLabel.Location = new Point(250, 0);
            this.pointsLabel.Size = new Size(100, 50);
            this.pointsLabel.AutoSize = true;
            this.UpdateLabelText();
        }
    }
}
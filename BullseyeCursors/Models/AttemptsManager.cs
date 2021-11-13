using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class AttemptsManager
    {
        private const int InitialNoOfAttempts = 5;
        private Label attemptsLabel;

        public AttemptsManager(Label attemptsLabelArg)
        {
            this.RemainingNoOfAttempts = InitialNoOfAttempts;
            InitializeLabel(attemptsLabelArg);
        }

        public int RemainingNoOfAttempts { get; private set; }

        public void Decrement()
        {
            this.RemainingNoOfAttempts--;
            this.UpdateLabelText();
        }

        public void Reset()
        {
            this.RemainingNoOfAttempts = InitialNoOfAttempts;
            this.UpdateLabelText();
        }

        public void DisplayAttemptsMinusOneText()
            => this.attemptsLabel.Text = StringResources.GetAttemptsWithMinusOneText(this.RemainingNoOfAttempts);
        
        private void UpdateLabelText() => this.attemptsLabel.Text = StringResources.GetAttemptsText(this.RemainingNoOfAttempts);

        private void InitializeLabel(Label attemptsLabelArg)
        {
            this.attemptsLabel = attemptsLabelArg;
            this.attemptsLabel.Location = new Point(50, 0);
            this.attemptsLabel.Size = new Size(100, 50);
            this.attemptsLabel.AutoSize = true;
            this.UpdateLabelText();
        }
    }
}
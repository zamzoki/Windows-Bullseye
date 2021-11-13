using System.Drawing;
using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class AttemptsManager
    {
        private readonly int initialNoOfAttempts;
        private Label attemptsLabel;

        public AttemptsManager(int initialNoOfAttemptsArg, Label attemptsLabelArg)
        {
            this.initialNoOfAttempts = initialNoOfAttemptsArg;
            this.RemainingNoOfAttempts = initialNoOfAttemptsArg;
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
            this.RemainingNoOfAttempts = this.initialNoOfAttempts;
            this.UpdateLabelText();
        }

        public void DisplayAttemptsMinusOneText()
        {
            this.attemptsLabel.Text = StringResources.GetAttemptsWithMinusOneText(this.RemainingNoOfAttempts);
        }
        
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
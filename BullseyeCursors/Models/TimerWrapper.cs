using System.Windows.Forms;

namespace BullseyeCursors.Models
{
    public class TimerWrapper
    {
        private readonly Timer timer;
        
        public TimerWrapper(Timer timerArg)
        {
            this.timer = timerArg;
        }

        public void Start()
        {
            timer.Interval = 1;
            timer.Enabled = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Enabled = false;
        }
    }
}
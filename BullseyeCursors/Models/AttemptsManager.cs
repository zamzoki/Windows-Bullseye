using System.Collections.Generic;
using BullseyeCursors.Interfaces;

namespace BullseyeCursors.Models
{
    public class AttemptsManager : ISubject
    {
        private readonly int initialNoOfAttempts;
        private int noOfAttempts;
        private readonly List<IObserver> observers;

        public AttemptsManager(int initialNoOfAttemptsArg)
        {
            this.initialNoOfAttempts = initialNoOfAttemptsArg;
            this.noOfAttempts = initialNoOfAttemptsArg;
            this.observers = new List<IObserver>();
        }

        public int Count => this.noOfAttempts;

        public void Decrement()
        {
            this.noOfAttempts--;
        }

        public void Reset()
        {
            this.noOfAttempts = this.initialNoOfAttempts;
        }

        public void Attach(IObserver observerArg)
        {
            if (!this.observers.Contains(observerArg))
            {
                this.observers.Add(observerArg);    
            }
        }

        public void Notify()
        {
            this.observers.ForEach(o => o.Update(this));
        }
    }
}
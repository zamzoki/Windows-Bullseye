namespace BullseyeCursors.Interfaces
{
    public interface ISubject
    {
        void Attach(IObserver observerArg);

        void Notify();
    }
}
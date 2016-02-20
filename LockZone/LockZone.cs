using System;
using System.Threading;

public class DisposableLock : IDisposable
{
    public virtual void Enter()
        => Monitor.Enter(this);
    public virtual void Leave()
        => Monitor.Exit(this);

    void IDisposable.Dispose()
        => Leave();
}

public struct LockZone
{
    public DisposableLock BaseLock;

    public LockZone(DisposableLock baseLock)
    {
        BaseLock = baseLock;
    }
    public static LockZone Spawn()
        => new LockZone(new DisposableLock());

    public IDisposable Locking()
    {
        BaseLock.Enter();
        return BaseLock;
    }
}

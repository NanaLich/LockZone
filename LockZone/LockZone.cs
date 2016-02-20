using System;
using System.Threading;

public class LockZone : IDisposable
{
    public virtual void Enter()
        => Monitor.Enter(this);
    public virtual void Leave()
        => Monitor.Exit(this);

    public virtual IDisposable Lock()
    {
        Enter();
        return this;
    }

    void IDisposable.Dispose()
        => Leave();
}

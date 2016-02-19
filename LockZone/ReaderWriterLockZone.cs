using System.Threading;

#if !SupportSilverlight

public abstract class ReaderWriterLockZone : LockZone
{
    protected ReaderWriterLockSlim BaseLock { get; }

    protected ReaderWriterLockZone(ReaderWriterLockSlim rwl)
    {
        BaseLock = rwl;
    }
}

public class ReadLockZone : ReaderWriterLockZone
{
    public ReadLockZone(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterReadLock();

    public override void Leave()
        => BaseLock.ExitReadLock();
}

public class WriteLockZone : ReaderWriterLockZone
{
    public WriteLockZone(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterWriteLock();

    public override void Leave()
        => BaseLock.ExitWriteLock();
}

#endif

using System;
using System.Threading;

#if !SupportSilverlight

public class ReaderLockWrapper : AbstractReaderWriterLockWrapper
{
    public ReaderLockWrapper(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterReadLock();

    public override void Leave()
        => BaseLock.ExitReadLock();
}

public struct ReaderLockZone
{
    public ReaderLockWrapper UnderlayedReaderLock;

    public ReaderLockZone(ReaderLockWrapper readerLock)
    {
        UnderlayedReaderLock = readerLock;
    }
    public ReaderLockZone(ReaderWriterLockSlim rwl)
        : this(new ReaderLockWrapper(rwl))
    { }

    public IDisposable Locking()
    {
        UnderlayedReaderLock.Enter();
        return UnderlayedReaderLock;
    }
}

#endif

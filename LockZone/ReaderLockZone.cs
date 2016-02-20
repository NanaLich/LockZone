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
    public ReaderLockWrapper UnderlaidReaderLock;

    public ReaderLockZone(ReaderLockWrapper readerLock)
    {
        UnderlaidReaderLock = readerLock;
    }
    public ReaderLockZone(ReaderWriterLockSlim rwl)
        : this(new ReaderLockWrapper(rwl))
    { }

    public IDisposable Locking()
    {
        UnderlaidReaderLock.Enter();
        return UnderlaidReaderLock;
    }
}

#endif

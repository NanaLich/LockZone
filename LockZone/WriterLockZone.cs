using System;
using System.Threading;

#if !SupportSilverlight

public class WriterLockWrapper : AbstractReaderWriterLockWrapper
{
    public WriterLockWrapper(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterWriteLock();

    public override void Leave()
        => BaseLock.ExitWriteLock();
}

public struct WriterLockZone
{
    public WriterLockWrapper UnderlayedWriterLock;

    public WriterLockZone(WriterLockWrapper writerLock)
    {
        UnderlayedWriterLock = writerLock;
    }
    public WriterLockZone(ReaderWriterLockSlim rwl)
        : this(new WriterLockWrapper(rwl))
    { }

    public IDisposable Locking()
    {
        UnderlayedWriterLock.Enter();
        return UnderlayedWriterLock;
    }
}

#endif

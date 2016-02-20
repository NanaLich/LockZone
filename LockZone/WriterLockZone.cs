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
    public WriterLockWrapper UnderlaidWriterLock;

    public WriterLockZone(WriterLockWrapper writerLock)
    {
        UnderlaidWriterLock = writerLock;
    }
    public WriterLockZone(ReaderWriterLockSlim rwl)
        : this(new WriterLockWrapper(rwl))
    { }

    public IDisposable Locking()
    {
        UnderlaidWriterLock.Enter();
        return UnderlaidWriterLock;
    }
}

#endif

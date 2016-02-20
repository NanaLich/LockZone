using System;
using System.Threading;

#if !SupportSilverlight

public abstract class AbstractReaderWriterLockWrapper : DisposableLock
{
    internal protected ReaderWriterLockSlim BaseLock { get; }

    protected AbstractReaderWriterLockWrapper(ReaderWriterLockSlim rwl)
    {
        BaseLock = rwl;
    }
}

public struct ReaderWriterLockZone
{
    public ReaderLockWrapper UnderlayedReaderLock;
    public WriterLockWrapper UnderlayedWriterLock;

    public ReaderWriterLockZone(ReaderWriterLockSlim rwl)
    {
        UnderlayedReaderLock = new ReaderLockWrapper(rwl);
        UnderlayedWriterLock = new WriterLockWrapper(rwl);
    }
    public static ReaderWriterLockZone Spawn()
        => new ReaderWriterLockZone(new ReaderWriterLockSlim());

    public IDisposable ReaderLock()
    {
        UnderlayedReaderLock.Enter();
        return UnderlayedReaderLock;
    }
    public IDisposable WriterLock()
    {
        UnderlayedWriterLock.Enter();
        return UnderlayedWriterLock;
    }
}

#endif

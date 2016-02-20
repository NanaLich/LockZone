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
    public ReaderLockWrapper UnderlaidReaderLock;
    public WriterLockWrapper UnderlaidWriterLock;

    public ReaderWriterLockZone(ReaderWriterLockSlim rwl)
    {
        UnderlaidReaderLock = new ReaderLockWrapper(rwl);
        UnderlaidWriterLock = new WriterLockWrapper(rwl);
    }
    public static ReaderWriterLockZone Spawn()
        => new ReaderWriterLockZone(new ReaderWriterLockSlim());

    public IDisposable ReaderLocking()
    {
        UnderlaidReaderLock.Enter();
        return UnderlaidReaderLock;
    }
    public IDisposable WriterLocking()
    {
        UnderlaidWriterLock.Enter();
        return UnderlaidWriterLock;
    }
}

#endif

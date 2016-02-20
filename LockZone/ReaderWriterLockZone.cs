using System;
using System.Threading;

#if !SupportSilverlight

public abstract class DisposableReaderWriterLock : DisposableLock
{
    internal protected ReaderWriterLockSlim BaseLock { get; }

    protected DisposableReaderWriterLock(ReaderWriterLockSlim rwl)
    {
        BaseLock = rwl;
    }
}

public struct ReaderWriterLockZone
{
    public DisposableReaderLock UnderlayedReaderLock;
    public DisposableWriterLock UnderlayedWriterLock;

    public ReaderWriterLockZone(ReaderWriterLockSlim rwl)
    {
        UnderlayedReaderLock = new DisposableReaderLock(rwl);
        UnderlayedWriterLock = new DisposableWriterLock(rwl);
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

public class DisposableReaderLock : DisposableReaderWriterLock
{
    public DisposableReaderLock(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterReadLock();

    public override void Leave()
        => BaseLock.ExitReadLock();
}

public struct ReaderLockZone
{
    public DisposableReaderLock UnderlayedReaderLock;

    public ReaderLockZone(DisposableReaderLock readerLock)
    {
        UnderlayedReaderLock = readerLock;
    }
    public ReaderLockZone(ReaderWriterLockSlim rwl)
        : this(new DisposableReaderLock(rwl))
    { }

    public IDisposable Lock()
    {
        UnderlayedReaderLock.Enter();
        return UnderlayedReaderLock;
    }
}

public class DisposableWriterLock : DisposableReaderWriterLock
{
    public DisposableWriterLock(ReaderWriterLockSlim rwl)
        : base(rwl)
    { }

    public override void Enter()
        => BaseLock.EnterWriteLock();

    public override void Leave()
        => BaseLock.ExitWriteLock();
}

public struct WriterLockZone
{
    public DisposableWriterLock UnderlayedWriterLock;

    public WriterLockZone(DisposableWriterLock writerLock)
    {
        UnderlayedWriterLock = writerLock;
    }
    public WriterLockZone(ReaderWriterLockSlim rwl)
        : this(new DisposableWriterLock(rwl))
    { }

    public IDisposable Lock()
    {
        UnderlayedWriterLock.Enter();
        return UnderlayedWriterLock;
    }
}

#endif

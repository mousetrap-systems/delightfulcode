using System.Collections;

/// <summary>
/// Safe FIFO implementation for any type.
/// NOTE: If serializing, check to see that `T` is also serializable.
/// </summary>
[Serializable]
[Health(CodeStability.RequiresCommentary)]
public class CircularBuffer<T> : IEnumerable<T>, IEnumerable
{
    private readonly int _size;

    private readonly object _locker;

    private int _count;

    private int _head;

    private int _rear;

    private T[] _values;

    public int Size => _size;

    public object SyncRoot => _locker;

    public int Count => UnsafeCount;

    public int SafeCount
    {
        get
        {
            lock (_locker)
            {
                return UnsafeCount;
            }
        }
    }

    public int UnsafeCount => _count;

    public bool IsFull => (SafeCount >= Size) ? true : false;

    public CircularBuffer() : this(10) { } // Set a default size of 10

    public CircularBuffer(int max)
    {
        _size = max;
        _locker = new object();
        _count = 0;
        _head = 0;
        _rear = 0;
        _values = new T[_size];
    }

    private static int Incr(int index, int size)
    {
        return (index + 1) % size;
    }

    private void UnsafeEnsureQueueNotEmpty()
    {
        if (_count == 0)
        {
            throw new Exception("Empty queue");
        }
    }

    public void Enqueue(T obj)
    {
        UnsafeEnqueue(obj);
    }

    public void SafeEnqueue(T obj)
    {
        lock (_locker)
        {
            UnsafeEnqueue(obj);
        }
    }

    public void UnsafeEnqueue(T obj)
    {
        _values[_rear] = obj;
        if (Count == Size)
        {
            _head = Incr(_head, Size);
        }

        _rear = Incr(_rear, Size);
        _count = Math.Min(_count + 1, Size);
    }

    public T Dequeue()
    {
        return UnsafeDequeue();
    }

    public T SafeDequeue()
    {
        lock (_locker)
        {
            return UnsafeDequeue();
        }
    }

    public T UnsafeDequeue()
    {
        UnsafeEnsureQueueNotEmpty();
        T result = _values[_head];
        _values[_head] = default(T);
        _head = Incr(_head, Size);
        _count--;
        return result;
    }

    public T Peek()
    {
        return UnsafePeek();
    }

    public T SafePeek()
    {
        lock (_locker)
        {
            return UnsafePeek();
        }
    }

    public T UnsafePeek()
    {
        UnsafeEnsureQueueNotEmpty();
        return _values[_head];
    }

    public IEnumerator<T> GetEnumerator()
    {
        return UnsafeGetEnumerator();
    }

    public IEnumerator<T> SafeGetEnumerator()
    {
        lock (_locker)
        {
            List<T> list = new List<T>(_count);
            IEnumerator<T> enumerator = UnsafeGetEnumerator();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }

            return list.GetEnumerator();
        }
    }

    public IEnumerator<T> UnsafeGetEnumerator()
    {
        int index = _head;
        for (int i = 0; i < _count; i++)
        {
            yield return _values[index];
            index = Incr(index, _size);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
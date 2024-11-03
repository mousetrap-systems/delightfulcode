using System.Collections;

/// <summary>
/// Safe FIFO implementation for any type.
/// NOTE: If serializing, check to see that `T` is also serializable.
/// </summary>
[Author("GPT-4", 2024)]
[Serializable]
[Health(CodeStability.RequiresCommentary)]
public class CircularBuffer<T> : IEnumerable<T>, IEnumerable //, ICloneable
{
    private readonly int _size;

    private readonly object _locker;

    private int _count;

    // '_head' points to the oldest item in the buffer(the next item to be dequeued).
    // '_rear' points to the next available slot in the buffer(where the next item will be enqueued).

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

    /// <summary>
    /// Is the entire size of the buffer, full of items? Easy check.
    /// </summary>
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

    /// <summary>
    /// From the 'rear' this will return the most recent item added to the stack, if it's present.
    /// </summary>
    public T? GetItemFromOldestItems(int index)
    {
        if (index < 0 || index >= _count)
        {
            return default;
        }

        // Calculate the actual index in the buffer

        int actualIndex = (_head + index) % _size;

        return _values[actualIndex];
    }

    /// <summary>
    /// From the 'head' this will return the oldest item added to the buffer, if it's present.
    /// </summary>
    public T? GetItemFromMostRecent(int index)
    {
        if (index < 0 || index >= _count)
        {
            return default;
        }

        // Calculate the actual index in the buffer
        int actualIndex = (_rear - 1 - index + _size) % _size;

        return _values[actualIndex];
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

    // New method to iterate from rear to front
    public IEnumerable<T> IterateFromRearToFront()
    {
        lock (_locker)
        {
            for (int i = 0; i < _count; i++)
            {
                int index = (_rear - 1 - i + _size) % _size;
                yield return _values[index];
            }
        }
    }

    /// <summary>
    /// Clears the buffer by resetting the head, rear, and count. All items are removed.
    /// </summary>
    public void Clear()
    {
        lock (_locker)
        {
            _count = 0;
            _head = 0;
            _rear = 0;
            _values = new T[_size]; // Reset the array to default values
        }
    }

    /// <summary>
    /// Updates the item in the buffer, that was MOST RECENTLY ADDED
    /// </summary>
    public void UpdateMostRecent(T newValue)
    {
        if (_count == 0)
        {
            // it was empty previously, so just enqueue the item for simplicity
            Enqueue(newValue);
        }
        else
        {
            // Calculate the index of the most recent item
            int recentIndex = (_rear - 1 + _size) % _size;
            // int recentIndex = (_head) % _size;
            _values[recentIndex] = newValue;
        }
    }
}
using System.Collections;

namespace DelightfulCode
{
    /// <summary>
    /// Circular Buffer object, supporting any type, FIFO set of items given a specific capacity.
    /// </summary>
    /// <typeparam name="T">The type of elements in the buffer.</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    /// <example>
    /// This sample shows how to declare a new CircularBuffer of int.
    /// <code>
    ///   CircularBuffer<int> buffer = new CircularBuffer<int>();
    /// </code>
    /// </example>
    [Author("Joao Portela", 2022)]
    [Author("Warren James", 2024 - 2025)]
    [Health(CodeStability.MissionCritical)]
    [Health(CodeStability.SuperStable)]
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly T[] _buffer; // internal array of objects, sized initially at creation

        private int _start; // Index of the first element in buffer.

        private int _end; // Index after the last element in the buffer.

        private int _itemCount; // Current number of items in the buffer (i.e. aside from default<T>)

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularBuffer{T}"/> class.
        /// </summary>
        /// <param name='capacity'>Buffer capacity. Must be positive.</param>
        public CircularBuffer(int capacity)
        {
            if (capacity < 1) throw new ArgumentException("Circular buffer cannot have negative or zero capacity.", nameof(capacity));

            _buffer = new T[capacity];
            _itemCount = 0;
            _start = 0;
            _end = 0;
        }

        /// <summary>
        /// Maximum capacity of the buffer. Elements pushed into the buffer after
        /// maximum capacity is reached (IsFull = true), will cause an element to be removed.
        /// </summary>
        public int Capacity => _buffer.Length;

        /// <summary>
        /// Boolean indicating if Circular is at full capacity, if the item is full,
        /// adding more elements will cause elements to be removed from the other end of the buffer.
        /// </summary>
        public bool IsFull => ItemCount == Capacity;

        /// <summary>
        /// True if has no elements inside the ring.
        /// </summary>
        public bool IsEmpty => ItemCount == 0;

        /// <summary>
        /// The number of items currently in the circular buffer, ignoring any currently empty slots.
        /// This may be smaller than or equal to (but not larger than) the maximum CAPACITY.
        /// </summary>
        public int ItemCount => _itemCount;

        /// <summary>
        /// Element at the front of the buffer - this[0].
        /// MOST RECENT VALUE (for typical addition)
        /// </summary>
        /// <returns>The value of the element of type T at the front of the buffer.</returns>
        public T PeekHead
        {
            get
            {
                ThrowIfEmpty();
                return _buffer[_start];
            }
        }

        /// <summary>
        /// Element at the back of the buffer - this[Size - 1].
        /// Reveals the element but does not dislodge it.
        /// </summary>
        /// <returns>The value of the element of type T at the back of the buffer.</returns>
        public T PeekTail
        {
            get
            {
                ThrowIfEmpty();
                return _buffer[(_end != 0 ? _end : Capacity) - 1];
            }
        }

        /// <summary>
        /// Index access to elements in buffer.
        /// Index does not loop around like when adding elements,
        /// valid interval is [0;Size]
        /// </summary>
        /// <param name="index">Index of element to access.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when index is outside of [; Size[ interval.</exception>
        public T this[int index]
        {
            get
            {
                if (IsEmpty) throw new IndexOutOfRangeException(string.Format("Cannot access index {0} because the buffer is empty", index));
                if (index >= _itemCount) throw new IndexOutOfRangeException(string.Format("Cannot access index {0} because it is outside the buffer size of {1}", index, _itemCount));

                int actualIndex = InternalIndex(index);
                return _buffer[actualIndex];
            }

            set
            {
                if (IsEmpty) throw new IndexOutOfRangeException(string.Format("Cannot access index {0} because the buffer is empty", index));
                if (index >= _itemCount) throw new IndexOutOfRangeException(string.Format("Cannot access index {0} because it is outside the buffer size of {1}", index, _itemCount));

                int actualIndex = InternalIndex(index);
                _buffer[actualIndex] = value;
            }
        }

        /// <summary>
        /// Adds an item to the FRONT of the buffer. If the buffer is full,
        /// the item at the back is removed (i.e. it disappears).
        /// </summary>
        /// <param name="item">The item to add to the buffer.</param>
        public void EnqueueAtHead(T item)
        {
            PushHead(item);
        }

        /// <summary>
        /// Removes and returns the item at the FRONT of the buffer.
        /// Throws an exception if the buffer is empty.
        /// This operates like a PEZ dispenser action.
        /// </summary>
        /// <returns>The item that was at the front of the buffer.</returns>
        public T DequeueFromHead()
        {
            return PopHead();
        }

        /// <summary>
        /// Adds an item to the head of the buffer and removes an item from the tail if the buffer is full.
        /// If the buffer is not full, only the insert operation is performed. If it disloaded an item, you will get it.
        /// Note you need to be explicit about the type because any non-values in a non-nullable type will have defaults.
        /// </summary>
        /// <param name="item">The item to add to the buffer.</param>
        /// <returns>The item that was removed from the buffer, or null if the buffer was not full.</returns>
        public T? EnqueueAtHeadAndDequeueFromTail(T item)
        {
            T? itemToDequeue = default(T);

            if (IsFull)
            {
                itemToDequeue = PeekTail;
            }

            PushHead(item);
            return itemToDequeue;
        }

        /// <summary>
        /// Adds an item to the front of the buffer. If the buffer is full, the item at the back is removed.
        /// This is the primary method for adding items; 'Enqueue' is an alias to this method.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void PushHead(T item)
        {
            if (IsFull)
            {
                Decrement(ref _start, Capacity);
                _end = _start;
                _buffer[_start] = item;
            }
            else
            {
                Decrement(ref _start, Capacity);
                _buffer[_start] = item;
                ++_itemCount;
            }
        }

        /// <summary>
        /// Adds an item to the back of the buffer.
        /// If the buffer is full, the item at the front is removed.
        /// </summary>
        /// <param name="item">The item to add to the back of the buffer.</param>
        public void PushTail(T item)
        {
            if (IsFull)
            {
                _buffer[_end] = item;
                Increment(ref _end, Capacity);
                _start = _end;
            }
            else
            {
                _buffer[_end] = item;
                Increment(ref _end, Capacity);
                ++_itemCount;
            }
        }

        /// <summary>
        /// Removes and returns the item at the front of the buffer.
        /// This is the primary method for removing items; 'Dequeue' is an alias to this method.
        /// Throws an exception if the buffer is empty.
        /// </summary>
        /// <returns>The item that was at the front of the buffer.</returns>
        public T PopHead()
        {
            ThrowIfEmpty("Cannot dequeue from an empty buffer.");

            T item = _buffer[_start];
            _buffer[_start] = default(T); // wipe the existing
            Increment(ref _start, Capacity);
            --_itemCount;

            return item;
        }

        /// <summary>
        /// Removes and returns the item at the back of the buffer.
        /// Throws an exception if the buffer is empty.
        /// </summary>
        /// <returns>The item that was at the back of the buffer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the buffer is empty.</exception>
        public T PopTail()
        {
            ThrowIfEmpty("Cannot pop from an empty buffer.");

            T item = _buffer[(_end != 0 ? _end : Capacity) - 1];
            Decrement(ref _end, Capacity);
            _buffer[_end] = default(T); // wipe the existing
            --_itemCount;

            return item;
        }

        /// <summary>
        /// Clears the contents of the entire buffer - the 'Size' is reset to 0, while the original Capacity remains unchanged.
        /// The contents are replaced with the default value of type T. For reference types, this is null;
        /// for value types, this is different ... e.g. zero (0) for numeric types or false for bool, etc.
        /// </summary>
        public void Clear()
        {
            // to clear we just reset everything.
            _start = 0;
            _end = 0;
            _itemCount = 0;

            Array.Clear(_buffer, 0, _buffer.Length);
        }

        /// <summary>
        /// Converts the buffer to an array with the 'head' item as the first element.
        /// The 'head' is the most recently added item in the buffer.
        /// </summary>
        /// <returns>An array representation of the buffer with the 'head' item first.</returns>
        public T[] ToArrayWithHeadFirst()
        {
            T[] newArray = new T[ItemCount];
            int newArrayOffset = 0;
            var segments = ToArraySegments();

            foreach (ArraySegment<T> segment in segments)
            {
                if (segment.Array != null)
                {
                    Array.Copy(segment.Array, segment.Offset, newArray, newArrayOffset, segment.Count);
                    newArrayOffset += segment.Count;
                }
            }

            return newArray;
        }

        /// <summary>
        /// Converts the buffer to an array with the 'tail' item as the first element.
        /// The 'tail' is the oldest item in the buffer.
        /// </summary>
        /// <returns>An array representation of the buffer with the 'tail' item first.</returns>
        public T[] ToArrayWithTailFirst()
        {
            T[] newArray = new T[ItemCount];
            int newArrayOffset = 0;
            var segments = ToArraySegments();

            Array.Reverse(segments); // Reverse the segments so that we start from the tail

            foreach (ArraySegment<T> segment in segments)
            {
                if (segment.Array != null)
                {
                    // Copy the elements in reverse order
                    for (int i = segment.Count - 1; i >= 0; i--)
                    {
                        newArray[newArrayOffset++] = segment.Array[segment.Offset + i];
                    }
                }
            }

            return newArray;
        }

        /// <summary>
        /// Updates the most recently added item at the head of the buffer.
        /// NOTE: If the buffer is empty, an exception is thrown, so make sure you know ahead-of-time.
        /// </summary>
        [Author("Warren James", 2023)]
        public void UpdateHead(T newValue)
        {
            if (_itemCount == 0)
            {
                throw new InvalidOperationException("Buffer is empty.");
            }

            _buffer[_start] = newValue;
        }

        /// <summary>
        /// Returns the item at the specified position in the buffer.
        /// The position is calculated starting from the head of the buffer.
        /// </summary>
        /// <param name="offset">The position of the item to peek.</param>
        /// <returns>The item at the specified position.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the offset is outside the range of the buffer size.</exception>
        public T PeekAt(int offset)
        {
            if (offset < 0 || offset >= _itemCount)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be within the buffer size.");
            }

            // Calculate the actual index in the buffer
            int actualIndex = (_start + offset) % Capacity;

            return _buffer[actualIndex];
        }

        /// <summary>
        /// Retrieves the current state of the circular buffer as two ArraySegments. 
        /// These segments represent the actual layout of the data in the underlying array,
        /// which can be non-contiguous due to the nature of a circular buffer.
        /// 
        /// The logical order of elements (i.e., the order in which they were added to the buffer) 
        /// is maintained within each segment. The first segment contains the oldest elements,
        /// and the second segment contains the newest elements.
        /// 
        /// This method provides a performant way to access the buffer's contents without 
        /// creating a new array or copying elements, which is especially useful when working 
        /// with methods that accept a list of array segments.
        /// </summary>
        /// <remarks>
        /// Note that one or both of the returned segments could be empty, depending on the current state of the buffer.
        /// </remarks>
        /// <returns>An IList of two ArraySegments that together represent the contents of the buffer.</returns>
        public ArraySegment<T>[] ToArraySegments()
        {
            return new[] { GetFirstBufferSegment(), GetSecondBufferSegment() };
        }

        /// <summary>
        /// Returns an enumerator that iterates through this buffer.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate this collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var segments = ToArraySegments();
            foreach (ArraySegment<T> segment in segments)
            {
                if (segment.Array != null)
                {
                    for (int i = 0; i < segment.Count; i++)
                    {
                        yield return segment.Array[segment.Offset + i];
                    }
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through this buffer.
        /// This is the non-generic version of the GetEnumerator method, 
        /// and is required for compatibility with the non-generic IEnumerable interface.
        /// </summary>
        /// <returns>
        /// An object implementing the IEnumerator interface 
        /// that can be used to iterate through this buffer.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        /// <summary>
        /// Throws an InvalidOperationException if the buffer is empty.
        /// This method is used to check the state of the buffer before operations 
        /// that require it to not be empty, such as Dequeue or Peek.
        /// </summary>
        /// <param name="message">
        /// The error message that will be included in the InvalidOperationException. 
        /// If not provided, a default message will be used.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the buffer is empty.
        /// </exception>
        private void ThrowIfEmpty(string message = "Cannot access an empty buffer.")
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Increments the provided index, wrapping around to the beginning of the buffer if necessary.
        /// This method is used to navigate the circular buffer when adding or removing elements.
        /// </summary>
        /// <param name="index">The index to be incremented.</param>
        private static void Increment(ref int index, int capacity)
        {
            if (++index == capacity)
            {
                index = 0; // Wrap around to the beginning of the buffer
            }
        }

        /// <summary>
        /// Decrements the provided index, wrapping around to the end of the buffer if necessary.
        /// This method is used to navigate the circular buffer when adding or removing elements.
        /// </summary>
        /// <param name="index">The index to be decremented.</param>
        private static void Decrement(ref int index, int capacity)
        {
            if (index == 0)
            {
                index = capacity;
            }

            index--;
        }

        /// <summary>
        /// Transforms a logical index (based on the order elements were added) into an actual index in the underlying array.
        /// This method is used to navigate the circular buffer when accessing elements by their logical position.
        /// </summary>
        /// <param name="index">The logical index of the element.</param>
        /// <returns>The actual index of the element in the underlying array.</returns>
        private int InternalIndex(int index)
        {
            return _start + (index < (Capacity - _start) ? index : index - Capacity);
        }

        /// <summary>
        /// Gets the first segment of the buffer. This segment starts from the 'start' index and ends at the end of the buffer, or at the 'end' index if 'start' is less than 'end'.
        /// If the buffer is empty, it returns an empty segment.
        /// </summary>
        /// <returns>An ArraySegment representing the first segment of the buffer.</returns>
        private ArraySegment<T> GetFirstBufferSegment()
        {
            if (IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            else if (_start < _end)
            {
                return new ArraySegment<T>(_buffer, _start, _end - _start);
            }
            else
            {
                return new ArraySegment<T>(_buffer, _start, _buffer.Length - _start);
            }
        }

        /// <summary>
        /// Gets the second segment of the buffer. This segment starts from index 0 and ends at the 'end' index, if 'end' is less than or equal to 'start'.
        /// If 'end' is greater than 'start' or the buffer is empty, it returns an empty segment.
        /// </summary>
        /// <returns>An ArraySegment representing the second segment of the buffer.</returns>
        private ArraySegment<T> GetSecondBufferSegment()
        {
            if (IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            else if (_start < _end)
            {
                return new ArraySegment<T>(_buffer, _end, 0);
            }
            else
            {
                return new ArraySegment<T>(_buffer, 0, _end);
            }
        }
    }
}

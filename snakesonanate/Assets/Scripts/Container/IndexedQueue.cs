﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexedQueue<T>
{
    T[] array;
    int start;
    int len;

    public IndexedQueue()
    {
        array = new T[8];
        start = 0;
        len = 0;
    }

    public void Enqueue(T t)
    {
        if (len == array.Length)
        {
            //increase the size of the cicularBuffer, and copy everything
            T[] bigger = new T[array.Length * 2];
            for (int i = 0; i < len; i++)
            {
                bigger[i] = array[(start + i) % len];
            }
            start = 0;
            array = bigger;
        }
        array[(start + len) % array.Length] = t;
        ++len;
    }

    public T Dequeue()
    {
        var result = array[start];
        start = (start + 1) % array.Length;
        --len;
        return result;
    }

    public int Count { get { return len; } }

    public T this[int index]
    {
        get
        {
            return array[(start + index) % array.Length];
        }
    }
}
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct HistoryEleComp : IBufferElementData
{
    public float3 pos;
    public double timestamp;
}

[GenerateAuthoringComponent]
public struct HistoryRingBufComp : IComponentData
{
    public int bufferOpacity;
    public int bufferOpacityPlusOne => bufferOpacity + 1;
    public int end;
    public int start;
    // [Range(1, 60)]
    // public int samplePerSec;
    [HideInInspector]
    public double sampleAccumulator;

#if UNITY_EDITOR
    public int length;
    public void DebugLength()
    {
        length = Length;
    }
#endif

    public void UseDefault()
    {
        start = end = 0;
    }

    public bool IsEmpty => start == end;

    public bool IsFull => (end + 1) % bufferOpacityPlusOne == start;

    public int Length => ((end - start) + bufferOpacityPlusOne) % bufferOpacityPlusOne;

    public bool Push(out int idx)
    {
        if (IsFull)
        {
            idx = -1;
            return false;
        }
        idx = end++;
        end %= bufferOpacityPlusOne;
        return true;
    }

    public bool Pop(out int idx)
    {
        if (IsEmpty)
        {
            idx = -1;
            return false;
        }
        idx = --end;

        idx += bufferOpacityPlusOne;
        idx %= bufferOpacityPlusOne;

        end += bufferOpacityPlusOne;
        end %= bufferOpacityPlusOne;

        return true;
    }

    public bool Unshift(out int idx)
    {
        if (IsFull)
        {
            idx = -1;
            return false;
        }

        idx = --start;

        idx += bufferOpacityPlusOne;
        idx %= bufferOpacityPlusOne;

        start += bufferOpacityPlusOne;
        start %= bufferOpacityPlusOne;
        return true;
    }

    public bool Shift(out int idx)
    {
        if (IsEmpty)
        {
            idx = -1;
            return false;
        }

        idx = start++;
        start %= bufferOpacityPlusOne;

        return true;
    }

    public bool Head(out int idx)
    {
        if (IsEmpty)
        {
            idx = -1;
            return false;
        }
        idx = start;
        return true;
    }

    public int this[int idx]
    {
        get
        {
            return (start + idx) % bufferOpacityPlusOne;
        }
    }
}
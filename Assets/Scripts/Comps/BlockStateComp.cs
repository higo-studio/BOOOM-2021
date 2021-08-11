using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BlockStateComp : IComponentData
{
    public uint timestamp;
    public uint2 pos;
}
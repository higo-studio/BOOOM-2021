using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct BlockStateComp : IComponentData
{
    public uint timestamp;
}
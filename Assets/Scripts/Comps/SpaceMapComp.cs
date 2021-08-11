using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SpaceMapComp : IComponentData
{
    public uint width;
    public uint height;
    [Range(10, 100)]
    public uint cubeSize;
}

public struct SpaceMapStateComp : ISystemStateComponentData
{
}

public struct SpaceMapTileComp : IBufferElementData {
    public uint x;
    public uint y;
}
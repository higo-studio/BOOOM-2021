using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct SpaceMapComp : IComponentData
{
    public uint width;
    public uint height;
    [Range(10, 100)]
    public uint cubeSize;
}

public struct BlockEntityBuffer : IBufferElementData
{
    Entity entity;
}

public struct SpaceMapStateComp : ISystemStateComponentData
{

}
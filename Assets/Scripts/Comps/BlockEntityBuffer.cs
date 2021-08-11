using Unity.Entities;

[GenerateAuthoringComponent]
public struct BlockEntityBuffer : IBufferElementData
{
    Entity entity;
}

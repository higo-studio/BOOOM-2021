using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct HistoryPosComp : IBufferElementData
{
    public float3 pos;
}

using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class VelocityCompAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float x;
    public float y;
    public float z;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new VelocityComp()
        {
            Value = new float3(x, y, z)
        });
    }
}
public struct VelocityComp : IComponentData
{
    public float3 Value;
}
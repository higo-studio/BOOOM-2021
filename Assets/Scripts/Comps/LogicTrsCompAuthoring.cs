using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class LogicTrsCompAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var pos = transform.position;
        dstManager.AddComponentData(entity, new LogicTrsComp()
        {
            pos = pos,
        });
    }
}

public struct LogicTrsComp : IComponentData
{
    public float3 pos;
}
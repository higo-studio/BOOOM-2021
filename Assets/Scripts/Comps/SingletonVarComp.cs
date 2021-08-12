using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SingletonVarComp : IComponentData
{
    public Entity mainCamera;
}

[UpdateInGroup(typeof(GameObjectConversionGroup))]
public class SingletonConversionSys : GameObjectConversionSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
        DstEntityManager.World.GetOrCreateSystem<FixedStepSimulationSystemGroup>().Timestep = 1f;
    }
    protected override void OnUpdate()
    {
        var singletonEnt = DstEntityManager.CreateEntity(typeof(SingletonVarComp));
        DstEntityManager.SetName(singletonEnt, "Singeton Entity");

        Entities.ForEach((Camera camera) =>
        {
            var ent = GetPrimaryEntity(camera);
            DstEntityManager.SetComponentData(singletonEnt, new SingletonVarComp()
            {
                mainCamera = ent
            });
        });
    }
}
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SpaceMapInitSys : SystemBase
{
    public EntityArchetype blockArchetype;
    protected override void OnCreate()
    {
        base.OnCreate();
        blockArchetype = EntityManager.CreateArchetype(
            typeof(BlockStateComp)
        );
    }
    protected override void OnUpdate()
    {
        Entities.WithNone<SpaceMapStateComp>().ForEach((Entity entity, ref BlockEntityBuffer blockBuffer, in SpaceMapComp map) =>
        {
            for(var i = 0; i < 10; i++)
            {
                var ent = EntityManager.CreateEntity(blockArchetype);
                
            }
            EntityManager.AddComponentData(entity, new SpaceMapStateComp());
        });
    }
}

public class SpaceMapDestroySys : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, in Rotation rotation) =>
        {
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job. Note that the 'rotation' parameter is
            // marked as 'in', which means it cannot be modified,
            // but allows this job to run in parallel with other jobs
            // that want to read Rotation component data.
            // For example,
            //     translation.Value += math.mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
        }).Schedule();
    }
}

using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SpaceMapInitSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();

    }
    protected override void OnUpdate()
    {
        var blockArchetype = EntityManager.CreateArchetype(
            typeof(BlockStateComp),
            typeof(Parent),
            typeof(Translation)
        );

        Entities.WithNone<SpaceMapStateComp>().ForEach((Entity e, in SpaceMapComp space) =>
        {
            var buffer = EntityManager.AddBuffer<SpaceMapTileComp>(e);
            buffer.ResizeUninitialized((int)(space.width * space.height));
            for (uint i = 0; i < space.width; i++)
            {
                for (uint j = 0; j < space.height; j++)
                {
                    var idx = (int)(i * space.height + j);
                    var tile = buffer[idx];
                    tile.x = i;
                    tile.y = j;
                    buffer[idx] = tile;
                }
            }

            EntityManager.AddComponent<SpaceMapStateComp>(e);
        }).WithStructuralChanges().Run();

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

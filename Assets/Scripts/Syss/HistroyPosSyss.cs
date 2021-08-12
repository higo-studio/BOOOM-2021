using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class HistoryPosInitSys : SystemBase
{
    protected override void OnUpdate()
    {
        var mng = EntityManager;
        Entities.WithNone<HistoryInputEleComp>().ForEach((Entity entity, in HistoryRingBufComp ring) =>
        {
            var buffer = mng.AddBuffer<HistoryInputEleComp>(entity);
            buffer.ResizeUninitialized((int)ring.bufferOpacityPlusOne);
        }).WithStructuralChanges().Run();
    }
}

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public class RecordInputSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<SingletonInputComp>();
    }
    protected override void OnUpdate()
    {
        var curTime = Time.ElapsedTime;
        var dt = Time.DeltaTime;
        var input = GetSingleton<SingletonInputComp>();
        Entities.ForEach((ref DynamicBuffer<HistoryInputEleComp> posBuffer, ref HistoryRingBufComp ring, in Translation t) =>
        {
            ring.sampleAccumulator += dt;
            var requiredDt = (1d / dt);
            if (ring.sampleAccumulator < requiredDt)
            {
                return;
            }

            ring.sampleAccumulator -= requiredDt;

            if (ring.IsFull)
            {
                ring.Shift(out var _);
            }
            if (ring.Push(out var idx))
            {
                posBuffer[idx] = new HistoryInputEleComp()
                {
                    input = input,
                    timestamp = curTime
                };
            }
        }).ScheduleParallel();
    }
}
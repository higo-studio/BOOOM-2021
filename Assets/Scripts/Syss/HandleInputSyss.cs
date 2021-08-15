using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(RecordInputSys))]
public class HandleInputSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
    }
    protected override void OnUpdate()
    {
        var time = Time.DeltaTime;
        var timestamp = Time.ElapsedTime;
        Entities.WithBurst().ForEach((ref CharacterControllerInternalData internalInput, ref HistoryRingBufComp ring, in DynamicBuffer<HistoryInputEleComp> buf, in ReceiveInputComp receiveInput) =>
        {
            // internalInput.Input;
            if (ring.Head(out var idx))
            {
                var ele = buf[idx];
                if (timestamp < ele.timestamp + receiveInput.delay)
                {
                    return;
                }
                ring.Shift(out var _);
                internalInput.Input = ele.input;
            }
        }).Run();
    }
}

[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateBefore(typeof(HistoryPosInitSys))]
public class InputHistoryInitSys : SystemBase
{
    protected override void OnUpdate()
    {
        var mng = EntityManager;
        Entities.WithNone<HistoryRingBufComp>().ForEach((Entity e, in ReceiveInputComp receivInputComp) =>
        {
            mng.AddComponentData(e, new HistoryRingBufComp()
            {
                bufferOpacity = (int)math.ceil(receivInputComp.delay * 60 + 1)
            });
        }).WithStructuralChanges().Run();
    }
}
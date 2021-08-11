using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MovableSys : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        Entities.ForEach((ref LogicTrsComp t, in VelocityComp v) =>
        {
            t.pos += (v.Value * dt);
        }).ScheduleParallel();
    }
}

public class ShipRenderSys : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation tt, in LogicTrsComp t) =>
        {
            tt.Value = t.pos;
        }).ScheduleParallel();
    }
}

[UpdateAfter(typeof(ShipRenderSys))]
public class RecordShipPosPreSec : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref DynamicBuffer<HistoryPosComp> posBuffer, in LogicTrsComp t) =>
        {
            posBuffer.Add(new HistoryPosComp()
            {
                pos = t.pos
            });
        }).ScheduleParallel();
    }
}
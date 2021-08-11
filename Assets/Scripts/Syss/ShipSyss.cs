using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MovableSys : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        Entities.ForEach((ref Translation t, in VelocityComp v) =>
        {
            t.Value += (v.Value * dt);
        }).ScheduleParallel();
    }
}
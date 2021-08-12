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

[UpdateAfter(typeof(MovableSys))]
public class RecordPosPreSec : SystemBase
{
    protected override void OnUpdate()
    {
        var curTime = Time.ElapsedTime;
        var dt = Time.DeltaTime;
        Entities.ForEach((ref DynamicBuffer<HistoryEleComp> posBuffer, ref HistoryRingBufComp ring, in LogicTrsComp t) =>
        {
            ring.sampleAccumulator += dt;
            var requiredDt = (1d / ring.samplePerSec);
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
                posBuffer[idx] = new HistoryEleComp()
                {
                    pos = t.pos,
                    timestamp = curTime
                };
            }
#if UNITY_EDITOR
            ring.DebugLength();
#endif
        }).ScheduleParallel();
    }
}

[UpdateAfter(typeof(RecordPosPreSec))]
public class HistoryPosRenderSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<SingletonVarComp>();
    }
    protected override void OnUpdate()
    {
        var timestamp = Time.ElapsedTime;
        var gVar = GetSingleton<SingletonVarComp>();
        var cameraTrs = EntityManager.GetComponentData<Translation>(gVar.mainCamera);
        Entities.ForEach((ref Translation tt, ref HistoryRingBufComp ring, in DynamicBuffer<HistoryEleComp> historyBuffer) =>
        {
            for (var i = 0; i < ring.Length; i++)
            {
                var bufIdx = ring[i];
                var history = historyBuffer[bufIdx];
                if (history.timestamp + math.length(cameraTrs.Value - history.pos) / 1 > timestamp)
                {
                    break;
                }
                tt.Value = history.pos;
            }
        }).Schedule();
    }
}
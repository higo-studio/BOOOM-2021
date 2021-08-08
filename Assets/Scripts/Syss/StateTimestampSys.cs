using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class StateTimestampSys : SystemBase
{
    protected override void OnUpdate()
    {   
        Entities.ForEach((ref BlockStateComp blockState) => {
            blockState.timestamp++;
        }).Schedule();
    }
}

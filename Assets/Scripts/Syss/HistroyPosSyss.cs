using Unity.Entities;
using Unity.Mathematics;

[UpdateInGroup(typeof(InitializationSystemGroup ))]
public class HistoryPosInitSys : SystemBase
{
    protected override void OnUpdate()
    {
        var mng = EntityManager;
        Entities.WithNone<HistoryEleComp>().ForEach((Entity entity, in HistoryRingBufComp ring) => {
            var buffer = mng.AddBuffer<HistoryEleComp>(entity);
            buffer.ResizeUninitialized((int)ring.bufferOpacityPlusOne);
        }).WithStructuralChanges().Run();
    }
}
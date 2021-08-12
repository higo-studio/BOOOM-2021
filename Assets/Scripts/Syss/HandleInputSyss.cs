using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public class HandleInputSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<SingletonInputComp>();
    }
    protected override void OnUpdate()
    {
        var time = Time.DeltaTime;
        var input = GetSingleton<SingletonInputComp>();
        Entities.ForEach((ref Translation t, in VelocityComp vec) =>
        {
            t.Value.x += vec.Value.x * time * input.HorizontalAxis;
            t.Value.z += vec.Value.y * time * input.VerticalAxis;
        }).Schedule();
    }
}
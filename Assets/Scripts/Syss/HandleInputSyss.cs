using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public class HandleInputSys : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        RequireSingletonForUpdate<CharacterControllerInput>();
    }
    protected override void OnUpdate()
    {
        var time = Time.DeltaTime;
        var input = GetSingleton<CharacterControllerInput>();
        Entities.WithBurst().WithAll<ReceiveInputComp>().ForEach((ref CharacterControllerInternalData internalInput) =>
        {
            internalInput.Input = input;
        }).Schedule();
    }
}
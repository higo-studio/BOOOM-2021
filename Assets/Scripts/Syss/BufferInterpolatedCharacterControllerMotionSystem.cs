using Unity.Physics;
using Unity.Physics.GraphicsIntegration;
using Unity.Physics.Systems;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
// override the behavior of BufferInterpolatedRigidBodiesMotion
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(BuildPhysicsWorld)), UpdateBefore(typeof(ExportPhysicsWorld))]
[UpdateAfter(typeof(BufferInterpolatedRigidBodiesMotion)), UpdateBefore(typeof(CharacterControllerSystem))]
public class BufferInterpolatedCharacterControllerMotionSystem : SystemBase
{
    CharacterControllerSystem m_CharacterControllerSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_CharacterControllerSystem = World.GetOrCreateSystem<CharacterControllerSystem>();
    }

    protected override void OnUpdate()
    {
        Dependency = Entities
            .WithName("UpdateCCInterpolationBuffers")
            .WithNone<PhysicsExclude>()
            .WithBurst()
            .ForEach((ref PhysicsGraphicalInterpolationBuffer interpolationBuffer, in CharacterControllerInternalData ccInternalData, in Translation position, in Rotation orientation) =>
            {
                interpolationBuffer = new PhysicsGraphicalInterpolationBuffer
                {
                    PreviousTransform = new RigidTransform(orientation.Value, position.Value),
                    PreviousVelocity = ccInternalData.Velocity,
                };
            }).ScheduleParallel(Dependency);

        m_CharacterControllerSystem.AddInputDependency(Dependency);
    }
}

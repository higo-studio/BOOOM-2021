using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics;

public class InputAuthoring : MonoBehaviour
{
    CharacterControllerInput sInput = new CharacterControllerInput();
    private void Update()
    {
        var sys = World.DefaultGameObjectInjectionWorld.GetExistingSystem<SimulationSystemGroup>();
        sInput.Movement.x = Input.GetAxisRaw("Horizontal");
        sInput.Movement.y = Input.GetAxisRaw("Vertical");
        sInput.Jumped = Input.GetButton("Jump") ? 1 : 0;
        sys.SetSingleton(sInput);
        
    }
}

public struct SingletonVarComp : IComponentData
{
    public Entity mainCamera;
}

[UpdateInGroup(typeof(GameObjectConversionGroup))]
public class SingletonConversionSys : GameObjectConversionSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
        // DstEntityManager.World.GetOrCreateSystem<FixedStepSimulationSystemGroup>().Timestep = 0.2f;
    }
    protected override void OnUpdate()
    {
        var singletonEnt = DstEntityManager.CreateEntity(typeof(SingletonVarComp), typeof(CharacterControllerInput));
        DstEntityManager.SetName(singletonEnt, "Singeton Entity");

        Entities.ForEach((Camera camera) =>
        {
            var ent = GetPrimaryEntity(camera);
            DstEntityManager.SetComponentData(singletonEnt, new SingletonVarComp()
            {
                mainCamera = ent
            });
            DstEntityManager.SetComponentData(singletonEnt, new CharacterControllerInput());
        });
    }
}
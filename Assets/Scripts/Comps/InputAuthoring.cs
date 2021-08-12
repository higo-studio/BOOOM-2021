using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class InputAuthoring : MonoBehaviour
{
    SingletonInputComp sInput = new SingletonInputComp();
    private void FixedUpdate()
    {
        var sys = World.DefaultGameObjectInjectionWorld.GetExistingSystem<SimulationSystemGroup>();
        sInput.HorizontalAxis = Input.GetAxis("Horizontal");
        sInput.VerticalAxis = Input.GetAxis("Vertical");
        sInput.Jump = Input.GetButtonDown("Jump");
        sys.SetSingleton(sInput);
    }
}

public struct SingletonInputComp : IComponentData
{
    public float HorizontalAxis;
    public float VerticalAxis;
    public bool Jump;
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
        DstEntityManager.World.GetOrCreateSystem<FixedStepSimulationSystemGroup>().Timestep = 0.2f;
    }
    protected override void OnUpdate()
    {
        var singletonEnt = DstEntityManager.CreateEntity(typeof(SingletonVarComp), typeof(SingletonInputComp));
        DstEntityManager.SetName(singletonEnt, "Singeton Entity");

        Entities.ForEach((Camera camera) =>
        {
            var ent = GetPrimaryEntity(camera);
            DstEntityManager.SetComponentData(singletonEnt, new SingletonVarComp()
            {
                mainCamera = ent
            });
            DstEntityManager.SetComponentData(singletonEnt, new SingletonInputComp());
        });
    }
}
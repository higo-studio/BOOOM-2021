using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics;

public class InputAuthoring : MonoBehaviour
{
    public Camera mainCamera;
    public Transform plant;
    CharacterControllerInput sInput = new CharacterControllerInput();
    public Transform flag;
    public Space MovementSpace;
    private void Update()
    {
        var sys = World.DefaultGameObjectInjectionWorld.GetExistingSystem<SimulationSystemGroup>();
        sInput.Movement.x = Input.GetAxisRaw("Horizontal");
        sInput.Movement.y = Input.GetAxisRaw("Vertical");
        sInput.Jumped = Input.GetButton("Jump") ? 1 : 0;
        sys.SetSingleton(sInput);
        if (mainCamera)
        {
            var mousePos = Input.mousePosition;
            var mouseWordPos = mainCamera.ScreenToWorldPoint(new Vector3()
            {
                x = mousePos.x,
                y = mousePos.y,
                z = 10
            });
            var mouseOnPlantPoint = GameUtils.CalculateLineSurfacePoint(
                mainCamera.transform.position,
                mouseWordPos - mainCamera.transform.position,
                plant.position,
                Vector3.up
            );
            Debug.DrawLine(mainCamera.transform.position, mouseOnPlantPoint, Color.red);
            if (flag)
            {
                flag.position = mouseOnPlantPoint;
            }
            sInput.MovementSpace = MovementSpace;
            sInput.PointLookAt = mouseOnPlantPoint;
        }
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
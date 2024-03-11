using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour ShootAbility;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float dashSpeed = 1.5f;
    [SerializeField] private float shootForce = 3;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData() { Speed = this.moveSpeed });
        dstManager.AddComponentData(entity, new Dash() { DashForce = this.dashSpeed });

        if (ShootAbility != null && ShootAbility is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData() { Force = this.shootForce});
        }
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
    public float Dash;
    public float Shoot;
}

public struct MoveData : IComponentData
{
    public float Speed;
}

public struct ShootData : IComponentData
{
    public float Force;
}
public struct Dash : IComponentData
{
    public float DashForce;
}
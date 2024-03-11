using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour ShootAbility;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float shootForce = 3;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData() { Speed = this.moveSpeed });

        if (ShootAbility != null && ShootAbility is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData() { Force = this.shootForce});
        }
    }
}

public struct InputData : IComponentData
{
    public float2 Move;
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
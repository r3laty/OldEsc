using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour ShootAbility;
    [SerializeField] private PlayerSettings playerSettings;

    private float _moveSpeed;
    private float _dashSpeed;
    private float _shootForce;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _moveSpeed = playerSettings.MoveSpeed;
        _dashSpeed = playerSettings.DashingForce;
        _shootForce = playerSettings.ShootingForce;

        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData() { Speed = this._moveSpeed });
        dstManager.AddComponentData(entity, new Dash() { DashForce = this._dashSpeed });

        if (ShootAbility != null && ShootAbility is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData() { Force = this._shootForce });
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
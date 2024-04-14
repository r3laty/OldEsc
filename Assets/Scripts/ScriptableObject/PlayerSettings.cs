using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObject/Settings", order = 20)]
public class PlayerSettings : ScriptableObject
{
    public float ShootingForce;
    public float DashingForce;
    public float MoveSpeed;
}

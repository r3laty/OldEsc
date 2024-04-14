using UnityEngine;
using Zenject;

public class InjectionPlayerSettings : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettingsSO;

    private float _shootingForce;
    private float _dashingForce;
    private float _moveSpeed;
    
    private GetPlayerSettings _playerSettings;
    [Inject]
    public void Init(GetPlayerSettings playerSettings)
    {
        this._playerSettings = playerSettings;
    }
    private void Start()
    {
        _shootingForce = _playerSettings.ShootingForce;
        _dashingForce = _playerSettings.DashingForce;
        _moveSpeed = _playerSettings.MoveSpeed;
        Debug.Log($"{_shootingForce} - shooting, {_dashingForce} - dashing, {_moveSpeed} - move speed");

        playerSettingsSO.ShootingForce = _shootingForce;
        playerSettingsSO.DashingForce = _dashingForce;
        playerSettingsSO.MoveSpeed = _dashingForce;
    }
}

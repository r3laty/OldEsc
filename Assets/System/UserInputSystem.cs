using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery _movementQuery;

    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _dashAction;

    private float2 _moveInput;
    private float _dashInput;
    private float _shootInput;
    protected override void OnCreate()
    {
        _movementQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }
    protected override void OnStartRunning()
    {
        _moveAction = new InputAction(name: "move", binding: "<Gamepad>/rightStick");
        _moveAction.AddCompositeBinding("Dpad")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/s")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/d");

        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.Enable();

        _shootAction = new InputAction(name: "shoot", binding: "<Keyboard>/Space");
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable();

        _dashAction = new InputAction(name: "dash", binding: "<Keyboard>/leftShift");
        _dashAction.performed += context => { _dashInput = context.ReadValue<float>(); };
        _dashAction.started += context => { _dashInput = context.ReadValue<float>(); };
        _dashAction.canceled += context => { _dashInput = context.ReadValue<float>(); };
        _dashAction.Enable();
    }
    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _shootAction.Disable();
        _dashAction.Disable();
    }
    protected override void OnUpdate()
    {
        Entities.With(_movementQuery).ForEach(
         (Entity entity, ref InputData inputData) =>
         {
             inputData.Move = _moveInput;
             inputData.Shoot = _shootInput;
             inputData.Dash = _dashInput;
         });
    }

}

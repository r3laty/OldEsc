using Unity.Entities;

public class CharacterShootSystem : ComponentSystem
{
    private EntityQuery _shootQuery;
    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<ShootData>(),
            ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_shootQuery).ForEach(
        (Entity entity, UserInputData userInputData, ref InputData input, ref ShootData shootData) =>
        {
            if (input.Shoot > 0 && userInputData.ShootAbility != null && userInputData.ShootAbility is IAbility ability)
            {
                ability.Execute();
            }
        });
    }
}

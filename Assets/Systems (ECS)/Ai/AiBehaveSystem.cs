using Unity.Entities;
using static BehaviourManager;

public class AiBehaveSystem : ComponentSystem
{
    private EntityQuery _behaveQuery;
    protected override void OnCreate()
    {
        _behaveQuery = GetEntityQuery(ComponentType.ReadOnly<AiAgent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_behaveQuery).ForEach(
        (Entity entity, BehaviourManager manager) =>
        {
            manager.CurrentBehavior?.Behave();
        });
    }
}

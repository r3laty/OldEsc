using Unity.Entities;
using static BehaviourManager;

public class AiEvaluateSystem : ComponentSystem
{
    private EntityQuery _evaluateQuery;
    protected override void OnCreate()
    {
        _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AiAgent>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_evaluateQuery).ForEach(
        (Entity entity, BehaviourManager manager) =>
        {
            float highScore = float.MinValue;

            manager.CurrentBehavior = null;

            foreach (var behavior in manager.Behaviours)
            {
                if (behavior is IBehaviour ai)
                {
                    var currentScore = ai.Evaluate();
                    if (currentScore > highScore)
                    {
                        highScore = currentScore;
                        manager.CurrentBehavior = ai;
                    }
                }
            }
        });
    }
}

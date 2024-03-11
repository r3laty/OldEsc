using Unity.Entities;
using UnityEngine;

public class DashSystem : ComponentSystem
{
    private EntityQuery _dashQuery;
    protected override void OnCreate()
    {
        _dashQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                        ComponentType.ReadOnly<Dash>(),
                        ComponentType.ReadOnly<Transform>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_dashQuery).ForEach(
        (Entity entity, Transform transform, ref InputData inputData, ref Dash dash) =>
        {
            var pos = transform.position;
            pos += new Vector3(0, 0, inputData.Dash * dash.DashForce);
            transform.position = pos;
        });
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;
    private Collider[] _results = new Collider[50];
    private EntityManager _entityManager;
    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
            ComponentType.ReadOnly<Transform>());
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    protected override void OnUpdate()
    {
        Entities.With(_collisionQuery).ForEach((Entity entity, CollisionAbility collisionAbility, ref ActorColliderData colliderData) =>
        {
            var go = collisionAbility.gameObject;
            float3 position = go.transform.position;
            Quaternion rotation = go.transform.rotation;
            collisionAbility.Collisions?.Clear();

            int size = 0;

            switch (colliderData.ColliderType)
            {
                case ColliderType.Sphere:
                    size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, _results);
                    break;

                case ColliderType.Capsule:
                    var center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2;
                    var point0 = colliderData.CapsuleStart + position;
                    var point1 = colliderData.CapsuleEnd + position;

                    size = Physics.OverlapCapsuleNonAlloc(point0, point1, colliderData.CapsuleRadius, _results);
                    break;

                case ColliderType.Box:
                    size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents, _results,
                        colliderData.BoxOrientation * rotation);
                    break;
            }
            if (size > 0)
            {
                foreach (var result in _results)
                {
                    collisionAbility?.Collisions?.Add(result);
                }


                collisionAbility.Execute();
            }
        });
    }
}

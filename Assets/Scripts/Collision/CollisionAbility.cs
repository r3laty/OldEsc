using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider Collider;

    public List<MonoBehaviour> CollisionActions = new List<MonoBehaviour>();
    public List<IAbillityTarget> CollidersActionAbillities = new List<IAbillityTarget>();

    [HideInInspector] public List<Collider> Collisions;
    private void Start()
    {
        foreach (var item in CollisionActions)
        {
            if (item is IAbillityTarget ability)
            {
                CollidersActionAbillities.Add(ability);
            }
            else
            {
                Debug.LogError($"ACTION + {item.name} IS NOT ABILITY");
            }
        }
    }
    public void Execute()
    {
        foreach (var item in CollidersActionAbillities)
        {
            Collisions.ForEach(collider =>
            {
                if (collider != null) item.Targets.Add(collider.gameObject);
            });
            item.Execute();
        }
    }
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;

        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeoff = true
                });
                break;

            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    SphereCenter = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    SphereRadius = capsuleRadius,
                    initialTakeoff = true
                });
                break;

            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeoff = true
                });
                break;
        }
        Collider.enabled = false;
    }
}

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool initialTakeoff;
}
public enum ColliderType
{
    Sphere,
    Capsule,
    Box
}

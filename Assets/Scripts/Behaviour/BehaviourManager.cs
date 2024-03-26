using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<Behaviour> Behaviours = new List<Behaviour>();
    public IBehaviour CurrentBehavior;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AiAgent>(entity);
    }

    private void Start()
    {
         
    }

    public struct AiAgent : IComponentData
    {

    }
}

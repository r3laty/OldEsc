using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IAbillityTarget
{
    public int Damage = 10;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null)
            {
                if (health.Health <= 0)
                {
                    Damage = 0;
                }
                health.Damage(Damage);
            }
        }
    }
}

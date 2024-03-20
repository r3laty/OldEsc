using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IAbillityTarget
{
    public int NeedToHeal = 10;
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null)
            {
                if (health.Health >= 100)
                {
                    NeedToHeal = 0;
                }
                health.Heal(NeedToHeal);
            }
        }
    }
}

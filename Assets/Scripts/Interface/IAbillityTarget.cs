using System.Collections.Generic;
using UnityEngine;

public interface IAbillityTarget : IAbility
{
    List<GameObject> Targets { get; set; }
}


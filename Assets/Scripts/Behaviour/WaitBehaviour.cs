using UnityEngine;

public class WaitBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] private GameObject character;
    public void Behave()
    {
        print("WAIT");
    }

    public float Evaluate()
    {
        return Vector3.Distance(transform.position, character.transform.position);
    }
}

using UnityEngine;

public class RotateBehavior : MonoBehaviour, IBehaviour
{
    [SerializeField] private GameObject character;
    [Space]
    [SerializeField] private GameObject bulletPrefab;
    [Space]
    [SerializeField] private float shootingForce = 1;
    public void Behave()
    {
        var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var newBulletRb = newBullet.GetComponent<Rigidbody>();

        newBulletRb.AddForce(Vector3.left * (shootingForce / 1), ForceMode.Impulse);

        Debug.Log($"Find enemy in {Vector3.Distance(transform.position, character.transform.position)} meters");
    }

    public float Evaluate()
    {
        return 10;
    }
}

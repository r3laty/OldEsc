using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;

    private float _shootTime = float.MinValue;
    public void Execute(float shootingForce)
    {
        if (Time.time < _shootTime + shootDelay)
        {
            return;
        }
        _shootTime = Time.time;

        if (bullet != null)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * shootingForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarningFormat("[NO PREFAB] " +
                "\nThere is no bullet prefab in" + 
                this.name + " script");
        }
    }
}

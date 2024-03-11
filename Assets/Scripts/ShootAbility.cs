using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;

    private float _shootTime = float.MinValue;
    private bool _canDestroy;
    public void Execute(float shootingForce)
    {
        Vector3 shot = new Vector3(shootingForce, 0, 0);
        if (Time.time < _shootTime + shootDelay)
        {
            return;
        }
        _shootTime = Time.time;

        if (bullet != null)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.back * shootingForce, ForceMode.Impulse);

            print("Shoted!");
            print(shootingForce + " Shooting force");
        }
        else
        {
            Debug.LogWarningFormat("[NO PREFAB] " +
                "\nThere is no bullet - prefab");
        }
    }
}

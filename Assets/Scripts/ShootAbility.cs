using System;
using TMPro;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    [HideInInspector] public PlayerStats stats = new PlayerStats();

    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootingForce = 10;
    [Space]
    [SerializeField] private TextMeshProUGUI shootCount;
    [SerializeField] private string _textPatter = "Количество выстрелов: ";

    private float _shootTime = float.MinValue;
    private void Start()
    {
        var jsonString = PlayerPrefs.GetString("ShootCount");
        if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
        {
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }

        var jsonStr = jsonString.Substring(14);
        var convertedJsonString = jsonStr.TrimEnd('}');

        shootCount.text = _textPatter + convertedJsonString;
    }
    public void Execute()
    {
        if (Time.time < _shootTime + shootDelay)
        {
            return;
        }
        _shootTime = Time.time;

        if (bullet != null)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            stats.ShootCount++;
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

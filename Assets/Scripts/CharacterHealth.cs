using System;
using TMPro;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int Health = 100;
    [Space]
    [SerializeField] private ShootAbility shootAbility;
    [Space]
    [SerializeField] private TextMeshProUGUI hpCountText;
    [SerializeField] private string hpCountPattern;
    
    private void Update()
    {
        hpCountText.text = hpCountPattern + Health.ToString();
    }
    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            WriteStats();
            
            Destroy(gameObject);
        }
    }

    private void WriteStats()
    {
        var jsonString = JsonUtility.ToJson(shootAbility.stats);
        Debug.Log(jsonString);
        PlayerPrefs.SetString("ShootCount", jsonString);
    }
}

using TMPro;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int Health = 100;
    
    [SerializeField] private TextMeshProUGUI hpCountText;
    [SerializeField] private string hpCountPattern;
    
    private void Update()
    {
        hpCountText.text = hpCountPattern + Health.ToString();
    }
    public void Heal(int heal)
    {
        Health += heal;
    }
}

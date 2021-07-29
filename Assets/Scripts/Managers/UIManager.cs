using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject damageTextPrefab;
    public GameObject moneyTextPrefab;
    public GameObject healthTextPrefab;

    private void Awake() 
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DisplayDamage(Vector3 position, float damage, bool crit = false) //Stay in place
    {
        GameObject DamageTextInstance = Instantiate(damageTextPrefab);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());
        DamageTextInstance.transform.position = position;
    }

    public void DisplayDamage(Transform transform, float damage, bool crit = false) //Follow hit object
    {
        GameObject DamageTextInstance = Instantiate(damageTextPrefab, transform);
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());
    }

    public void DisplayMoneyPickup(Vector3 position, float value)
    {
        GameObject MoneyTextInstance = Instantiate(moneyTextPrefab);
        MoneyTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("+ " + value.ToString());
        MoneyTextInstance.transform.position = position;
    }

    public void DisplayHealthPickup(Vector3 position, float value)
    {
        GameObject HealthTextInstance = Instantiate(healthTextPrefab);
        HealthTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("+ " + value.ToString());
        HealthTextInstance.transform.position = position;
    }
}

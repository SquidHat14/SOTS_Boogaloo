using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject damageTextPrefab;
    public GameObject moneyTextPrefab;
    public GameObject healthTextPrefab;

    public GameObject damageCritTextPrefab;

    public Slider slider;

    public TextMeshProUGUI moneyText;

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

    public void DisplayDamage(Vector3 position, float damage, bool crit) //Stay in place
    {
        GameObject DamageTextInstance;
        if(crit)
        {
            DamageTextInstance = Instantiate(damageCritTextPrefab);
        }
        else
        {
            DamageTextInstance = Instantiate(damageTextPrefab);
        }
        TextMeshPro damageText = DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>();
        damageText.SetText(damage.ToString());
        DamageTextInstance.transform.position = position;
    }

    public void DisplayDamage(Transform transform, float damage, bool crit) //Follow hit object
    {
        GameObject DamageTextInstance;

        if(crit)
        {
            DamageTextInstance = Instantiate(damageCritTextPrefab, transform);
        }
        else
        {
            DamageTextInstance = Instantiate(damageTextPrefab, transform);
        }
        TextMeshPro damageText = DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>();
        damageText.SetText(damage.ToString());
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

    public void moveHealthSlider(float currentHealth, float maxHealth)
    {

    }

    public void setMaxHealthText()
    {

    }

    public void setMoneyText(float value)
    {
        moneyText.SetText(value.ToString());
    }
}

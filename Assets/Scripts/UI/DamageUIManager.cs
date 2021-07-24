using UnityEngine;
using TMPro;

public class DamageUIManager : MonoBehaviour
{
    public static DamageUIManager Instance;

    public GameObject damageTextPrefab;

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
}

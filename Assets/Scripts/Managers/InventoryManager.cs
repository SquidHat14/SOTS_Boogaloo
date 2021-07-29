using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<ScriptableObject, int> inventory = new Dictionary<ScriptableObject, int>();

    private int money;

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

    public void addMoney(int value)
    {
        money += value;
    }

    public int getMoney()
    {
        return money;
    }

    public void addItemToInventory(ScriptableObject item, int count)
    {
        inventory.Add(item, count);
    }
}

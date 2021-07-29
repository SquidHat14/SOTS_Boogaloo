using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    private float _health = 100f;
    public float Health {get {return _health;} set {_health = value;}}

    private float maxHealth = 100f;

    private float _healModifier = 1f;
    public float HealModifier {get {return _healModifier;} set {_healModifier = value;}}

    private float _damageModifier = 1f;
    public float DamageModifier {get {return _damageModifier;} set {_damageModifier = value;}}

    private float _inbounddamageModifier = 1f;
    public float InboundDamageModifier {get {return _inbounddamageModifier;} set {_inbounddamageModifier = value;}}

    private float _speedModifier = 1f;
    public float SpeedModifier {get {return _speedModifier;} set {_speedModifier = value;}}

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

    public void Heal(float value)
    {
        _health += Mathf.Clamp(_health + (value * _healModifier), 0, maxHealth);
    }

    public void TakeDamage(float value)
    {
        _health -= Mathf.Clamp(_health - (value * _inbounddamageModifier), 0, maxHealth);
        if(_health <= 0)
        {
            Debug.Log("Dead");
            //Die()
        }
    }
}

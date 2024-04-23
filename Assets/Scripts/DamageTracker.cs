using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DamageTracker : MonoBehaviour {
    public static float damage = 0;
    public TextMeshProUGUI damageText;

    void Start() {

    }

    void Update() {
        damageText.text = $"Damage: {damage:F2}%";
    }

    public void AddDamage(float damageToAdd) {
        float newDmg = damage + damageToAdd;
        if (newDmg > damage) {
            damage += damageToAdd;
        }
    }
}
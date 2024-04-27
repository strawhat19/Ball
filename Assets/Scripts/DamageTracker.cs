using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DamageTracker : MonoBehaviour {
    public static float damage = 0;
    public TextMeshProUGUI damageText;

    void Update() {
        GlobalData.Damage = damage;
        string updatedDamage = GlobalData.RemoveDotZeroZero(damage.ToString("F2"));
        damageText.text = $"Damage: {updatedDamage}%";
    }

    public void AddDamage(float damageToAdd) {
        float newDmg = damage + damageToAdd;
        if (newDmg > damage) {
            damage += damageToAdd;
        }
    }
}
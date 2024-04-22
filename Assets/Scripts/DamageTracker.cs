using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DamageTracker : MonoBehaviour {
    public static float damage = 0;
    public TextMeshProUGUI damageText;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        damageText.text = $"Damage: {damage}%";
    }

    public void AddDamage(float damageToAdd) {
        float newDmg = damage + damageToAdd;
        if (newDmg > damage) {
            damage += damageToAdd;
        }
    }
}
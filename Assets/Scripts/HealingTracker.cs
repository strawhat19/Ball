using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealingTracker : MonoBehaviour {
    public static float healing = 0;
    public TextMeshProUGUI healingText;

    void Start() {

    }

    void Update() {
        healingText.text = $"Healing: {healing:F2}%";
    }

    public void AddHealing(float healingToAdd) {
        float newHP = healing + healingToAdd;
        if (newHP > healing) {
            healing += healingToAdd;
        }
    }
}